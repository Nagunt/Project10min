using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics
{
    [RequireComponent(typeof(Collider2D))]
    public class PhysicsCollider2D : MonoBehaviour
    {
        private static Dictionary<Collider2D, Character> colData = new Dictionary<Collider2D, Character>();

        public static Character GetData(Collider2D key) {
            if (colData == null) {
                colData = new Dictionary<Collider2D, Character>();
            }
            if (colData.TryGetValue(key, out Character value)) {
                return value;
            }
            return null;
        }

        public static void Clear()
        {
            colData.Clear();
        }

        [SerializeField]
        private Collider2D col;
        [SerializeField]
        private Character character;

        private void Awake()
        {
            colData.Add(col, character);
        }

        private void OnDestroy()
        {
            colData.Remove(col);
        }
    }
}