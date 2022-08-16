using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileHitbox2D : MonoBehaviour {
        [SerializeField]
        private Projectile2D projectile;

        private void OnCollisionEnter2D(Collision2D collision) {
            projectile.onCollisionEnter2D?.Invoke(projectile, collision);
        }

        private void OnCollisionStay2D(Collision2D collision) {
            projectile.onCollisionStay2D?.Invoke(projectile, collision);
        }

        private void OnCollisionExit2D(Collision2D collision) {
            projectile.onCollisionExit2D?.Invoke(projectile, collision);
        }
    }
}
