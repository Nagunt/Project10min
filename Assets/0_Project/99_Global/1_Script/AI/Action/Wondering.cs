using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TenMinute.AI.SharedVariables;
using UnityEngine;

namespace TenMinute.AI
{
    public class Wondering : Action {
        private Vector2[] dir = new Vector2[] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
        private float range;
        private Vector2 direction;
        private float deltaTime = 0f;

        public SharedCharacter character;

        public override void OnStart()
        {
            character.Value.Stop();
            range = Random.Range(2f, 4f);
            direction = dir[Random.Range(0, dir.Length)];
            deltaTime = 0f;
        }

        public override TaskStatus OnUpdate()
        {
            if(deltaTime < range / character.Value.Speed)
            {
                character.Value.Move(direction);
                deltaTime += Time.deltaTime;
                return TaskStatus.Running;
            }
            character.Value.Stop();
            return TaskStatus.Success;
        }

    }
}
