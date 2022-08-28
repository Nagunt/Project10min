using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

namespace TenMinute.Graphics {
    public class Graphic_Text : MonoBehaviour {
        [SerializeField]
        private TextMeshPro _textMesh;

        private Sequence _sequence;

        public Action<Graphic_Text> onComplete;

        public Graphic_Text SetText(string text) {
            _textMesh.SetText(text);
            return this;
        }

        public Graphic_Text SetActive(bool state) {
            gameObject.SetActive(state);
            return this;
        }

        public Graphic_Text SetColor(Color color) {
            _textMesh.color = color;
            return this;
        }

        public Graphic_Text SetPosition(Vector2 pos) {
            transform.position = new Vector2(pos.x + UnityEngine.Random.Range(-0.1f, 0.1f), pos.y);
            return this;
        }

        public Graphic_Text Play() {
            if (_sequence.IsActive()) {
                _sequence.Kill();
            }
            _sequence = DOTween.Sequence();
            _sequence.
                Append(transform.DOMoveY(.5f, 1.75f).SetRelative()).
                AppendInterval(0.25f).
                SetAutoKill().
                OnComplete(() => onComplete?.Invoke(this)).
                OnKill(() => _sequence = null).
                Play();
            return this;
        }
    }
}
