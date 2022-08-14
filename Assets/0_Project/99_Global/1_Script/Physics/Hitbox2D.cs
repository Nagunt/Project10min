using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hitbox2D : MonoBehaviour
    {
        private static Dictionary<Rigidbody2D, Character> data = new Dictionary<Rigidbody2D, Character>();

        public static Character GetData(Rigidbody2D key)
        {
            if (data == null)
            {
                data = new Dictionary<Rigidbody2D, Character>();
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
        private Rigidbody2D rb2D;
        [SerializeField]
        private Character character;

        private void Awake()
        {
            data.Add(rb2D, character);
        }

        private void OnDestroy()
        {
            data.Remove(rb2D);
        }
    }
}