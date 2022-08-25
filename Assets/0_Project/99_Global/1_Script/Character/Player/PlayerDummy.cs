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
            Global_EventSystem.Combat.on피해입을예정 += On피해입을예정;
            Global_EventSystem.Combat.on피해입음 += On피해입음;

        }

        private void On피해입을예정(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.대상캐릭터 == this) {
                DataEntity data = entity.GetData(dataIndex);
                data.Add배수(2f);
                Debug.Log(data.데이터);
            }
        }

        private void On피해입음(Entity entity, int dataIndex) {
            if (entity.IsRoot &&
                entity.대상캐릭터 == this) {
                Debug.Log($"받은 피해 : {entity.GetData(dataIndex).총피해량}");
                entity.Add서브엔티티(Entity.Create(
                    target: this).
                    Add방어무시고정피해((int)(entity.GetData(dataIndex).총피해량 * .5f)), dataIndex);
            }
        }

        private void OnHPValueChanged(int prev, int current) {
            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIHP설정, current);
        }
    }
}
