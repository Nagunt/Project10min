using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TenMinute.Physics;

namespace TenMinute {
    [CustomEditor(typeof(ExtraCollider2D))]
    public class ExtraCollider2DBuildButton : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            ExtraCollider2D builder = (ExtraCollider2D)target;
            if (GUILayout.Button("Build")) {
                builder.MakeShape(true);
            }
        }
    }
}
