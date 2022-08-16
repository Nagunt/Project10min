using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TenMinute.AI.SharedVariables;
using UnityEngine;

namespace TenMinute.AI
{
    public class AttackToTarget : Action {
        public SharedCharacter character;
        public SharedCharacter otherCharacter;

        private bool isComplete = false;

        public override void OnStart()
        {
            isComplete = false;
            character.Value.AttackToTarget(
                otherCharacter.Value,
                () => isComplete = true
                );
        }

        public override TaskStatus OnUpdate()
        {
            return isComplete ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}
