using System.Collections;
using System.Collections.Generic;
using TenMinute.Physics;
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

            yield return waitForDelay;

            Projectile2D newProjectile = Instantiate(projectile);
            newProjectile.transform.position = _owner.transform.position;
            newProjectile.onCollisionEnter2D += OnHit;
            newProjectile.Fire(_dir, 2.5f, 3f);

            yield return waitForDelay;

            yield return waitForEndDelay;

            onComplete?.Invoke();
        }

        private void OnHit(Projectile2D projectile, Collision2D col) {
            Character target = Hitbox2D.GetData(col.rigidbody);
            if (target != null &&
                target.CompareTag("Player")) {
                Debug.Log("Player Hit");
                Destroy(projectile.gameObject);
            }
        }
    }
}
