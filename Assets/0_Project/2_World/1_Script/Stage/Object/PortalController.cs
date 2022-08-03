using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {
    public class PortalController : MonoBehaviour {
        [SerializeField]
        private Portal portal;
        [SerializeField]
        private Transform[] pos;
        
        public void MakePortal(params int[] index) {
            for(int i = 0; i < index.Length; ++i) {
                if (0 <= i && i < pos.Length) {
                    Portal newPortal = Instantiate(portal, pos[i]);
                    newPortal.Init(index[i]);
                }
            }
        }
    }
}
