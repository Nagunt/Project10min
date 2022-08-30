using System.Collections;
using System.Collections.Generic;
using TenMinute.Physics;
using TenMinute.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {
    public class EnemyAttack_BowMan : EnemyAttack {

        [SerializeField]
        private Projectile2D projectile;

        protected override IEnumerator Routine(UnityAction onComplete) {
            float unitDelay = 1 / _owner.ATKSpeed;
            WaitForSeconds waitForDelay = new WaitForSeconds(unitDelay * 0.1f);
            WaitForSeconds waitForEndDelay = new WaitForSeconds(unitDelay * 0.8f);

            SetDirection((_target.transform.position - _owner.transform.position).normalized);
            transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.right, _dir) * (_dir.y > 0 ? 1f : -1f));

            _owner.Graphic.SetMotionTime_Attack(unitDelay * .2f);
            _owner.Graphic.SetState_Attack(true);

            yield return waitForDelay;

            Projectile2D newProjectile = Instantiate(projectile);
            newProjectile.transform.position = _owner.transform.position;
            newProjectile.onCollisionEnter2D += OnHit;
            newProjectile.Fire(_dir, 2.5f, 3f);

            yield return waitForDelay;

            _owner.Graphic.SetState_Attack(false);

            yield return waitForEndDelay;

            onComplete?.Invoke();
        }

        private void OnHit(Projectile2D projectile, Collision2D col) {
            Character target = Character.GetCharacter(col.collider);
            if (target != null &&
                target.CompareTag("Player")) {
                Entity.Create(
                    source: _owner,
                    target: target).
                    Add«««ÿ(_owner.ATK).
                    Execute();
                Destroy(projectile.gameObject);
            }
        }
    }
}
