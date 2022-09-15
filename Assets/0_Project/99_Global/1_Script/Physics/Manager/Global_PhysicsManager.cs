using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics {
    public class Global_PhysicsManager : MonoBehaviour {
        public static Global_PhysicsManager Instance { get; private set; } = null;

        [SerializeField]
        private ExtraCollider2D _exCol2D;

        private void Init() {
            Instance = this;
            
        }

        private void Awake() {
            if (Instance == null) {
                Init();
            }
        }

    }
}