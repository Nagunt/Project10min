using System.Collections;
using System.Collections.Generic;
using TenMinute.Event;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TenMinute {
    public class Player : Character {
        [Header("- Player")]
		[SerializeField]
        private PlayerInput playerInput;

        // input
        private Vector2 _move;
        private Vector2 _look;
        private bool _sprint;
        private bool _dodge;
        private bool _attack;

        public override void Init() {
            base.Init();
			½ºÅÝSpeed = 5f;
			playerInput.onActionTriggered += OnInput;
		}

		private void OnInput(InputAction.CallbackContext data) {
			switch (data.action.name) {
				case "Move":
					_move = data.ReadValue<Vector2>();
					break;
			}
		}

		private void Update() {
			Move(_move);
		}
	}
}
