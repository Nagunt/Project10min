using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace TenMinute {
    public class Player : Character {
        [Header("- Player")]

        [SerializeField]
        private PlayerInput playerInput;
        private Dictionary<ActionType, InputAction> _actionSet;


        
        
        [field: SerializeField]
        public Vector2 move { get; set; }
        private Vector2 postMove;
        [field: SerializeField]
        public Vector2 look { get; set; }
        
        [field: SerializeField]
        public bool fire { get; set; }


        #region 임시 변수
        [Header (" - Player Settings")]
        
        [SerializeField]
        float PlayerDashCoolDown = 0.5f;
        [SerializeField]
        float PlayerDashTime = 0.3f;
        [SerializeField]
        float PlayerDashDistance = 4f;

        

        [Header(" - Weapon")]
        [SerializeField]
        PlayerAttack playerAttack;

        [Header("TempAttackEffect")]
        [SerializeField]
        LineRenderer LineRenderer;
        [SerializeField]
        int LineSegment;

        #endregion



        public bool isAttack { get; set; }
        bool dash;
        bool isDash;
        Vector3 DashVector;

        private void Start() 
        {
            _actionSet = new Dictionary<ActionType, InputAction>();
            foreach (string name in Enum.GetNames(typeof(ActionType)))
            {
                try
                {
                    InputAction currentAction = playerInput.actions[name];
                    if(currentAction != null)
                    {
                        ActionType actionType = (ActionType)Enum.Parse(typeof(ActionType), name);
                        _actionSet.Add(actionType, currentAction);
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.Log("해당 playerinput 에 해당하는 actiontype이 존재 하지 않음");
                }
            }
            
            playerInput.onActionTriggered += OnActionTriggered;
            StartPlayerFire();
            StartCoroutine(PlayerDash());
        }

        private void OnActionTriggered(InputAction.CallbackContext obj) {
            /*if (playerInput.currentActionMap == obj.action.actionMap) {
                Debug.Log(obj.action);
                
            }*/
            switch (GetActionType(obj.action.name))
            {
                case ActionType.Move:
                    move = obj.ReadValue<Vector2>();
                    postMove = move != Vector2.zero ? move : postMove;

                    break;
                case ActionType.Look:
                    
                    look = Camera.main.ScreenToWorldPoint( obj.ReadValue<Vector2>());
                    
                    

                    break;
                case ActionType.Fire:
                    if(obj.action.phase == InputActionPhase.Started)
                    {
                        Debug.Log("Attack");
                        fire = true;
                        
                    }
                    else if(obj.action.phase == InputActionPhase.Canceled)
                    {
                        Debug.Log($"Attack {obj.action.phase}");
                        fire = false;
                    }                    
                    break;
                case ActionType.Dash:
                    if(obj.action.phase == InputActionPhase.Started)
                    {
                        dash = true;
                        Debug.Log("Dash");
                    }
                    
                    break;
                
                case ActionType.None:
                    //Debug.Log("정의되지 않음");
                    break;
            }
            
        }

        private void FixedUpdate()
        {
            if(isDash)
            {
                RB2D.velocity = DashVector;
            }
            else
            {
                RB2D.velocity = (isAttack || fire) ? Vector3.zero : (Vector3)move * 스텟Speed;
            }
        }
        

        private ActionType GetActionType(string name)
        {
            if(Enum.TryParse(name, out ActionType type))
            {
                return type;
            }
            return ActionType.None;
        }

        
        void StartPlayerFire()
        {
            StartCoroutine(playerAttack.PlayerFire());
        }
        
        IEnumerator PlayerDash()
        {
            while (true)
            {
                yield return new WaitUntil(() => dash == true);
                isDash = true;
                //여기서부터
                int layerMask = 1 << LayerMask.NameToLayer("Terrain");
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, look,Mathf.Infinity,layerMask);
                if(hit.Length != 0)
                {
                    Debug.Log(hit[0].transform.gameObject);
                }
                
                DashVector = postMove * PlayerDashDistance / PlayerDashTime;
                
                //
                yield return new WaitForSeconds(PlayerDashTime);
                isDash = false;
                yield return new WaitForSeconds(PlayerDashCoolDown);
                dash = false;
            }

        }
        public IEnumerator CreateCircle(float radius, float angle, float angleRange, Vector3 position)
        {
            float x, y;
            float Angle = angle - angleRange/2;
            LineRenderer.positionCount = LineSegment + 1;
            LineRenderer.enabled = true;

            for (int i = 0; i < LineSegment + 1; i++)
            {
                x = Mathf.Cos(Mathf.Deg2Rad * Angle) * radius + position.x;
                y = Mathf.Sin(Mathf.Deg2Rad * Angle) * radius + position.y;

                LineRenderer.SetPosition(i, new Vector3(x, y, -5));
                Angle += angleRange / LineSegment;
            }
            yield return new WaitForSeconds(스텟ATKSpeed * 0.8f);
            LineRenderer.enabled = false;
        }

    }

    

    enum ActionType
    {
        None = 0,
        Move,
        Look,
        Fire,
        Dash
    }
}
