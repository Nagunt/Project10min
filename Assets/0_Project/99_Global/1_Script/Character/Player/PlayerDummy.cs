using System.Collections;
using System.Collections.Generic;
using TenMinute.Event;
using UnityEngine;

namespace TenMinute {
    public class PlayerDummy : Character {
        public override void Init() {
            base.Init();

            Entity.Create(
                source : null, 
                target : this).
                Addȿ���ο�(Data.EffectID.ȿ��1, 1, 5f).
                Execute();

            onHPValueChanged += OnHPValueChanged;
        }

        private void OnHPValueChanged(int prev, int current) {
            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIHP����, current);
        }
    }
}
