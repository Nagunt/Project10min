using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace TenMinute.AI
{
    public class AttackToTarget : Action
    {
        public SharedCharacter character;
        public SharedCharacter otherCharacter;

        public override void OnStart()
        {
            character.Value.Stop();
            character.Value.Attack(otherCharacter.Value);
        }

        public override TaskStatus OnUpdate()
        {
            return character.Value.IsAttack ? TaskStatus.Running : TaskStatus.Success;
        }
    }
}
