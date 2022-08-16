using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {
    public class EnemyAttack : MonoBehaviour {
        [SerializeField]
        protected Character _owner;
        protected Character _target;
        protected Vector3 _pos;
        protected Vector3 _dir;

        public EnemyAttack SetTarget(Character target) {
            _target = target;
            return this;
        }

        public EnemyAttack SetPosition(Vector3 pos) {
            _pos = pos;
            return this;
        }

        public EnemyAttack SetDirection(Vector3 dir) {
            _dir = dir;
            return this;
        }

        public virtual EnemyAttack Init() {
            return this;
        }

        public void Run(UnityAction onComplete) {
            StartCoroutine(Routine(onComplete));
        }

        protected virtual IEnumerator Routine(UnityAction onComplete) {
            onComplete?.Invoke();
            yield break;
        }
    }
}
