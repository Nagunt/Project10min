using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;
using System;
using TenMinute.Physics;

namespace TenMinute {

    public sealed class Artifact_�ð����� : Artifact {
        public Artifact_�ð�����() :
            base(ArtifactID.�ð�����) { }

        public override void OnEnable() {
            Owner.on���� += On����;
        }

        public override void OnDisable() {
            Owner.on���� -= On����;
        }

        private void On����(DataEntity entity) {
            if (entity.��ü == Owner) {
                int targetCount = ArtifactValues[0] + Value * ArtifactValues[1];
                Collider2D[] cols = Physics2D.OverlapCircleAll(entity.���.transform.position, 5);
                foreach (Collider2D col in cols) {
                    Character target = PhysicsCollider2D.GetData(col);
                    if (target != null) {
                        entity.Add������ƼƼ(Entity.Create(
                            source: entity.��ü,
                            target: target).
                            Add�߰�����(ArtifactValues[2] + ((Value / 3) * ArtifactValues[3])));
                        break;
                    }
                }
            }
        }
    }

}