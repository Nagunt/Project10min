using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TenMinute.Data;

namespace TenMinute {

    [System.Flags]
    public enum EffectAttribute {
        None = 0,
        ��ø���� = 1 << 0,
        �ݿ����� = 1 << 1,
    }

    public partial class Effect {
        private EffectID _id;
        private EffectAttribute _attribute;
        private Character _owner;
        private int _value;
        private float _duration;

        private Sequence _sequence;

        public EffectID ID => _id;
        public EffectAttribute Attribute => _attribute;
        public Character Owner => _owner;
        public int Value => _value;
        public float Duration => _duration;

        protected Effect(EffectID id, EffectAttribute attribute = EffectAttribute.None) {
            _id = id;
            _attribute = attribute;
        }
        public Effect SetOwner(Character owner) {
            _owner = owner;
            return this;
        }
        public Effect SetValue(int value) {
            _value = value;
            return this;
        }
        public Effect SetDuration(float duration) {
            _duration = duration;
            return this;
        }
        public void Merge(Effect effect) {
            _value = Attribute.HasFlag(EffectAttribute.��ø����) ? _value + effect.Value : 1;
            if (Attribute.HasFlag(EffectAttribute.�ݿ�����) == false) {
                if (_sequence.IsActive()) {
                    _sequence.Restart(); // ���ӽð� ����
                }
            }
        }
        public void Subtract(Effect effect) {
            _value = Attribute.HasFlag(EffectAttribute.��ø����) ? _value - effect.Value : 0;
            if (Value <= 0) {
                OnDisable();
            }
        }
        public virtual void OnEnable() {
            if (Attribute.HasFlag(EffectAttribute.�ݿ�����) == false) {
                _sequence = DOTween.Sequence();
                _sequence.
                    AppendInterval(Duration).
                    AppendCallback(OnDurationEnd).
                    OnKill(() => _sequence = null).
                    Play();
            }
        }
        public virtual void OnDisable() {
            if (_sequence.IsActive()) {
                _sequence.Kill();
            }
            Owner.RemoveEffect(ID);
        }
        public virtual void OnDurationEnd() {
            OnDisable();
        }
    }
}
