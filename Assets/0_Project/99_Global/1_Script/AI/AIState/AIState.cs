using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.AI {

    public class AIState : MonoBehaviour {

        public bool IsRunning { get; protected set; }

        protected AIStateMachine _machine;
        protected Coroutine _routine;

        public virtual void Init(AIStateMachine machine) {
            _machine = machine;
        }

        public virtual void OnStart() {

        }

        public void Execute() {
            IsRunning = true;
            _routine = StartCoroutine(Routine());
        }

        protected virtual IEnumerator Routine() {
            IsRunning = false;
            yield break;
        }

        public virtual void OnExit() {
            if (IsRunning) {
                StopCoroutine(_routine);
                IsRunning = false;
            }
        }
    }
}
