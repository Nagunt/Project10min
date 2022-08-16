using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TenMinute.Event;

namespace TenMinute {
    public class Room_Base : MonoBehaviour {
        [Header("- Base")]
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private PortalController portalController;
        public Grid Grid => grid;
        public bool IsCleared { get; protected set; }
        public PortalController PortalController => portalController;
        public virtual void Init() {
            IsCleared = false;
            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIMap¼³Á¤, this);
        }
    }
}
