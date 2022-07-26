using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;

namespace TenMinute {
    public class Portal : MonoBehaviour {
        [SerializeField]
        private int portalIndex;
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                Global_EventSystem.Game.CallOnPortalArrived(portalIndex);
            }
        }
    }
}

