using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute
{
    public class PlayerAttack_ShortSword : PlayerAttack
    {
        [SerializeField]
        Player Owner;
        [SerializeField]
        float FireRange;
        [SerializeField]
        float PlayerAttackAngle;
        [SerializeField]
        float _stat°æÁ÷ = 0;
        [SerializeField]
        float _stat³Ë¹é = 0;
        [SerializeField]
        float _statATKPercent = 100;
        [SerializeField]
        float _statATKSpeedPercent = 100;

        

        private void Awake()
        {
            stat°æÁ÷ = _stat°æÁ÷;
            stat³Ë¹é = _stat³Ë¹é;
            statATKPercent = _statATKPercent;
            statATKSpeedPercent = _statATKSpeedPercent;

            
        }
        override public IEnumerator PlayerFire()
        {
            while (true)
            {
                yield return new WaitUntil(() => Owner.fire == true);
                Owner.isAttack = true;
                Vector3 templook = Owner.look;


                Collider2D[] inHitBox = Physics2D.OverlapCircleAll(transform.position, FireRange);

                StartCoroutine(Owner.CreateCircle(FireRange, Mathf.Atan2(templook.y - transform.position.y, templook.x - transform.position.x) * Mathf.Rad2Deg, PlayerAttackAngle, transform.position));


                foreach (Collider2D t in inHitBox)
                {
                    float angle = Vector2.Angle(templook - transform.position, t.transform.position - transform.position);
                    if (angle < PlayerAttackAngle / 2 && t.CompareTag("Enemy"))
                    {
                        t.GetComponent<TestEnemy>().GetDamaged();
                    }



                }

                yield return new WaitForSeconds(Owner.ATKSpeed * (_statATKSpeedPercent / 100));
                Owner.isAttack = false;
            }

        }

    }

}
