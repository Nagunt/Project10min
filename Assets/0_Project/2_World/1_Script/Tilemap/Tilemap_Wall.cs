using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TenMinute.Event;

namespace TenMinute {
    public class Tilemap_Wall : MonoBehaviour {
        [SerializeField]
        private Rigidbody2D rb2D;
        [SerializeField]
        private Tilemap[] layers;


        private void OnCollisionStay2D(Collision2D collision) {
            foreach (Tilemap tilemap in layers) {
                foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
                    Vector3Int localPos = new Vector3Int(pos.x, pos.y, pos.z);
                    if (tilemap.HasTile(localPos)) {
                        tilemap.SetTileFlags(localPos, TileFlags.None);
                        tilemap.SetColor(localPos, new Color(1f, 1f, 1f, 1f));

                    }
                }
            }
            
            List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
            rb2D.GetContacts(contactPoints);
            foreach(ContactPoint2D point2D in contactPoints) {
                if (rb2D.OverlapPoint(point2D.collider.transform.position)) {
                    /*
                    Debug.DrawLine(
                            new Vector3(point2D.point.x - 0.05f, point2D.point.y - 0.05f),
                            new Vector3(point2D.point.x + 0.05f, point2D.point.y + 0.05f),
                            Color.blue,
                            3f);
                    Debug.DrawLine(
                        new Vector3(point2D.point.x - 0.05f, point2D.point.y + 0.05f),
                        new Vector3(point2D.point.x + 0.05f, point2D.point.y - 0.05f),
                        Color.blue,
                        3f);*/

                    foreach(Tilemap tilemap in layers) {
                        Vector3Int tilePos = tilemap.WorldToCell(point2D.point);
                        if (tilemap.HasTile(tilePos)) {
                            tilemap.SetTileFlags(tilePos, TileFlags.None);
                            tilemap.SetColor(tilePos, new Color(1f, 1f, 1f, 0.5f));
                        }
                    }
                }
            }
        }
    }
}
