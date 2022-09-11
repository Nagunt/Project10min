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
            Owner.on피해 += OnDamage;
        }

        public override void OnDisable() {
            Owner.on피해 -= OnDamage;
        }

        private void OnDamage(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.주체캐릭터 == Owner) {
                Collider2D[] cols = Physics2D.OverlapCircleAll(entity.대상캐릭터.transform.position, 5f);
                if (cols.Length > 0) {
                    for(int i = 0; i < cols.Length; ++i) {
                        if (cols[i].CompareTag("Player")) {
                            Character target = Character.GetCharacter(cols[i]);
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

    public sealed class Artifact_검술연마 : Artifact {

        public Artifact_검술연마() :
            base(ArtifactID.검술연마) { }

        public override void OnEnable() {

        }

        public override void OnDisable() {

        }
    }

    public sealed class Artifact_칼날돌진 : Artifact {

        public Artifact_칼날돌진() :
            base(ArtifactID.칼날돌진) { }

        public override void OnEnable() {

        }

        public override void OnDisable() {

        }
    }

    public sealed class Artifact_공격강화 : Artifact {

        public Artifact_공격강화() :
            base(ArtifactID.공격강화) { }

        public override void OnEnable() {
            Owner.onCalcATK비율 += OnCalcATK배율;
        }

        public override void OnDisable() {
            Owner.onCalcATK비율 -= OnCalcATK배율;
        }

        private float OnCalcATK배율() {
            return 1.1f;
        }
    }
}