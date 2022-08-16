using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {
    public class Room_Battle : Room_Base {

        public override void Init() {
            base.Init();
            StartCoroutine(BattleRoutine());
        }

        IEnumerator BattleRoutine() {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Master_World.EnemyCount <= 0);
            IsCleared = true;
        }
    }
}


