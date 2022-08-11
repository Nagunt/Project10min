using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.AI {
    public class AICondition : MonoBehaviour {
        [field:SerializeField]
        public string Key { get; private set; }
        protected AIStateMachine _machine;
        public virtual void Init(AIStateMachine machine) {
            _machine = machine;
        }
        public virtual bool Check() {
            return true;
        }
    }
}
