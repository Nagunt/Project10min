using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

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
        public SharedGameObject otherGameObject;

        public override TaskStatus OnUpdate()
        {
            List<Collider2D> results = new List<Collider2D>();
            if (area.Value.OverlapCollider(filter, results) > 0)
            {
                results = results.Where(c => c.CompareTag(tag.Value)).ToList();
                if (results.Count > 0)
                {
                    otherGameObject.SetValue(results[0].gameObject);
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
            otherGameObject = null;
        }
    }
}
