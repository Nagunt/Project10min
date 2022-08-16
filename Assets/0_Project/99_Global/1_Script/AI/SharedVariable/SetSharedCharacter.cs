using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace TenMinute.AI.SharedVariables {
    [TaskCategory("Unity/SharedVariable")]
    [TaskDescription("Sets the SharedCharacter variable to the specified object. Returns Success.")]
    public class SetSharedCharacter : Action {
        [Tooltip("The value to set the SharedCharacter to")]
        public SharedCharacter targetValue;
        [RequiredField]
        [Tooltip("The SharedCharacter to set")]
        public SharedCharacter targetVariable;
        [Tooltip("Can the target value be null?")]
        public SharedBool valueCanBeNull;

        public override TaskStatus OnUpdate() {
            targetVariable.Value = ((targetValue.Value != null || valueCanBeNull.Value) ? targetValue.Value : targetVariable.Value);

            return TaskStatus.Success;
        }

        public override void OnReset() {
            valueCanBeNull = false;
            targetValue = null;
            targetVariable = null;
        }
    }
}