using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.UI {
    // 네이밍 규칙
    // (Scene 이름)_(UI이름)UI(Action)
    // 2개 이상의 Scene에서 사용될 이벤트의 경우 Global이라 지칭
    public enum UIEventID {

        World_InGameUIHP설정,
        World_InGameUIMap설정,
        World_InGameUIMap업데이트,
    }
}