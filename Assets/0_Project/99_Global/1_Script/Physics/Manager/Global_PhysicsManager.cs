using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics {
    public class Global_PhysicsManager : MonoBehaviour {
        public static Global_PhysicsManager Instance { get; private set; } = null;

        [SerializeField]
        private ExtraCollider2D _exCol2D;

        public ExtraCollider2D Col2D => _exCol2D;

        private void Init() {
            Instance = this;
            Col2D.gameObject.SetActive(false);
        }

        private void Awake() {
            if (Instance == null) {
                Init();
            }
        }
        public int OverlapExtraCol2D(ContactFilter2D filter, List<Collider2D> col) {
            Col2D.gameObject.SetActive(true);
            int data = Physics2D.OverlapCollider(Col2D.Col2D, filter, col);
            Col2D.gameObject.SetActive(false);
            return data;
        }

    }
}