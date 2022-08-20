using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TenMinute.Data;
using TenMinute.Event;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {
    public class Character : MonoBehaviour {
        [Header("- Base")]
        [SerializeField]
        protected CharacterID _id;
        [SerializeField]
        protected Animator _animator;
        [SerializeField]
        protected Rigidbody2D _rb2D;

        #region Status

        public virtual int HP {
            get {
                return _HP;
            }
            set {
                int prev = _HP;
                _HP = Mathf.Clamp(value, 0, MaxHP);
                onHPValueChanged?.Invoke(prev, _HP);
            }
        }
        public int MaxHP {
            get {
                int ������ġ = 0;
                float �������� = 1.0f;
                if (onCalcMaxHP��ġ != null) {
                    foreach (var f in onCalcMaxHP��ġ.GetInvocationList().Cast<Func<int>>()) {
                        ������ġ += f();
                    }
                }
                if (onCalcMaxHP���� != null) {
                    foreach (var f in onCalcMaxHP����.GetInvocationList().Cast<Func<float>>()) {
                        float �� = f();
                        if (�� <= 0) continue;
                        �������� *= ��;
                    }
                }
                return Mathf.RoundToInt(����HP * �������� + ������ġ);
            }
        }
        public int ATK {
            get {
                int ������ġ = 0;
                float �������� = 1.0f;
                if (onCalcATK��ġ != null) {
                    foreach (var f in onCalcATK��ġ.GetInvocationList().Cast<Func<int>>()) {
                        ������ġ += f();
                    }
                }
                if (onCalcATK���� != null) {
                    foreach (var f in onCalcATK����.GetInvocationList().Cast<Func<float>>()) {
                        float �� = f();
                        if (�� <= 0) continue;
                        �������� *= ��;
                    }
                }
                return Mathf.RoundToInt(����ATK * �������� + ������ġ);
            }
        }
        public int DEF {
            get {
                int ������ġ = 0;
                float �������� = 1.0f;
                if (onCalcDEF��ġ != null) {
                    foreach (var f in onCalcDEF��ġ.GetInvocationList().Cast<Func<int>>()) {
                        ������ġ += f();
                    }
                }
                if (onCalcDEF���� != null) {
                    foreach (var f in onCalcDEF����.GetInvocationList().Cast<Func<float>>()) {
                        float �� = f();
                        if (�� <= 0) continue;
                        �������� *= ��;
                    }
                }
                return Mathf.RoundToInt(����DEF * �������� + ������ġ);
            }
        }
        public float Speed {
            get {
                float ������ġ = 0;
                float �������� = 1.0f;
                if (onCalcSpeed��ġ != null) {
                    foreach (var f in onCalcSpeed��ġ.GetInvocationList().Cast<Func<float>>()) {
                        ������ġ += f();
                    }
                }
                if (onCalcSpeed���� != null) {
                    foreach (var f in onCalcSpeed����.GetInvocationList().Cast<Func<float>>()) {
                        float �� = f();
                        if (�� <= 0) continue;
                        �������� *= ��;
                    }
                }
                return ����Speed * �������� + ������ġ;
            }
        }
        public float Poise {
            get {
                float ������ġ = 0;
                float �������� = 1.0f;
                if (onCalcPoise��ġ != null) {
                    foreach (var f in onCalcPoise��ġ.GetInvocationList().Cast<Func<float>>()) {
                        ������ġ += f();
                    }
                }
                if (onCalcPoise���� != null) {
                    foreach (var f in onCalcPoise����.GetInvocationList().Cast<Func<float>>()) {
                        float �� = f();
                        if (�� <= 0) continue;
                        �������� *= ��;
                    }
                }
                return ����Poise * �������� + ������ġ;
            }
        }
        public float Weight {
            get {
                float ������ġ = 0;
                float �������� = 1.0f;
                if (onCalcWeight��ġ != null) {
                    foreach (var f in onCalcWeight��ġ.GetInvocationList().Cast<Func<float>>()) {
                        ������ġ += f();
                    }
                }
                if (onCalcWeight���� != null) {
                    foreach (var f in onCalcWeight����.GetInvocationList().Cast<Func<float>>()) {
                        float �� = f();
                        if (�� <= 0) continue;
                        �������� *= ��;
                    }
                }
                return ����Weight * �������� + ������ġ;
            }
        }
        public float ATKSpeed {
            get {
                float ������ġ = 0;
                float �������� = 1.0f;
                if (onCalcATKSpeed��ġ != null) {
                    foreach (var f in onCalcATKSpeed��ġ.GetInvocationList().Cast<Func<float>>()) {
                        ������ġ += f();
                    }
                }
                if (onCalcATKSpeed���� != null) {
                    foreach (var f in onCalcATKSpeed����.GetInvocationList().Cast<Func<float>>()) {
                        float �� = f();
                        if (�� <= 0) continue;
                        �������� *= ��;
                    }
                }
                return ����ATKSpeed * �������� + ������ġ;
            }
        }

        // ��� ������ ������ �Ǵ� ��ġ. 
        // �� ��ġ�� ���� �󿡼� �ٸ� ������ġ�� ���ؼ� �����Ǽ� �ȵȴ�.

        protected int _HP;
        [Header("- Status")]
        [SerializeField] protected int ����HP;
        [SerializeField] protected int ����ATK;
        [SerializeField] protected int ����DEF;
        [SerializeField] protected float ����Speed;
        [SerializeField] protected float ����ATKSpeed;
        [SerializeField] protected float ����Poise;
        [SerializeField] protected float ����Weight;
        

        // ������ ���� �����ϴµ� ���� �߰� ��ҵ�.

        public Func<int> onCalcMaxHP��ġ;
        public Func<int> onCalcATK��ġ;
        public Func<int> onCalcDEF��ġ;
        public Func<float> onCalcSpeed��ġ;
        public Func<float> onCalcPoise��ġ;
        public Func<float> onCalcWeight��ġ;
        public Func<float> onCalcATKSpeed��ġ;

        public Func<float> onCalcMaxHP����;
        public Func<float> onCalcATK����;
        public Func<float> onCalcDEF����;
        public Func<float> onCalcSpeed����;
        public Func<float> onCalcPoise����;
        public Func<float> onCalcWeight����;
        public Func<float> onCalcATKSpeed����;

        // DataEntity���� ����ϴ� Callback

        public On�̺�Ʈ on���ؿ���;
        public On�̺�Ʈ on����;

        public On�̺�Ʈ on�߰����ؿ���;
        public On�̺�Ʈ on�߰�����;

        public On�̺�Ʈ onHPȸ������;
        public On�̺�Ʈ onHPȸ��;

        public On�̺�Ʈ onArtifactȹ�濹��;
        public On�̺�Ʈ onArtifactȹ��;

        

        public On�̺�Ʈ onEffect�ο�����;
        public On�̺�Ʈ onEffect�ο�;

        public On�̺�Ʈ onEffectȸ������;
        public On�̺�Ʈ onEffectȸ��;

        public On�̺�Ʈ onEffect���ſ���;
        public On�̺�Ʈ onEffect����;

        // UI��� ����� Callback

        public Action<int, int> onHPValueChanged;

        #endregion

        [Header("- AI")]
        [SerializeField] protected bool _isNPC;
        [SerializeField] protected BehaviorTree _behaviorTree;

        public CharacterID ID => _id;
        public Animator Animator => _animator;
        public Rigidbody2D RB2D => _rb2D;
        public bool IsAlive => IsInit && IsDead == false && IsDispose == false;
        public bool IsNPC => _isNPC;
        public bool IsInit { get; protected set; } = false;
        public bool IsDead { get; protected set; }
        public bool IsDispose { get; protected set; }

        private void Start() {
            if (IsInit == false) {
                Init();
            }
        }

        #region Physics
        public virtual void Move(Vector2 dir) {
            RB2D.velocity = dir.normalized * Speed;
            Animator.SetBool("IsMove", true);
        }
        public virtual void Stop() {
            RB2D.velocity = Vector2.zero;
            Animator.SetBool("IsMove", false);
        }
        #endregion

        #region Artifact, Immune, Effect
        protected Dictionary<ArtifactID, Artifact> _artifacts;
        protected Dictionary<EffectID, Effect> _effects;
        protected HashSet<EffectID> _immunes;
        public virtual void AddArtifact(Artifact artifact) {
            _artifacts.Add(artifact.ID, artifact);
        }
        public virtual void RemoveArtifact(ArtifactID artifact) {
            _artifacts.Remove(artifact);
        }
        public virtual bool HasArtifact(ArtifactID artifact) {
            return _artifacts.ContainsKey(artifact);
        }
        public virtual Artifact GetArtifact(ArtifactID artifact) {
            if (_artifacts.TryGetValue(artifact, out Artifact value)) {
                return value;
            }
            return null;
        }
        public virtual void AddImmune(EffectID effect) {
            _immunes.Add(effect);
        }
        public virtual void RemoveImmune(EffectID effect) {
            _immunes.Remove(effect);
        }
        public virtual bool IsImmune(EffectID effect) {
            return _immunes.Contains(effect);
        }
        public virtual void AddEffect(Effect effect) {
            _effects.Add(effect.ID, effect);
        }
        public virtual void RemoveEffect(EffectID effect) {
            _effects.Remove(effect);
        }
        public virtual bool HasEffect(EffectID effect) {
            return _effects.ContainsKey(effect);
        }
        public virtual Effect GetEffect(EffectID effect) {
            if (_effects.TryGetValue(effect, out Effect value)) {
                return value;
            }
            return null;
        }
        #endregion

        public virtual void Init() {
            _effects = new Dictionary<EffectID, Effect>();
            _immunes = new HashSet<EffectID>();
            _artifacts = new Dictionary<ArtifactID, Artifact>();
            HP = MaxHP;
            IsInit = true;
        }

        public virtual void Dead() {
            IsDead = true;
        }

        public virtual void AttackToTarget(Character target, UnityAction onComplete) {
            onComplete?.Invoke();
        }

        public virtual void Dispose() {
            IsDispose = true;
        }

        public virtual void Heal(int value) {
            HP += value;
        }

        public virtual void Damage(int value, float ���� = 0f, float �˹� = 0f) {
            HP -= value;
        }
    }
}

