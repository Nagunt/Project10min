using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TenMinute.Physics;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using TenMinute.AI.SharedVariables;

namespace TenMinute.AI
{
    [TaskCategory("Physics")]
    public class HasEnteredTrigger2DAdvanced : Conditional
    {
        [Tooltip("面倒备开")]
        public SharedCollider2D area;
        [Tooltip("面倒炼扒")]
        public ContactFilter2D filter;
        [Tooltip("The tag of the GameObject to check for a trigger against")]
        public SharedString tag = "";
        [Tooltip("The object that entered the trigger")]
        public SharedCharacter otherCharacter;

        private List<Collider2D> resultContainer = new List<Collider2D>();

        public override TaskStatus OnUpdate()
        {
            resultContainer.Clear();
            if (area.Value.OverlapCollider(filter, resultContainer) > 0)
            {
                resultContainer = resultContainer.Where(c => c.CompareTag(tag.Value)).ToList();
                if (resultContainer.Count > 0)
                {
                    otherCharacter.SetValue(Character.GetCharacter(resultContainer[0]));
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            tag = "";
            area = null;
            filter = default;
            otherCharacter = null;
        }
    }
}
