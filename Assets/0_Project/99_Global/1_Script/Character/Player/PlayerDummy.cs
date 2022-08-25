using System;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Combat;
using TenMinute.Event;
using UnityEngine;

namespace TenMinute {
    public class PlayerDummy : Character {
        public override void Init() {
            base.Init();
            onHPValueChanged += OnHPValueChanged;
            Global_EventSystem.Combat.on������������ += On������������;
            Global_EventSystem.Combat.on�������� += On��������;

        }

        private void On������������(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.���ĳ���� == this) {
                DataEntity data = entity.GetData(dataIndex);
                data.Add���(2f);
                Debug.Log(data.������);
            }
        }

        private void On��������(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.���ĳ���� == this) {
                Debug.Log($"���� ���� : {entity.GetData(dataIndex).�����ط�}");
                entity.Add���꿣ƼƼ(Entity.Create(
                    target: this).
                    Add���ð�������((int)(entity.GetData(dataIndex).�����ط� * .5f)), dataIndex);
            }
        }

        private void OnHPValueChanged(int prev, int current) {
            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIHP����, current);
        }
    }
}
