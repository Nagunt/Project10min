using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;

namespace TenMinute {
    public class Enemy : Character {

        public override void Init() {
            base.Init();
            Debug.Log("적 Init");
            Global_EventSystem.Game.CallOnEnemySpawned(this);
            StartCoroutine(UpdateRoutine());
        }

        public override void Dead() {
            base.Dead();
            Global_EventSystem.Game.CallOnEnemyDead(this);
        }

        public override void Dispose() {
            base.Dispose();
            Global_EventSystem.Game.CallOnEnemyDisposed(this);
        }

        private IEnumerator UpdateRoutine() {
            WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
            while (IsAlive) {
                Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIMap업데이트, transform, true);
                yield return waitForFixedUpdate;
            }
            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIMap업데이트, transform, false);
        }
    }
}
