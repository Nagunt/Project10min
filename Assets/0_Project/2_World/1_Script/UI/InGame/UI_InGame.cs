using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace TenMinute.UI {
    public class UI_InGame : UI_Base {

        [Header("- InGame")]
        [SerializeField]
        private TextMeshProUGUI text_HP;
        [SerializeField]
        private Image image_HP;
        [SerializeField]
        private Image dot;
        [SerializeField]
        private RectTransform mapAnchor;
        [SerializeField]
        private RectTransform mapLayer_Floor;
        [SerializeField]
        private RectTransform mapLayer_Block;
        [SerializeField]
        private RectTransform mapLayer_Object;

        private Vector2 dotSize;
        private Dictionary<Transform, Image> dotObject;

        protected override void Start() {
            base.Start();
            dotSize = dot.rectTransform.sizeDelta;
            dotObject = new Dictionary<Transform, Image>();
        }

        public void SetUI_HP(int value) {
            text_HP.SetText($"{value / 60}:{value % 60}");
            image_HP.fillAmount = value / 600f;
        }

        public void SetUI_Map(Room_Base room) {
            mapAnchor.sizeDelta = new Vector2(room.Area.width * dotSize.x, room.Area.height * dotSize.y);
            mapAnchor.localPosition = Vector2.zero;
            for (int i = mapLayer_Floor.childCount - 1; i >= 0; i--) {
                Destroy(mapLayer_Floor.GetChild(i).gameObject);
            }
            for (int i = mapLayer_Block.childCount - 1; i >= 0; i--) {
                Destroy(mapLayer_Block.GetChild(i).gameObject);
            }
            int xMin = (int)room.Area.xMin;
            int xMax = (int)room.Area.xMax;
            int yMin = (int)room.Area.yMin;
            int yMax = (int)room.Area.yMax;
            for (int y = yMin; y < yMax; y++) {
                for (int x = xMin; x < xMax; x++) {
                    if (room.Tilemap_Floor.HasTile(new Vector3Int(x, y, 0))) {
                        Image newDot = Instantiate(dot, mapLayer_Floor);
                        newDot.color = Color.white;
                        newDot.rectTransform.localPosition = new Vector2(x * dotSize.x, y * dotSize.y);
                        newDot.gameObject.SetActive(true);
                    }
                    if (room.Tilemap_Block.HasTile(new Vector3Int(x, y, 0))) {
                        Image newDot = Instantiate(dot, mapLayer_Block);
                        newDot.color = Color.black;
                        newDot.rectTransform.localPosition = new Vector2(x * dotSize.x, y * dotSize.y);
                        newDot.gameObject.SetActive(true);
                    }
                }
            }
        }

        public void UpdateUI_Map(Transform target, bool state) {
            if (state) {
                if (dotObject.TryGetValue(target, out Image targetDot)) {
                    targetDot.rectTransform.localPosition = new Vector2(target.localPosition.x * dotSize.x, target.localPosition.y * dotSize.y);
                }
                else {
                    Image newDot = Instantiate(dot, mapLayer_Object);
                    switch (target.tag) {
                        case "Player":
                            newDot.color = Color.green;
                            break;
                        case "Enemy":
                            newDot.color = Color.red;
                            break;
                    }
                    newDot.rectTransform.pivot = new Vector2(0.5f, newDot.rectTransform.pivot.y);
                    newDot.rectTransform.localPosition = new Vector2(target.localPosition.x * dotSize.x, target.localPosition.y * dotSize.y);
                    newDot.gameObject.SetActive(true);
                    dotObject.Add(target, newDot);
                }
            }
            else {
                if (dotObject.TryGetValue(target, out Image targetDot)) {
                    dotObject.Remove(target);
                    Destroy(targetDot.gameObject);
                }
            }
        }
    }
}