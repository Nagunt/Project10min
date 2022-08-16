using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Physics;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {
    public class EnemyAttack_SwordMan : EnemyAttack {
        [SerializeField]
        private Area2D _collider;
        [SerializeField]
        private GameObject _graphic;

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
            transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.right, _dir) * (_dir.y > 0 ? 1f : -1f));

            _owner.Animator.SetBool("IsAttack", true);
            _owner.Animator.SetInteger("AttackPhase", 0);
            _owner.Animator.SetFloat("MotionTime", 1 / (unitDelay * 0.125f));

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
                AppendInterval(0.5f).
                AppendCallback(() => {
                    _collider.gameObject.SetActive(false);
                    _graphic.gameObject.SetActive(false);
                }).
                OnKill(() => attackCheckSequence = null).
                Play();

            _owner.Animator.SetInteger("AttackPhase", 1);
            _owner.Animator.SetFloat("MotionTime", 1 / (unitDelay * 0.125f));

            yield return waitForDelay;

            _owner.Animator.SetInteger("AttackPhase", 2);
            _owner.Animator.SetFloat("MotionTime", 1 / (unitDelay * 0.75f));

            yield return waitForEndDelay;

            _owner.Animator.SetBool("IsAttack", false);
            _owner.Animator.SetInteger("AttackPhase", 0);

            onComplete?.Invoke();
        }

        private void OnHit(Collider2D col) {
            Character target = Hitbox2D.GetData(col.attachedRigidbody);
            if (target != null &&
                target.CompareTag("Player")) {
                Debug.Log("Player Hit");
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