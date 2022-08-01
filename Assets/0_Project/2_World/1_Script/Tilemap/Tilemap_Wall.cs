using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TenMinute {
    public class Tilemap_Wall : MonoBehaviour {
        [SerializeField]
        private Tilemap tilemap;
        [SerializeField]
        private TilemapRenderer tilemapRenderer;
        private void OnTriggerEnter2D(Collider2D collision) {
            Debug.Log(collision);
        }
    }
}
