using BehaviorDesigner.Runtime.Tasks;
using TenMinute.AI.SharedVariables;

namespace TenMinute.AI {
    [TaskCategory("Unity/Character")]

    public class IsKnockDown : Conditional {
        public SharedCharacter targetCharacter;

        public override TaskStatus OnUpdate() {
            return targetCharacter.Value.IsKnockDown ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset() {
            targetCharacter = null;
        }
    }
}
