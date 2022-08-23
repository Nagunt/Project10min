using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Physics;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {

    public class EnemyAttack_BrokenAutomata : EnemyAttack {

        [SerializeField]
        private Area2D _collider;
        [SerializeField]
        private GameObject _graphic;

        private Sequence attackCheckSequence;
        public override EnemyAttack Init() {
            _collider.onCollisionEnter2D += OnHit;
            _collider.gameObject.SetActive(false);
            _graphic.gameObject.SetActive(false);
            return base.Init();
        }

        protected override IEnumerator Routine(UnityAction onComplete) {
            float unitDelay = 1 / _owner.ATKSpeed;
            WaitForSeconds waitForDelay = new WaitForSeconds(unitDelay * 0.1f);
            WaitForSeconds waitForEndDelay = new WaitForSeconds(unitDelay * 0.8f);

            SetDirection((_target.transform.position - _owner.transform.position).normalized);
            transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.right, _dir) * (_dir.y > 0 ? 1f : -1f));

            _owner.Animator.SetFloat("AttackSpeed", 1 / (unitDelay * .2f));
            _owner.Animator.SetBool("IsAttack", true);

            yield return waitForDelay;

            attackCheckSequence = DOTween.Sequence();
            attackCheckSequence.
                AppendCallback(() => {
                    _collider.gameObject.SetActive(true);
                    _graphic.gameObject.SetActive(true);
                }).
                AppendInterval(0.5f).
                AppendCallback(() => {
                    _collider.gameObject.SetActive(false);
                    _graphic.gameObject.SetActive(false);
                }).
                OnKill(() => attackCheckSequence = null).
                Play();

            yield return waitForDelay;

            _owner.Animator.SetBool("IsAttack", false);

            yield return waitForEndDelay;

            onComplete?.Invoke();
        }

        private void OnHit(Collision2D col) {
            Character target = PhysicsCollider2D.GetData(col.collider);
            if (target != null &&
                target.CompareTag("Player")) {

            }
        }

        private void OnDestroy() {
            if (attackCheckSequence.IsActive()) {
                attackCheckSequence.Kill();
            }
        }
    }
}