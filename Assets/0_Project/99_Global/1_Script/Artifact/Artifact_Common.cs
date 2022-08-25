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
            Global_EventSystem.Combat.on�������� += OnDamage;
        }

        public override void OnDisable() {
            base.OnDisable();
            Global_EventSystem.Combat.on�������� -= OnDamage;
        }

        private void OnDamage(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.��üĳ���� == Owner) {
                entity.Add���꿣ƼƼ(Entity.Create(
                    source: this,
                    target: entity.���ĳ����).
                    Add����(10), dataIndex);
            }
        }
    }

}