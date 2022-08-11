using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.AI {
    public class AILoiter : AIState {

        private Vector2[] dir = new Vector2[] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

        public override void OnStart() {
            Debug.Log("배회 시작");
            _machine.Owner.Stop();
        }

        protected override IEnumerator Routine() {
            float range = 0f;
            Vector2 direction = Vector2.zero;
            WaitForSeconds waitForDelay = new WaitForSeconds(2f);
            while (true) {
                range = Random.Range(2f, 4f);
                direction = dir[Random.Range(0, dir.Length)];

                float deltaTime = 0f;

                while (deltaTime < range / _machine.Owner.Speed) {
                    _machine.Owner.Move(direction);
                    deltaTime += Time.deltaTime;
                    yield return null;
                }

                _machine.Owner.Stop();
                yield return waitForDelay;
            }
        }
    }
}
