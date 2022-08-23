using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;
using System;
using TenMinute.Physics;

namespace TenMinute {

    public sealed class Artifact_시간파편 : Artifact {
        public Artifact_시간파편() :
            base(ArtifactID.시간파편) { }

        public override void OnEnable() {
            Owner.on피해 += On피해;
        }

        public override void OnDisable() {
            Owner.on피해 -= On피해;
        }

        private void On피해(DataEntity entity) {
            if (entity.주체 == Owner) {
                int targetCount = ArtifactValues[0] + Value * ArtifactValues[1];
                Collider2D[] cols = Physics2D.OverlapCircleAll(entity.대상.transform.position, 5);
                foreach (Collider2D col in cols) {
                    Character target = PhysicsCollider2D.GetData(col);
                    if (target != null) {
                        entity.Add하위엔티티(Entity.Create(
                            source: entity.주체,
                            target: target).
                            Add추가피해(ArtifactValues[2] + ((Value / 3) * ArtifactValues[3])));
                        break;
                    }
                }
            }
        }
    }

}