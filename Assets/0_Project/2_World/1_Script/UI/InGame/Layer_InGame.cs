using TenMinute.Event;
using UnityEngine;

namespace TenMinute.UI {
    public class Layer_InGame : Layer_Base<UI_InGame> {
        protected override void Start() {
            base.Start();
            Global_EventSystem.UI.Register<int>(UIEventID.World_InGameUIHP설정, SetUI_HP, false);
            Global_EventSystem.UI.Register<Room_Base>(UIEventID.World_InGameUIMap설정, SetUI_Map, false);
            Global_EventSystem.UI.Register<Transform, bool>(UIEventID.World_InGameUIMap업데이트, UpdateUI_Map, false);
        }

        private void SetUI_HP(int value) {
            if (_uiObject != null) {
                _uiObject.SetUI_HP(value);
            }
        }

        private void SetUI_Map(Room_Base room) {
            if (_uiObject != null) {
                _uiObject.SetUI_Map(room);
            }
        }

        private void UpdateUI_Map(Transform target, bool state) {
            if(_uiObject != null) {
                _uiObject.UpdateUI_Map(target, state);
            }
        }
    }
}