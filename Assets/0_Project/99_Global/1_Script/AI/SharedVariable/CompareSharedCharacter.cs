using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace TenMinute.AI.SharedVariables {
    [TaskCategory("Unity/SharedVariable")]
    [TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
    public class CompareSharedCharacter : Conditional {
        [Tooltip("The first variable to compare")]
        public SharedCharacter variable;
        [Tooltip("The variable to compare to")]
        public SharedCharacter compareTo;

        public override TaskStatus OnUpdate() {
            if (variable.Value == null && compareTo.Value != null)
                return TaskStatus.Failure;
            if (variable.Value == null && compareTo.Value == null)
                return TaskStatus.Success;

            return variable.Value.Equals(compareTo.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset() {
            variable = null;
            compareTo = null;
        }
    }
}