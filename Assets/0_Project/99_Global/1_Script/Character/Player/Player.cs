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

        public bool CanAction => IsAlive && IsDash == false && IsAttack == false && IsKnockDown == false;

        public Weapon weapon;

        #region 입력

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
                if (CanAction == false) return;
                if (_attackRoutine != null) {
                    StopCoroutine(_attackRoutine);
                    _attackRoutine = null;
                    weapon.AddCombo();
                }
                IsAttack = true;
                _attackRoutine = StartCoroutine(AttackRoutine());
            }

            IEnumerator AttackRoutine() {
                weapon.Execute(() => IsAttack = false);
                yield return new WaitUntil(() => IsAttack == false);
                float inputTime = 0.5f;
                while (inputTime >= 0) {
                    inputTime -= Time.deltaTime;
                    yield return null;
                }
                weapon.Clear();
                _attackRoutine = null;
            }
        }

        public void OnDash(InputAction.CallbackContext context) {
            if (context.performed) {
                if (CanAction == false || _dashSequence.IsActive()) return;

                weapon.Cancel();

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

        public override void KnockDown(float time) {
            weapon.Cancel();
            base.KnockDown(time);
        }

        public override void Init() {
            base.Init();
            tag = "Player";
            weapon = Weapon.Create(Data.WeaponID.단검);
            weapon.OnObtain(this);
        }

        private void Update() {
            if (_moveDir == Vector2.zero) Stop();
            else {
                if (CanAction) Move(_moveDir);
            }
        }
    }
}
