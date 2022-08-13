using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace TenMinute.AI
{
    public class Attack : Action
    {
        public SharedCharacter character;
        public SharedGameObject target;

        public override void OnStart()
        {
            character.Value.Stop();
            character.Value.Attack(target.Value.GetComponentInParent<Character>());
        }

        public override TaskStatus OnUpdate()
        {
            return character.Value.IsAttack ? TaskStatus.Running : TaskStatus.Success;
        }
    }
}
