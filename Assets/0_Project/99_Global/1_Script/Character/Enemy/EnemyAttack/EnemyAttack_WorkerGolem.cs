using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Physics;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {
    public class EnemyAttack_WorkerGolem : EnemyAttack {
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
            WaitForSeconds waitForDelay = new WaitForSeconds(unitDelay * 0.2f);
            WaitForSeconds waitForEndDelay = new WaitForSeconds(unitDelay * 0.6f);

            SetPosition(_target.transform.position);

            _owner.Animator.SetFloat("AttackSpeed", 1 / (unitDelay * .4f));
            _owner.Animator.SetBool("IsAttack", true);

            yield return waitForDelay;

            attackCheckSequence = DOTween.Sequence();
            attackCheckSequence.
                AppendCallback(() => {
                    _collider.transform.position = _pos;
                    _graphic.transform.position = _pos;
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
