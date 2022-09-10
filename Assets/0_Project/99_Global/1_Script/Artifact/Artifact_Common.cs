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
            Owner.on���� += OnDamage;
        }

        public override void OnDisable() {
            Owner.on���� -= OnDamage;
        }

        private void OnDamage(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.��üĳ���� == Owner) {
                Collider2D[] cols = Physics2D.OverlapCircleAll(entity.���ĳ����.transform.position, 5f);
                if (cols.Length > 0) {
                    for(int i = 0; i < cols.Length; ++i) {
                        if (cols[i].CompareTag("Player")) {
                            Character target = Character.GetCharacter(cols[i]);
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

    public sealed class Artifact_�˼����� : Artifact {

        public Artifact_�˼�����() :
            base(ArtifactID.�˼�����) { }

        public override void OnEnable() {

        }

        public override void OnDisable() {

        }
    }

    public sealed class Artifact_Į������ : Artifact {

        public Artifact_Į������() :
            base(ArtifactID.Į������) { }

        public override void OnEnable() {

        }

        public override void OnDisable() {

        }
    }

    public sealed class Artifact_���ݰ�ȭ : Artifact {

        public Artifact_���ݰ�ȭ() :
            base(ArtifactID.���ݰ�ȭ) { }

        public override void OnEnable() {
            Owner.onCalcATK���� += OnCalcATK����;
        }

        public override void OnDisable() {
            Owner.onCalcATK���� -= OnCalcATK����;
        }

        private float OnCalcATK����() {
            return 1.1f;
        }
    }
}