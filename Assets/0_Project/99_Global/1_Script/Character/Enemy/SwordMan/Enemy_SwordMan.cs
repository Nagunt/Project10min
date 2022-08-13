using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TenMinute {
    public class Enemy_SwordMan : Enemy {

        [SerializeField]
        private GameObject attack;

        public override void Init() {
            base.Init();
            ����HP = 10;
            _HP = 10;
            ����ATK = 10;
            ����DEF = 1;
            ����Speed = 2;
            ����ATKSpeed = 0.25f;
        }

        // �� ���� ����� �ð��� 1 / ATKSpeed
        // ex) ATKSpeed�� 4 => �� ���� ����� �ð��� 0.25
        // ex) ATKSpeed�� 0.25 => �� ���� ����� �ð��� 4
        // SwordMan�� ���� ����� ���� : �������� 0.5�� ���� 0.5�� �ĵ����� 3��
        // �̸� �� ���� ����� ������ �ٲٸ� �������� 1/8 ���� 1/8 �ĵ����� 6/8

        public override void Attack(Character target)
        {
            base.Attack(target);
            StartCoroutine(Routine());

            IEnumerator Routine()
            {
                Vector2 direction = (target.transform.position - transform.position).normalized;
                float angle = Vector2.Angle(Vector2.up, direction) + (direction.x > 0 ? 90f : 0f) + (direction.x > 0 && direction.y > 0 ? 180f : 0f);
                attack.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                Debug.Log($"�������� {(1 / ATKSpeed) * 0.125f}");
                float deltaTime = 0;
                while (deltaTime < (1 / ATKSpeed) * 0.125f) // 1/8
                {
                    deltaTime += Time.deltaTime;
                    yield return null;
                }

                attack.SetActive(true);
                Sequence attackSequence = DOTween.Sequence();
                attackSequence.
                    AppendInterval(0.5f).
                    AppendCallback(() => attack.SetActive(false)).
                    Play();

                Debug.Log($"���ݸ�� {(1 / ATKSpeed) * 0.125f}");
                deltaTime = 0;
                while (deltaTime < (1 / ATKSpeed) * 0.125f) // 1/8
                {
                    deltaTime += Time.deltaTime;
                    yield return null;
                }

                Debug.Log($"�ĵ����� {(1 / ATKSpeed) * 0.75f}");
                deltaTime = 0;
                while (deltaTime < (1 / ATKSpeed) * 0.75f) // 6/8
                {
                    deltaTime += Time.deltaTime;
                    yield return null;
                }
                Debug.Log("��� ��");
                IsAttack = false;
            }
        }

    }
}
