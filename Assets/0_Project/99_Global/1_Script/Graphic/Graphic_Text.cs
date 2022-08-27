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

        public Graphic_Text SetText(string text) {
            _textMesh.SetText(text);
            return this;
        }

        public Graphic_Text SetColor(Color color) {
            _textMesh.color = color;
            return this;
        }

        public Graphic_Text SetPosition(Vector2 pos) {
            transform.position = pos;
            return this;
        }

        public Graphic_Text Play(Action onComplete = null) {
            if (_sequence.IsActive()) {
                _sequence.Kill();
            }
            _sequence = DOTween.Sequence();
            _sequence.
                Append(transform.DOMoveY(-1f, 2f).SetRelative()).
                AppendInterval(0.25f).
                SetAutoKill().
                OnComplete(() => onComplete?.Invoke()).
                OnKill(() => _sequence = null).
                Play();
            return this;
        }
    }
}
