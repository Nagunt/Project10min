using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Combat;
using TenMinute.Physics;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {
    public class EnemyAttack_SwordMan : EnemyAttack {
        [SerializeField]
        private ExtraCollider2D _collider;
        [SerializeField]
        private Transform _graphic;
        [SerializeField]
        private Transform _mask;

        private Sequence moveSequence;
        private Sequence attackCheckSequence;

        public override EnemyAttack Init() {
            _collider.onTriggerEnter2D += OnHit;
            _collider.gameObject.SetActive(false);
            _graphic.gameObject.SetActive(false);
            return base.Init();
        }

        protected override IEnumerator Routine(UnityAction onComplete) {
            float unitDelay = 1 / _owner.ATKSpeed;
            WaitForSeconds waitForDelay = new WaitForSeconds(unitDelay * 0.125f);
            WaitForSeconds waitForEndDelay = new WaitForSeconds(unitDelay * 0.75f);

            SetDirection((_target.transform.position - _owner.transform.position).normalized);

            _collider.
                SetRadius(2f).
                SetEllipse(true).
                SetOtherRadius(1f).
                SetArc(true).
                SetAngle(75f).
                SetCenterAngle(Vector2.Angle(Vector2.right, _dir) * (_dir.y > 0 ? 1f : -1f)).
                MakeShape();

            _mask.transform.localEulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.right, _dir) * (_dir.y > 0 ? 1f : -1f));

            _owner.Graphic.SetMotionTime_Attack(unitDelay * .25f);
            _owner.Graphic.SetState_Attack(true);

            yield return waitForDelay;

            moveSequence = DOTween.Sequence();
            moveSequence.
                AppendCallback(() => _owner.RB2D.velocity = _dir * (1 / (unitDelay * 0.125f))).
                AppendInterval(unitDelay * 0.125f).
                AppendCallback(_owner.Stop).
                OnKill(() => moveSequence = null).
                Play();

            attackCheckSequence = DOTween.Sequence();
            attackCheckSequence.
                AppendCallback(() => {
                    _collider.gameObject.SetActive(true);
                    _graphic.gameObject.SetActive(true);
                    
                }).
                AppendInterval(unitDelay * 0.125f).
                AppendCallback(() => {
                    _collider.gameObject.SetActive(false);
                    _graphic.gameObject.SetActive(false);
                }).
                OnKill(() => attackCheckSequence = null).
                Play();


            yield return waitForDelay;

            _owner.Graphic.SetState_Attack(false);

            yield return waitForEndDelay;

            onComplete?.Invoke();
        }

        private void OnHit(ExtraCollider2D t, Collider2D col) {
            Character target = Character.GetCharacter(col);
            if (target != null &&
                target.CompareTag("Player")) {
                Entity.Create(
                    source: _owner,
                    target: target).
                    Add«««ÿ(_owner.ATK).
                    Execute();
            }
        }

        private void OnDestroy() {
            if (moveSequence.IsActive()) {
                moveSequence.Kill();
            }
            if (attackCheckSequence.IsActive()) {
                attackCheckSequence.Kill();
            }
        }
    }
}
