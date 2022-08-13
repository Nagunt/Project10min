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

        private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

        private void Awake()
        {
            _horizontalViewHalfAngle = _horizontalViewAngle * 0.5f;
        }

        private Vector3 AngleToDirZ(float angleInDegree)
        {
            float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
        }

        public Collider2D[] GetTargets()
        {
            hitedTargetContainer.Clear();

            Vector2 originPos = transform.position;
            Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, _viewRadius);

            foreach (Collider2D hitedTarget in hitedTargets)
            {
                Vector2 targetPos = hitedTarget.transform.position;
                Vector2 dir = (targetPos - originPos).normalized;
                Vector2 lookDir = Vector2.up;

                // float angle = Vector3.Angle(lookDir, dir)
                // 아래 두 줄은 위의 코드와 동일하게 동작함. 내부 구현도 동일
                float dot = Vector2.Dot(lookDir, dir);
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                if (angle <= _horizontalViewHalfAngle)
                {
                    RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, _viewRadius, _obstacleMask);
                    if (!rayHitedTarget)
                    {
                        hitedTargetContainer.Add(hitedTarget);
                    }
                }
            }

            if (hitedTargetContainer.Count > 0)
                return hitedTargetContainer.ToArray();
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

                Vector3 horizontalRightDir = AngleToDirZ(-_horizontalViewHalfAngle);
                Vector3 horizontalLeftDir = AngleToDirZ(_horizontalViewHalfAngle);

                Debug.DrawRay(originPos, horizontalLeftDir * _viewRadius, Color.cyan);
                Debug.DrawRay(originPos, horizontalRightDir * _viewRadius, Color.cyan);
            }
        }
    }
}
