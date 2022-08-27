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
            Owner.on피해 += OnDamage;
        }

        public override void OnDisable() {
            base.OnDisable();
            Owner.on피해 -= OnDamage;
        }

        private void OnDamage(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.주체캐릭터 == Owner) {
                Collider2D[] cols = Physics2D.OverlapCircleAll(entity.대상캐릭터.transform.position, 5f);
                if (cols.Length > 0) {
                    for(int i = 0; i < cols.Length; ++i) {
                        if (cols[i].CompareTag("Player")) {
                            Character target = PhysicsCollider2D.GetData(cols[i]);
                            if (target != entity.대상캐릭터) {
                                entity.Add서브엔티티(Entity.Create(
                                    id: EntityID.시간파편,
                                    source : this,
                                    target: target).
                                    Add피해(10), dataIndex);
                            }
                        }
                    }
                }
            }
        }
    }

}