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
        private GameObject portal;
        public Grid Grid => grid;

        public bool IsCleared { get; protected set; }
        public virtual void Init() {
            IsCleared = false;
            portal.SetActive(false);
            Global_EventSystem.Game.onRoomCleared += OnRoomCleared;
        }

        protected virtual void OnRoomCleared(int index) {
            portal.SetActive(true);
        }
        private void OnDestroy() {
            Global_EventSystem.Game.onRoomCleared -= OnRoomCleared;
        }
    }
}
