using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics {
    public class Projectile2D : MonoBehaviour {
        [SerializeField]
        Rigidbody2D rb2D;
        Sequence killSequence;

        public Action<Projectile2D, Collision2D> onCollisionEnter2D;
        public Action<Projectile2D, Collision2D> onCollisionStay2D;
        public Action<Projectile2D, Collision2D> onCollisionExit2D;

        public void Fire(Vector3 dir, float speed, float duration = 0f) {
            float angle = Vector3.Angle(Vector3.right, dir.normalized) * (dir.y > 0 ? 1f : -1f);
            transform.eulerAngles = new Vector3(0, 0, angle);
            rb2D.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
            if (duration > 0f) {
                killSequence = DOTween.Sequence();
                killSequence.
                    AppendInterval(duration).
                    AppendCallback(() => Destroy(gameObject)).
                    Play();
            }
        }

        private void OnDestroy() {
            if (killSequence.IsActive()) {
                killSequence.Kill();
            }
        }

    }
}
