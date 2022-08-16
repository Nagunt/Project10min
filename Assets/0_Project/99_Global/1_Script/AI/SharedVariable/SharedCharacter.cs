using BehaviorDesigner.Runtime;
using UnityEngine;

namespace TenMinute.AI.SharedVariables {
    [System.Serializable]
    public class SharedCharacter : SharedVariable<Character>
    {
        public static implicit operator SharedCharacter(Character value) { return new SharedCharacter { mValue = value }; }
    }
}