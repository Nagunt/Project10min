using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TenMinute.Physics;

namespace TenMinute {
    public class Enemy_SwordMan : Enemy {

        [SerializeField]
        private Area2D attack;

        public override void Init() {
            base.Init();
            스텟HP = 10;
            _HP = 10;
            스텟ATK = 10;
            스텟DEF = 1;
            스텟Speed = 2;
            스텟ATKSpeed = 0.25f;
        }

        // 총 공격 모션의 시간은 1 / ATKSpeed
        // ex) ATKSpeed가 4 => 총 공격 모션의 시간은 0.25
        // ex) ATKSpeed가 0.25 => 총 공격 모션의 시간은 4
        // SwordMan의 공격 모션의 구성 : 선딜레이 0.5초 공격 0.5초 후딜레이 3초
        // 이를 총 공격 모션의 비율로 바꾸면 선딜레이 1/8 공격 1/8 후딜레이 6/8

        public override void Attack(Character target)
        {
            base.Attack(target);
            StartCoroutine(Routine());

            IEnumerator Routine()
            {
                Vector2 direction = (target.transform.position - transform.position).normalized;
                float angle = Vector2.Angle(Vector2.up, direction) + (direction.x > 0 ? 90f : 0f) + (direction.x > 0 && direction.y > 0 ? 180f : 0f);
                attack.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                Debug.Log($"선딜레이 {(1 / ATKSpeed) * 0.125f}");
                float deltaTime = 0;
                while (deltaTime < (1 / ATKSpeed) * 0.125f) // 1/8
                {
                    deltaTime += Time.deltaTime;
                    yield return null;
                }

                attack.SetActive(true);
                attack.onTriggerEnter2D += OnHit;
                Sequence attackSequence = DOTween.Sequence();
                attackSequence.
                    AppendInterval(0.5f).
                    AppendCallback(() => {
                        attack.SetActive(false);
                        attack.onTriggerEnter2D -= OnHit;
                        }).
                    Play();

                Sequence moveSequence = DOTween.Sequence();
                moveSequence.
                    AppendCallback(() => rb2D.velocity = direction * (1 / ((1 / ATKSpeed) * 0.125f))).
                    AppendInterval((1 / ATKSpeed) * 0.125f).
                    AppendCallback(Stop).
                    Play();

                Debug.Log($"공격모션 {(1 / ATKSpeed) * 0.125f}");
                deltaTime = 0;
                while (deltaTime < (1 / ATKSpeed) * 0.125f) // 1/8
                {
                    deltaTime += Time.deltaTime;
                    yield return null;
                }

                Debug.Log($"후딜레이 {(1 / ATKSpeed) * 0.75f}");
                deltaTime = 0;
                while (deltaTime < (1 / ATKSpeed) * 0.75f) // 6/8
                {
                    deltaTime += Time.deltaTime;
                    yield return null;
                }
                Debug.Log("모션 끝");
                IsAttack = false;
            }
        }


        private void OnHit(Collider2D col)
        {
            Debug.Log(col.name);
            Character target = Hitbox2D.GetData(col.attachedRigidbody);
            if (target != null)
            {
                Debug.Log($"target : {target.name}");
            }
        }
    }
}
