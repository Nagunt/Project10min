using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Graphics {
    public class Global_GraphicManager : MonoBehaviour {
        public static Global_GraphicManager Instance { get; private set; } = null;

        [SerializeField]
        private Graphic_Text _prefab_Text;
        [SerializeField]
        private Graphic_Entity _prefab_Entity;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

        public Graphic_Text GetText() {
            return Instantiate(_prefab_Text);
        }

        public Graphic_Entity GetGraphic() {
            return Instantiate(_prefab_Entity);
        }
    }
}
