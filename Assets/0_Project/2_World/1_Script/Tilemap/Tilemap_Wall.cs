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
        private Collider2D col2D;

        private void OnCollisionStay2D(Collision2D collision) {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
                Vector3Int localPos = new Vector3Int(pos.x, pos.y, pos.z);
                if (tilemap.HasTile(localPos)) {
                    tilemap.SetTileFlags(localPos, TileFlags.None);
                    tilemap.SetColor(localPos, new Color(1f, 1f, 1f, 1f));
                    
                }
            }
            
            List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
            col2D.GetContacts(contactPoints);
            foreach(ContactPoint2D point2D in contactPoints) {
                if (col2D.OverlapPoint(point2D.collider.transform.position)) {
                    Debug.DrawLine(
                            new Vector3(point2D.point.x - 0.05f, point2D.point.y - 0.05f),
                            new Vector3(point2D.point.x + 0.05f, point2D.point.y + 0.05f),
                            Color.blue,
                            3f);
                    Debug.DrawLine(
                        new Vector3(point2D.point.x - 0.05f, point2D.point.y + 0.05f),
                        new Vector3(point2D.point.x + 0.05f, point2D.point.y - 0.05f),
                        Color.blue,
                        3f);

                    Vector3Int tilePos = tilemap.WorldToCell(point2D.point);
                    Vector2 debugTilePos = new Vector2(tilePos.x + .5f, tilePos.y + .5f);
                    Debug.DrawLine(
                        new Vector3(debugTilePos.x - 0.05f, debugTilePos.y - 0.05f),
                        new Vector3(debugTilePos.x + 0.05f, debugTilePos.y + 0.05f),
                        Color.cyan,
                        3f);
                    Debug.DrawLine(
                        new Vector3(debugTilePos.x - 0.05f, debugTilePos.y + 0.05f),
                        new Vector3(debugTilePos.x + 0.05f, debugTilePos.y - 0.05f),
                        Color.cyan,
                        3f);
                    if (tilemap.HasTile(tilePos)) {
                        tilemap.SetTileFlags(tilePos, TileFlags.None);
                        tilemap.SetColor(tilePos, new Color(1f, 1f, 1f, 0.5f));
                    }
                }
            }
        }
    }
}
