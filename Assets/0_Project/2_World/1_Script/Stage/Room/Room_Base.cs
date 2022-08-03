using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TenMinute.Event;

namespace TenMinute {
    public class Room_Base : MonoBehaviour {
        [Header("- Base")]
        [SerializeField]
        private Transform area;
        [SerializeField]
        private Tilemap tilemap_Floor;
        [SerializeField]
        private Tilemap tilemap_Block;
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private PortalController portalController;
        public Grid Grid => grid;
        public bool IsCleared { get; protected set; }
        public Rect Area { get; protected set; }
        public Tilemap Tilemap_Floor => tilemap_Floor;
        public Tilemap Tilemap_Block => tilemap_Block;
        public PortalController PortalController => portalController;
        public virtual void Init() {
            IsCleared = false;
            Area = new Rect(area.localPosition - area.localScale * .5f, area.localScale);
            Global_EventSystem.UI.Call(UI.UIEventID.World_InGameUIMap¼³Á¤, this);
        }
    }
}
