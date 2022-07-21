using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TenMinute {
    public class Player : MonoBehaviour {
        [SerializeField]
        private PlayerInput playerInput;

        private void Start() {
            playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext obj) {
            if (playerInput.currentActionMap == obj.action.actionMap) {
                Debug.Log(obj);
            }
        }

    }
}
