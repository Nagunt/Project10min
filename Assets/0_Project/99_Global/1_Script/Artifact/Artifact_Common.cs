using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;
using System;
using TenMinute.Physics;
using TenMinute.Event;
using TenMinute.Combat;

namespace TenMinute {

    public sealed class Artifact_�ð����� : Artifact {
        public Artifact_�ð�����() :
            base(ArtifactID.�ð�����) { }

        public override void OnEnable() {
            base.OnEnable();
            Owner.on���� += OnDamage;
        }

        public override void OnDisable() {
            base.OnDisable();
            Owner.on���� -= OnDamage;
        }

        private void OnDamage(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.��üĳ���� == Owner) {
                Collider2D[] cols = Physics2D.OverlapCircleAll(entity.���ĳ����.transform.position, 5f);
                if (cols.Length > 0) {
                    for(int i = 0; i < cols.Length; ++i) {
                        if (cols[i].CompareTag("Player")) {
                            Character target = PhysicsCollider2D.GetData(cols[i]);
                            if (target != entity.���ĳ����) {
                                entity.Add���꿣ƼƼ(Entity.Create(
                                    id: EntityID.�ð�����,
                                    source : this,
                                    target: target).
                                    Add����(10), dataIndex);
                            }
                        }
                    }
                }
            }
        }
    }

}