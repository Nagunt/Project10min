using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TenMinute.Data;

namespace TenMinute {

    public enum EffectType {
        None = 0,

    }

    [System.Flags]
    public enum EffectAttribute {
        None = 0,
        ��ø���� = 1 << 0,
        �������� = 1 << 1,
        ���ΰ��� = 1 << 2,
    }

    [System.Flags]
    public enum EffectDurationType {
        None = 0,
        ��ø�ñ������ӽð����� = 1 << 0,
        ��ø�ýű����ӽð����� = 1 << 1,
        �������ø����         = 1 << 2,
        ���������            = 1 << 3,
        �ݿ�����              = 1 << 4
    }

    public class EffectList {

        private Character _owner;
        private Dictionary<EffectID, Effect> _data;

        public EffectList(Character owner) {
            _owner = owner;
            _data = new Dictionary<EffectID, Effect>();
        }

        public bool Hasȿ��(EffectID effect) => _data.ContainsKey(effect);

        public Effect GetEffect(EffectID effect) {
            if(_data.TryGetValue(effect, out Effect target)) {
                return target;
            }
            return null;
        }

        public int GetEffectValue(EffectID effect) {
            if (_data.TryGetValue(effect, out Effect target)) {
                return target.Value;
            }
            return 0;
        }

        private void AddEffect(Effect effect) {
            _data.Add(effect.ID, effect);
            effect.Active(_owner);
        }

        private void RemoveEffect(EffectID effect) {
            Effect target = GetEffect(effect);
            _data.Remove(effect);
            target.Dispose();
        }

        public void Effect�ο�(Effect effect) {
            if (effect.Value != 0 || effect.Attribute.HasFlag(EffectAttribute.���ΰ���)) {
                if (Hasȿ��(effect.ID)) {
                    Effect mergeTarget = GetEffect(effect.ID);
                    mergeTarget.Merge(effect);

                    if (mergeTarget.IsActive == false) {
                        RemoveEffect(effect.ID);
                    }

                }
                else {
                    AddEffect(Effect.Create(effect.ID, effect.Value));
                }
            }
        }

        public void Effectȸ��(Effect effect) {
            if (Hasȿ��(effect.ID)) {
                Effect target = GetEffect(effect.ID);
                target.Subtract(effect);

                if (target.IsActive == false) {
                    RemoveEffect(effect.ID);
                }
            }
            else {
                if (effect.Attribute.HasFlag(EffectAttribute.��������)) {
                    AddEffect(Effect.Create(effect.ID, -effect.Value));
                }
            }
        }

        public void Effect����(EffectID effect) {
            if (Hasȿ��(effect)) {
                RemoveEffect(effect);
            }
        }
    }

    public partial class Effect {
        private EffectID _id;
        private EffectType _type;
        private EffectAttribute _attribute;
        private EffectDurationType _durationType;
        private Character _owner;
        private int _value;
        private float _duration;

        private Sequence _sequence;

        public EffectID ID => _id;
        public EffectType Type => _type;
        public EffectAttribute Attribute => _attribute;
        public EffectDurationType DurationType => _durationType;
        public Character Owner => _owner;
        public int Value => _value;
        public float Duration => _duration;

        public bool IsActive {
            get {
                if (Attribute.HasFlag(EffectAttribute.��������)) {
                    if (Attribute.HasFlag(EffectAttribute.���ΰ���)) {
                        return true;
                    }
                    else {
                        return Value != 0;
                    }
                }
                else {
                    if (Attribute.HasFlag(EffectAttribute.���ΰ���)) {
                        return Value >= 0;
                    }
                    else {
                        return Value > 0;
                    }
                }
            }
        }

        protected Effect(int value, 
            EffectID id, EffectType type = EffectType.None, EffectAttribute attribute = EffectAttribute.None, EffectDurationType durationType = EffectDurationType.None) {
            _value = value;
            _id = id;
            _type = type;
            _attribute = attribute;
            _durationType = durationType;
            if (_attribute.HasFlag(EffectAttribute.��ø����) == false) _value = 1;
        }

        public Effect SetDuration(float duration) {
            _duration = duration;
            return this;
        }
        public void Merge(Effect effect) {
            _value = Attribute.HasFlag(EffectAttribute.��ø����) ? _value + effect.Value : 1;
            if (IsActive) {
                if (DurationType.HasFlag(EffectDurationType.��ø�ñ������ӽð�����) ||
                    DurationType.HasFlag(EffectDurationType.��ø�ýű����ӽð�����)) {
                    _duration = DurationType.HasFlag(EffectDurationType.��ø�ñ������ӽð�����) ? _duration : effect.Duration;

                    if (_sequence.IsActive()) {
                        _sequence.Restart(); // ���ӽð� ����
                    }
                }
            }
        }
        public void Subtract(Effect effect) {
            _value = Attribute.HasFlag(EffectAttribute.��ø����) ? _value - effect.Value : 0;
        }
        public virtual void Active(Character owner) {
            _owner = owner;
            if (DurationType.HasFlag(EffectDurationType.�ݿ�����) == false) {
                _sequence = DOTween.Sequence();
                _sequence.
                    AppendInterval(Duration).
                    AppendCallback(OnDurationEnd).
                    OnKill(() => _sequence = null).
                    Play();
            }
        }
        public virtual void Dispose() {
            if (_sequence.IsActive()) {
                _sequence.Kill();
            }
        }
        private void OnDurationEnd() {
            Combat.Entity.Create(
                target: Owner).
                Addȿ������(ID).
                Execute();
        }
    }
}
