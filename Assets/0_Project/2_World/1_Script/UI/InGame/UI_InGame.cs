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
        protected override void Awake() {
            base.Awake();
        }

        public void SetUI_HP(int value) {
            text_HP.SetText($"{value / 60}:{value % 60}");
            image_HP.fillAmount = value / 600f;
        }
    }
}