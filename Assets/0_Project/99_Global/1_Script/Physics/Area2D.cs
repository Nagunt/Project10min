using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics
{
    public class Area2D : MonoBehaviour
    {
        public Action<Collision2D> onCollisionEnter2D;
        public Action<Collision2D> onCollisionStay2D;
        public Action<Collision2D> onCollisionExit2D;

        private void OnCollisionEnter2D(Collision2D collision) {
            onCollisionEnter2D?.Invoke(collision);
        }

        private void OnCollisionStay2D(Collision2D collision) {
            onCollisionStay2D?.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision) {
            onCollisionExit2D?.Invoke(collision);
        }
    }
}
