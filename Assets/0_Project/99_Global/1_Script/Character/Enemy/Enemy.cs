using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.AI;
using TenMinute.Event;
using BehaviorDesigner.Runtime;
using UnityEngine.Events;

namespace TenMinute {
    public class Enemy : Character {

        [SerializeField]
        private EnemyAttack enemyAttack;
        public override void Init() {
            base.Init();
            tag = "Enemy";
            enemyAttack.Init();
            Global_EventSystem.Game.CallOnEnemySpawned(this);
        }

        public override void AttackToTarget(Character target, UnityAction onComplete) {
            enemyAttack.
                SetTarget(target).
                Run(onComplete);
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
