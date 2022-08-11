using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.AI;
using TenMinute.Event;

namespace TenMinute {
    public class Enemy : Character {

        [Header("- AI")]
        [SerializeField]
        protected AIStateMachine machine;

        public override void Init() {
            base.Init();
            machine.Init(this);
            Global_EventSystem.Game.CallOnEnemySpawned(this);
        }

        public override void Dead() {
            base.Dead();
            Global_EventSystem.Game.CallOnEnemyDead(this);
        }

        public override void Dispose() {
            base.Dispose();
            Global_EventSystem.Game.CallOnEnemyDisposed(this);
        }
    }
}
