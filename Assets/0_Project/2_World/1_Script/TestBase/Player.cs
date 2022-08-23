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
        [field: SerializeField]
        public Vector2 look { get; set; }
        [field: SerializeField]
        public bool fire { get; set; }


        #region �ӽ� ����
        [Header (" - Player Settings")]
        [SerializeField]
        float FireRange;
        [SerializeField]
        float PlayerAttackAngle;
        [SerializeField]
        float PlayerDashCoolDown = 0.5f;
        [SerializeField]
        float PlayerDashTime = 0.3f;
        [SerializeField]
        float PlayerDashDistance = 4f;

        [Header("TempAttackEffect")]
        [SerializeField]
        LineRenderer LineRenderer;
        [SerializeField]
        int LineSegment;

        [Header(" - WeaponSetting")]
        [SerializeField]
        float WeaponATKSpeed;

        #endregion


        
        bool isAttack;
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
                    Debug.Log("�ش� playerinput �� �ش��ϴ� actiontype�� ���� ���� ����");
                }
            }
            
            playerInput.onActionTriggered += OnActionTriggered;
            StartCoroutine(PlayerFire());
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
                    //Debug.Log("���ǵ��� ����");
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
                RB2D.velocity = (isAttack || fire) ? Vector3.zero : (Vector3)move * ����Speed;
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

        

        IEnumerator PlayerFire()
        {
            while(true)
            {
                yield return new WaitUntil(() => fire == true);
                isAttack = true;
                Vector3 templook = look;


                Collider2D[] inHitBox = Physics2D.OverlapCircleAll(transform.position, FireRange);

                StartCoroutine(CreateCircle(FireRange, Mathf.Atan2(templook.y - transform.position.y, templook.x - transform.position.x) * Mathf.Rad2Deg, PlayerAttackAngle, transform.position));


                foreach (Collider2D t in inHitBox)
                {
                    float angle = Vector2.Angle(templook - transform.position, t.transform.position - transform.position);
                    if (angle < PlayerAttackAngle / 2 && t.CompareTag("Enemy"))
                    {
                        t.GetComponent<TestEnemy>().GetDamaged();
                    }



                }

                yield return new WaitForSeconds(����ATKSpeed);
                isAttack = false;
            }
            
        }
        IEnumerator PlayerDash()
        {
            while (true)
            {
                yield return new WaitUntil(() => dash == true);
                isDash = true;
                DashVector = move * PlayerDashDistance / PlayerDashTime;


                yield return new WaitForSeconds(PlayerDashTime);
                isDash = false;
                yield return new WaitForSeconds(PlayerDashCoolDown);
                dash = false;
            }

        }
        IEnumerator CreateCircle(float radius, float angle, float angleRange, Vector3 position)
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
            yield return new WaitForSeconds(����ATKSpeed * 0.8f);
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
