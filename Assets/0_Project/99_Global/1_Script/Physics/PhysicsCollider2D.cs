using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics
{
    [RequireComponent(typeof(Collider2D))]
    public class PhysicsCollider2D : MonoBehaviour
    {
        private static Dictionary<Collider2D, Character> data = new Dictionary<Collider2D, Character>();

        public static Character GetData(Collider2D key)
        {
            if (data == null)
            {
                data = new Dictionary<Collider2D, Character>();
            }
            if (data.TryGetValue(key, out Character value))
            {
                return value;
            }
            return null;
        }

        public static void Clear()
        {
            data.Clear();
        }

        [SerializeField]
        private Collider2D col;
        [SerializeField]
        private Character character;

        private void Awake()
        {
            data.Add(col, character);
        }

        private void OnDestroy()
        {
            data.Remove(col);
        }
    }
}