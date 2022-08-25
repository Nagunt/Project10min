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
        }
        private void OnHPValueChanged(int prev, int current) {
            Debug.Log($"HP 피해 : {prev} => {current}");

            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIHP설정, current);
        }
    }
}
