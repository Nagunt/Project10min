using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Graphics {

    public class Graphic_Base : MonoBehaviour {
        [SerializeField]
        private new Renderer renderer;

        private void Update() {
            renderer.sortingOrder = (int)(-transform.position.y * 10f);
        }
    }
}