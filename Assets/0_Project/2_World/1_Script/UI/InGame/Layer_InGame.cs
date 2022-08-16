using TenMinute.Event;
using UnityEngine;

namespace TenMinute.UI {
    public class Layer_InGame : Layer_Base<UI_InGame> {
        protected override void Start() {
            base.Start();
            Global_EventSystem.UI.Register<int>(UIEventID.World_InGameUIHP¼³Á¤, SetUI_HP, false);
        }

        private void SetUI_HP(int value) {
            if (_uiObject != null) {
                _uiObject.SetUI_HP(value);
            }
        }
    }
}