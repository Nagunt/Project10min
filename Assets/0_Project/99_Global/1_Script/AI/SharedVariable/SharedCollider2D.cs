using BehaviorDesigner.Runtime;
using UnityEngine;

namespace TenMinute.AI
{
    [System.Serializable]
    public class SharedCollider2D : SharedVariable<Collider2D>
    {
        public static implicit operator SharedCollider2D(Collider2D value) { return new SharedCollider2D { mValue = value }; }
    }
}