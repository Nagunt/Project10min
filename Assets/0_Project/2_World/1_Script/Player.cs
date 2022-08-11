using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace TenMinute {
    public class Player : MonoBehaviour {
        [SerializeField]
        private PlayerInput playerInput;
        private Dictionary<ActionType, InputAction> _actionSet;


        
        
        [field: SerializeField]
        public Vector2 move { get; set; }
        [field: SerializeField]
        public Vector2 look { get; set; }
        [field: SerializeField]
        public bool fire { get; set; }


        [SerializeField]
        float PlayerSpeed;


        #region �ӽ� ����
        [SerializeField]
        GameObject tempPointerObject;
        [SerializeField]
        float tempFireRange;

        #endregion
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
                        playerFire();
                    }
                    else
                    {
                        Debug.Log($"Attack {obj.action.phase}");
                        fire = false;
                    }                    
                    break;
                case ActionType.None:
                    //Debug.Log("���ǵ��� ����");
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

        void playerFire()
        {

            Collider2D[] inHitBox = Physics2D.OverlapCircleAll(transform.position, tempFireRange);

            foreach (Collider2D t in inHitBox)
            {
                Debug.LogWarning("Hit!");
                t.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                
            }
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
