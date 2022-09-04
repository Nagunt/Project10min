using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics {
    public class SightMesh2D : MonoBehaviour {

        [SerializeField]
        private int _rayCount = 360;
        [SerializeField]
        private float _distance = 3f;
        [SerializeField]
        private LayerMask _layerMask;

        private Mesh _mesh;

        private Vector3 GetVector3FromAngle(float angle) {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        private void Awake() {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        private void LateUpdate() {
            Build();
        }
        private void Build() {
            int vertexIndex = 1;
            int triangleIndex = 0;
            float angle = 0f;
            float unitAngle = 360f / _rayCount;
            Vector3 origin = transform.position;
            Vector3[] verticles = new Vector3[_rayCount + 2];
            Vector2[] uv = new Vector2[verticles.Length];
            int[] triangles = new int[_rayCount * 3];
            verticles[0] = origin;

            for (int i = 0; i <= _rayCount; ++i) {
                Vector3 vertex;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(
                    origin,
                    GetVector3FromAngle(angle),
                    _distance, _layerMask);
                if (raycastHit2D.collider == null) {
                    vertex = origin + GetVector3FromAngle(angle) * _distance;
                }
                else {
                    vertex = new Vector3(raycastHit2D.point.x, raycastHit2D.point.y, transform.position.z);
                }
                verticles[vertexIndex] = vertex;

                if (i > 0) {
                    triangles[triangleIndex] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }
                vertexIndex++;
                angle += unitAngle;

            }

            _mesh.vertices = verticles;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
            _mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        }

    }
}
