using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics
{
    public class Arc2D : MonoBehaviour
    {
        [SerializeField] private bool _debugMode = false;
        [Range(0f, 360f)]
        [SerializeField] private float _horizontalViewAngle = 0f;
        [SerializeField] private float _viewRadius = 1f;

        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstacleMask;

        private float _horizontalViewHalfAngle = 0f;

        private List<Collider2D> container = new List<Collider2D>();

        private void Awake()
        {
            _horizontalViewHalfAngle = _horizontalViewAngle * 0.5f;
        }

        public Collider2D[] GetTargets()
        {
            container.Clear();

            Vector2 originPos = transform.position;
            Collider2D[] cols = Physics2D.OverlapCircleAll(originPos, _viewRadius, _targetMask);
            
            foreach (Collider2D col in cols)
            {
                float lookAngle = transform.eulerAngles.z;
                float lookRadian = lookAngle * Mathf.Deg2Rad;
                Vector2 lookDir = new Vector2(Mathf.Cos(lookRadian), Mathf.Sin(lookRadian));
                Vector2 targetPos = col.transform.position;
                Vector2 dir = (targetPos - originPos).normalized;

                float angle = Vector2.Angle(lookDir, dir);

                if (angle <= _horizontalViewHalfAngle)
                {
                    RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, _viewRadius, _obstacleMask);
                    if (!rayHitedTarget)
                    {
                        container.Add(col);
                    }
                }
            }

            if (container.Count > 0)
                return container.ToArray();
            else
                return null;
        }


        private void OnDrawGizmos()
        {
            if (_debugMode)
            {
                _horizontalViewHalfAngle = _horizontalViewAngle * 0.5f;

                Vector3 originPos = transform.position;

                Gizmos.DrawWireSphere(originPos, _viewRadius);

                float angle = transform.eulerAngles.z;
                float leftAngle = angle + _horizontalViewHalfAngle;
                float rightAngle = angle - _horizontalViewHalfAngle;
                float leftRadian = leftAngle * Mathf.Deg2Rad;
                float rightRadian = rightAngle * Mathf.Deg2Rad;

                Vector3 horizontalLeftDir = new Vector3(Mathf.Cos(leftRadian), Mathf.Sin(leftRadian), 0);
                Vector3 horizontalRightDir = new Vector3(Mathf.Cos(rightRadian), Mathf.Sin(rightRadian), 0);

                Debug.DrawRay(originPos, horizontalLeftDir * _viewRadius, Color.cyan);
                Debug.DrawRay(originPos, horizontalRightDir * _viewRadius, Color.cyan);
            }
        }
    }
}
