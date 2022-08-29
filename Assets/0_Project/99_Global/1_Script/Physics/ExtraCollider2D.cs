using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics {
    [RequireComponent(typeof(PolygonCollider2D))]
    public class ExtraCollider2D : MonoBehaviour {
        [SerializeField]
        private PolygonCollider2D _col2D;
        [SerializeField]
        [Range(10, 360)]
        private int Count = 60;
        [SerializeField]
        private float Radius;
        [SerializeField]
        private bool IsArc;
        [SerializeField]
        [Range(1f, 180f)]
        private float Angle;
        [SerializeField]
        private bool IsEllipse;
        [SerializeField]
        private float OtherRadius;

        public void Build() {
            Vector2[] points = new Vector2[Count + (IsArc ? 2 : 0)];
            float unitAngle = (IsArc ? Angle * 2 : 360f) / Count;
            float unitRadian = unitAngle * Mathf.Deg2Rad;

            if (IsArc) {
                points[0] = Vector2.zero;
                for (int i = 0; i <= Count; ++i) {
                    float targetAngle = -Angle + unitAngle * i;
                    float radian = targetAngle * Mathf.Deg2Rad;
                    Vector2 newPoint = new Vector2(Radius * Mathf.Cos(radian), (IsEllipse ? OtherRadius : Radius) * Mathf.Sin(radian));
                    points[i + 1] = newPoint;
                }
            }
            else {
                for (int i = 0; i < Count; ++i) {
                    float radian = unitRadian * i;
                    Vector2 newPoint = new Vector2(Radius * Mathf.Cos(radian), (IsEllipse ? OtherRadius : Radius) * Mathf.Sin(radian));
                    points[i] = newPoint;
                }
            }

            if(_col2D == null) {
                _col2D = GetComponent<PolygonCollider2D>();
            }
            _col2D.SetPath(0, points);
        }

    }
}
