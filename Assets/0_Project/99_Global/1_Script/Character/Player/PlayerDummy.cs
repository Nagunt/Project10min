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
                Add효과부여(Data.EffectID.효과1, 1, 5f).
                Execute();

            onHPValueChanged += OnHPValueChanged;
        }

        private void OnHPValueChanged(int prev, int current) {
            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIHP설정, current);
        }
    }
}
