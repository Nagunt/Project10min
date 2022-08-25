using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;
using System;
using TenMinute.Physics;
using TenMinute.Event;
using TenMinute.Combat;

namespace TenMinute {

    public sealed class Artifact_시간파편 : Artifact {
        public Artifact_시간파편() :
            base(ArtifactID.시간파편) { }

        public override void OnEnable() {
            base.OnEnable();
            Global_EventSystem.Combat.on피해입음 += OnDamage;
        }

        public override void OnDisable() {
            base.OnDisable();
            Global_EventSystem.Combat.on피해입음 -= OnDamage;
        }

        private void OnDamage(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.주체캐릭터 == Owner) {
                entity.Add서브엔티티(Entity.Create(
                    source: this,
                    target: entity.대상캐릭터).
                    Add피해(10), dataIndex);
            }
        }
    }

}