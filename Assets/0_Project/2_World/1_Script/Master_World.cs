using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;

namespace TenMinute {
    public class Master_World : MonoBehaviour {

        public static bool GameState { get; private set; }

        private void Awake() {
            Global_EventSystem.Game.onGameStateChanged += OnGameStateChanged;
        }

        #region ฤÝน้

        private void OnGameStateChanged(bool state) {
            if (state != GameState) {
                GameState = state;
                Time.timeScale = GameState ? 1f : 0;
            }
        }

        #endregion

        private void OnDestroy() {
            Global_EventSystem.Game.onGameStateChanged -= OnGameStateChanged;
        }
    }
}

