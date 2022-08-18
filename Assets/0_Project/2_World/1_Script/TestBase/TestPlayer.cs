using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace TenMinute {
    public class TestPlayer : MonoBehaviour {
        [SerializeField]
        private PlayerInput playerInput;
        private Dictionary<ActionType, InputAction> _actionSet;


        
        
        [field: SerializeField]
        public Vector2 move { get; set; }
        [field: SerializeField]
        public Vector2 look { get; set; }
        [field: SerializeField]
        public bool fire { get; set; }


        #region 임시 변수
        [Header ("TempPlayerSettings")]
        [SerializeField]
        float PlayerSpeed;
        [SerializeField]
        GameObject tempPointerObject;
        [SerializeField]
        float tempFireRange;
        [SerializeField]
        float tempPlayerAttackAngle;
        #endregion

        [Header ("TempAttackEffect")]
        [SerializeField]
        LineRenderer tempLineRenderer;
        [SerializeField]
        int LineSegment;


        
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
                    tempPointerObject.transform.position = look;

                    break;
                case ActionType.Fire:
                    if(obj.action.phase == InputActionPhase.Started)
                    {
                        Debug.Log("Attack");
                        fire = true;
                        PlayerFire();
                    }
                    else
                    {
                        Debug.Log($"Attack {obj.action.phase}");
                        fire = false;
                    }                    
                    break;
                case ActionType.None:
                    //Debug.Log("정의되지 않음");
                    break;
            }
            
        }

        private void Update()
        {
            transform.position += (Vector3)move * PlayerSpeed;
            
        }

        private ActionType GetActionType(string name)
        {
            if(Enum.TryParse(name, out ActionType type))
            {
                return type;
            }
            return ActionType.None;
        }

        

        void PlayerFire()
        {
            Vector3 templook = look;
            

            Collider2D[] inHitBox = Physics2D.OverlapCircleAll(transform.position, tempFireRange);

            StartCoroutine(CreateCircle(tempFireRange, Mathf.Atan2(look.y - transform.position.y, look.x - transform.position.x) * Mathf.Rad2Deg, tempPlayerAttackAngle, transform.position));


            foreach (Collider2D t in inHitBox)
            {
                float angle = Vector2.Angle(templook - transform.position, t.transform.position - transform.position);
                if (angle < tempPlayerAttackAngle / 2 && t.CompareTag("Enemy"))
                {
                    t.GetComponent<TestEnemy>().GetDamaged();
                }
                
                
                
            }
        }
        IEnumerator CreateCircle(float radius, float angle, float angleRange, Vector3 position)
        {
            float x, y;
            float tempAngle = angle - angleRange/2;
            tempLineRenderer.positionCount = LineSegment + 1;
            tempLineRenderer.enabled = true;

            for (int i = 0; i<LineSegment + 1; i++)
            {
                x = Mathf.Cos(Mathf.Deg2Rad * tempAngle) * radius + position.x;
                y = Mathf.Sin(Mathf.Deg2Rad * tempAngle) * radius + position.y;

                tempLineRenderer.SetPosition(i, new Vector3(x, y, -5));
                tempAngle += angleRange / LineSegment;
            }
            yield return new WaitForSeconds(0.1f);
            tempLineRenderer.enabled = false;
        }

    }

    

    enum ActionType
    {
        None = 0,
        Move,
        Look,
        Fire
        
    }
}
