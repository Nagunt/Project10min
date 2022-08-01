using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TenMinute.Event;

namespace TenMinute {
    public class Tilemap_Wall : MonoBehaviour {
        [SerializeField]
        private Tilemap tilemap;
        [SerializeField]
        private TilemapRenderer tilemapRenderer;
        [SerializeField]
        private CompositeCollider2D col2D;
    }
}
