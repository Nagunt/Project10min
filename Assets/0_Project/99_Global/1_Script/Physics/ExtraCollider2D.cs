using System;
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
        private float Radius = 0.5f;
        [SerializeField]
        private bool IsArc;
        [SerializeField]
        [Range(1f, 180f)]
        private float Angle;
        [SerializeField]
        [Range(0f, 360f)]
        private float CenterAngle = 0f;
        [SerializeField]
        private bool IsEllipse;
        [SerializeField]
        private float OtherRadius;

        private List<Vector2> _pathInfo;
        private float _unitAngle;
        private bool _needInfoUpdate = false;

        public Action<ExtraCollider2D, Collider2D> onTriggerEnter2D;
        public Action<ExtraCollider2D, Collider2D> onTriggerStay2D;
        public Action<ExtraCollider2D, Collider2D> onTriggerExit2D;

        public Action<ExtraCollider2D, Collision2D> onCollisionEnter2D;
        public Action<ExtraCollider2D, Collision2D> onCollisionStay2D;
        public Action<ExtraCollider2D, Collision2D> onCollisionExit2D;

        public Collider2D Col2D => _col2D;

        private void Build() {
            _pathInfo = new List<Vector2>();
            _unitAngle = 360f / Count;
            float unitRadian = _unitAngle * Mathf.Deg2Rad;
            for (int i = 0; i < Count; ++i) {
                float radian = unitRadian * i;
                _pathInfo.Add(new Vector2(Radius * Mathf.Cos(radian), (IsEllipse ? OtherRadius : Radius) * Mathf.Sin(radian)));
            }
            if (_col2D == null) {
                _col2D = GetComponent<PolygonCollider2D>();
            }
        }

        public ExtraCollider2D SetRadius(float radius) {
            Radius = radius;
            _needInfoUpdate = true;
            return this;
        }

        public ExtraCollider2D SetRadius(float radius, float other) {
            Radius = radius;
            OtherRadius = other;
            _needInfoUpdate = true;
            IsEllipse = true;
            return this;
        }

        public ExtraCollider2D SetArc(bool state) {
            IsArc = state;
            return this;
        }

        public ExtraCollider2D SetAngle(float angle) {
            IsArc = true;
            Angle = angle;
            return this;
        }

        public ExtraCollider2D SetAngle(float angle, float center) {
            Angle = angle;
            CenterAngle = center;
            if (CenterAngle < 0) CenterAngle += 360f;
            IsArc = true;
            return this;
        }

        public ExtraCollider2D SetCenterAngle(float centerAngle) {
            IsArc = true;
            CenterAngle = centerAngle;
            if (CenterAngle < 0) CenterAngle += 360f;
            return this;
        }

        public ExtraCollider2D SetEllipse(bool state) {
            IsEllipse = state;
            _needInfoUpdate = true;
            return this;
        }

        public ExtraCollider2D SetPosition(Vector2 pos) {
            transform.position = pos;
            return this;
        }

        public ExtraCollider2D SetOtherRadius(float radius) {
            IsEllipse = true;
            _needInfoUpdate = true;
            OtherRadius = radius;
            return this;
        }

        public ExtraCollider2D MakeShape(bool isUpdate = false) {
            if (_pathInfo == null) {
                Build();
            }
            else {
                if (isUpdate) {
                    Build();
                }
                else {
                    if (_needInfoUpdate) {
                        _needInfoUpdate = false;
                        Build();
                    }
                }
            }

            if (IsArc) {
                List<Vector2> tempPath = new List<Vector2>();
                float startAngle = CenterAngle - Angle;
                float lastAngle = CenterAngle + Angle;
                if (startAngle < 0) startAngle += 360f;
                if (lastAngle < 0) lastAngle += 360f;
                if (lastAngle >= 360f) lastAngle -= 360f;
                int startIndex = (int)(startAngle / _unitAngle) + 1;
                int centerIndex = (int)(CenterAngle / _unitAngle);    
                int lastIndex = (int)(lastAngle / _unitAngle);
                bool canDraw = (Angle * 2) > _unitAngle;
                tempPath.Add(Vector2.zero);
                tempPath.Add(new Vector2(Radius * Mathf.Cos(startAngle * Mathf.Deg2Rad), (IsEllipse ? OtherRadius : Radius) * Mathf.Sin(startAngle * Mathf.Deg2Rad)));
                if (canDraw) {
                    if (startIndex < centerIndex) {
                        for (int i = startIndex; i <= centerIndex && i < Count; ++i) {
                            tempPath.Add(_pathInfo[i]);
                        }
                    }
                    else if (startIndex > centerIndex) {
                        for (int i = startIndex; i < Count; ++i) {
                            tempPath.Add(_pathInfo[i]);
                        }
                        for (int i = 0; i <= centerIndex && i < Count; ++i) {
                            tempPath.Add(_pathInfo[i]);
                        }
                    }
                }
                tempPath.Add(new Vector2(Radius * Mathf.Cos(CenterAngle * Mathf.Deg2Rad), (IsEllipse ? OtherRadius : Radius) * Mathf.Sin(CenterAngle * Mathf.Deg2Rad)));
                if (canDraw) {
                    if (centerIndex < lastIndex) {
                        for (int i = centerIndex + 1; i <= lastIndex && i < Count; ++i) {
                            tempPath.Add(_pathInfo[i]);
                        }
                    }
                    else if (centerIndex > lastIndex) {
                        for (int i = centerIndex + 1; i < Count; ++i) {
                            tempPath.Add(_pathInfo[i]);
                        }
                        for (int i = 0; i <= lastIndex && i < Count; ++i) {
                            tempPath.Add(_pathInfo[i]);
                        }
                    }
                }
                tempPath.Add(new Vector2(Radius * Mathf.Cos(lastAngle * Mathf.Deg2Rad), (IsEllipse ? OtherRadius : Radius) * Mathf.Sin(lastAngle * Mathf.Deg2Rad)));
                _col2D.SetPath(0, tempPath);
            }
            else {
                _col2D.SetPath(0, _pathInfo);
            }

            
            return this;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            onTriggerEnter2D?.Invoke(this, collision);
        }
        private void OnTriggerStay2D(Collider2D collision) {
            onTriggerStay2D?.Invoke(this, collision);
        }
        private void OnTriggerExit2D(Collider2D collision) {
            onTriggerExit2D?.Invoke(this, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            onCollisionEnter2D?.Invoke(this, collision);
        }
        private void OnCollisionStay2D(Collision2D collision) {
            onCollisionStay2D?.Invoke(this, collision);
        }
        private void OnCollisionExit2D(Collision2D collision) {
            onCollisionExit2D?.Invoke(this, collision);
        }
    }
}
