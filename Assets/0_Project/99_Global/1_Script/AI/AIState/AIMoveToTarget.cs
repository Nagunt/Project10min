using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.AI {
    public class AIMoveToTarget : AIState {
        [SerializeField]
        private float minDistance = 0.01f;

        public override void OnStart() {
            Debug.Log("추격 시작");
            _machine.Owner.Stop();
        }
        protected override IEnumerator Routine() {
            Transform target = null;
            while (true) {
                if (_machine.Target != null) {
                    target = _machine.Target;
                }
                while (target != null) {
                    if (Mathf.Abs(target.position.x - _machine.Owner.transform.position.x) <= minDistance &&
                        Mathf.Abs(target.position.y - _machine.Owner.transform.position.y) <= minDistance) {
                        
                    }
                    else {
                        Vector2 direction = (target.position - _machine.Owner.transform.position).normalized;
                        _machine.Owner.Move(direction);
                    }
                    yield return null;
                }
                yield return null;
            }
        }
    }
}
