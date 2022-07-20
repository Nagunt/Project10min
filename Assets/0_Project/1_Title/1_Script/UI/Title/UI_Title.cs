using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TenMinute.UI {

    public class UI_Title : UI_Base {
        [Header("- Title")]
        [SerializeField]
        private Button button_Start;
        [SerializeField]
        private Button button_Quit;


        protected override void Start() {
            button_Start.onClick.AddListener(OnClick_Start);
            button_Quit.onClick.AddListener(OnClick_Quit);
        }

        private void OnClick_Start() {
            Debug.Log("Start");
        }

        private void OnClick_Quit() {
            Application.Quit();
        }
    }
}