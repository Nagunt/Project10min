using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;

namespace TenMinute {
    public class Enemy : Character {

        public override void Init() {
            base.Init();
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
