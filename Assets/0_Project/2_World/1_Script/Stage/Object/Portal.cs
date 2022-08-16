using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;

namespace TenMinute {
    public class Portal : MonoBehaviour {

        private int _index = 0;
        private bool isInit = false;
        public void Init(int index) {
            isInit = true;
            _index = index;
        }
        private void OnTriggerEnter2D(Collider2D collision) {
            if (isInit) {
                if (collision.CompareTag("Player")) {
                    Global_EventSystem.Game.CallOnPortalArrived(_index);
                }
            }
        }
    }
}

