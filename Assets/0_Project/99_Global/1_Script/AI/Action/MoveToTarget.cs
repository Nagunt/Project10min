using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TenMinute.AI.SharedVariables;
using UnityEngine;

namespace TenMinute.AI
{
    public class MoveToTarget : Action {
        public SharedCharacter character;
        public SharedCharacter otherCharacter;
        public float minDistance = 0.01f;

        public override TaskStatus OnUpdate()
        {
            if (Mathf.Abs(otherCharacter.Value.transform.position.x - character.Value.transform.position.x) <= minDistance &&
                Mathf.Abs(otherCharacter.Value.transform.position.y - character.Value.transform.position.y) <= minDistance)
            {
                character.Value.Stop();
                return TaskStatus.Success;
            }
            else
            {
                Vector2 direction = (otherCharacter.Value.transform.position - character.Value.transform.position).normalized;
                character.Value.Move(direction);
            }
            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            character.Value.Stop();
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            otherCharacter = null;
            character = null;
            minDistance = 0.01f;
        }
    }
}