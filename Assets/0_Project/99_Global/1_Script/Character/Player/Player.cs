using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TenMinute.Physics;
using DG.Tweening;

namespace TenMinute {
    public class Player : Character {

        public bool IsDash { get; private set; } = false;
        private Sequence _dashSequence;

        [Header("- Player")]
        public int WeaponIndex;

        #region ют╥б

        Vector2 _moveDir;
        Vector2 _lookDir;

        public void OnMove(InputAction.CallbackContext context) {
            _moveDir = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context) {
            Vector2 mousePosition = Global_PhysicsManager.Instance.ScreenToWorldPosition(context.ReadValue<Vector2>());
            _lookDir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.performed) {
                
            }
        }

        public void OnDash(InputAction.CallbackContext context) {
            if (context.performed) {
                if (IsDash || _dashSequence.IsActive()) return;

                Vector2 distance = _lookDir * 4f;

                _dashSequence = DOTween.Sequence(this);
                _dashSequence.
                    OnStart(() => RB2D.velocity = Vector2.zero).
                    Append(RB2D.DOMove(transform.position + new Vector3(distance.x, distance.y, transform.position.z), 0.3f)).
                    AppendCallback(() => IsDash = false).
                    AppendInterval(0.2f).
                    OnKill(() => _dashSequence = null).
                    Play();
            }
        }

        #endregion

        public override void Init() {
            base.Init();
            tag = "Player";
        }

        private void Update() {
            if (_moveDir == Vector2.zero) Stop();
            else Move(_moveDir);
        }
    }
}
