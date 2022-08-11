using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TenMinute.AI {
    public class AICheckObjectInArea : AICondition {
        [SerializeField]
        private Collider2D area;
        [SerializeField]
        private ContactFilter2D filter;
        [SerializeField]
        private string targetTag;

        public override bool Check() {
            List<Collider2D> results = new List<Collider2D>();
            if(area.OverlapCollider(filter, results) > 0) {
                results = results.Where(c => c.CompareTag(targetTag)).ToList();
                if (results.Count > 0) {
                    _machine.SetTarget(results[0].transform);
                    return true;
                }
            }
            return false;
        }
    }
}
