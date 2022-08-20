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
                int 증감수치 = 0;
                float 증감비율 = 1.0f;
                if (onCalcMaxHP수치 != null) {
                    foreach (var f in onCalcMaxHP수치.GetInvocationList().Cast<Func<int>>()) {
                        증감수치 += f();
                    }
                }
                if (onCalcMaxHP비율 != null) {
                    foreach (var f in onCalcMaxHP비율.GetInvocationList().Cast<Func<float>>()) {
                        float 값 = f();
                        if (값 <= 0) continue;
                        증감비율 *= 값;
                    }
                }
                return Mathf.RoundToInt(스텟HP * 증감비율 + 증감수치);
            }
        }
        public int ATK {
            get {
                int 증감수치 = 0;
                float 증감비율 = 1.0f;
                if (onCalcATK수치 != null) {
                    foreach (var f in onCalcATK수치.GetInvocationList().Cast<Func<int>>()) {
                        증감수치 += f();
                    }
                }
                if (onCalcATK비율 != null) {
                    foreach (var f in onCalcATK비율.GetInvocationList().Cast<Func<float>>()) {
                        float 값 = f();
                        if (값 <= 0) continue;
                        증감비율 *= 값;
                    }
                }
                return Mathf.RoundToInt(스텟ATK * 증감비율 + 증감수치);
            }
        }
        public int DEF {
            get {
                int 증감수치 = 0;
                float 증감비율 = 1.0f;
                if (onCalcDEF수치 != null) {
                    foreach (var f in onCalcDEF수치.GetInvocationList().Cast<Func<int>>()) {
                        증감수치 += f();
                    }
                }
                if (onCalcDEF비율 != null) {
                    foreach (var f in onCalcDEF비율.GetInvocationList().Cast<Func<float>>()) {
                        float 값 = f();
                        if (값 <= 0) continue;
                        증감비율 *= 값;
                    }
                }
                return Mathf.RoundToInt(스텟DEF * 증감비율 + 증감수치);
            }
        }
        public float Speed {
            get {
                float 증감수치 = 0;
                float 증감비율 = 1.0f;
                if (onCalcSpeed수치 != null) {
                    foreach (var f in onCalcSpeed수치.GetInvocationList().Cast<Func<float>>()) {
                        증감수치 += f();
                    }
                }
                if (onCalcSpeed비율 != null) {
                    foreach (var f in onCalcSpeed비율.GetInvocationList().Cast<Func<float>>()) {
                        float 값 = f();
                        if (값 <= 0) continue;
                        증감비율 *= 값;
                    }
                }
                return 스텟Speed * 증감비율 + 증감수치;
            }
        }
        public float Poise {
            get {
                float 증감수치 = 0;
                float 증감비율 = 1.0f;
                if (onCalcPoise수치 != null) {
                    foreach (var f in onCalcPoise수치.GetInvocationList().Cast<Func<float>>()) {
                        증감수치 += f();
                    }
                }
                if (onCalcPoise비율 != null) {
                    foreach (var f in onCalcPoise비율.GetInvocationList().Cast<Func<float>>()) {
                        float 값 = f();
                        if (값 <= 0) continue;
                        증감비율 *= 값;
                    }
                }
                return 스텟Poise * 증감비율 + 증감수치;
            }
        }
        public float Weight {
            get {
                float 증감수치 = 0;
                float 증감비율 = 1.0f;
                if (onCalcWeight수치 != null) {
                    foreach (var f in onCalcWeight수치.GetInvocationList().Cast<Func<float>>()) {
                        증감수치 += f();
                    }
                }
                if (onCalcWeight비율 != null) {
                    foreach (var f in onCalcWeight비율.GetInvocationList().Cast<Func<float>>()) {
                        float 값 = f();
                        if (값 <= 0) continue;
                        증감비율 *= 값;
                    }
                }
                return 스텟Weight * 증감비율 + 증감수치;
            }
        }
        public float ATKSpeed {
            get {
                float 증감수치 = 0;
                float 증감비율 = 1.0f;
                if (onCalcATKSpeed수치 != null) {
                    foreach (var f in onCalcATKSpeed수치.GetInvocationList().Cast<Func<float>>()) {
                        증감수치 += f();
                    }
                }
                if (onCalcATKSpeed비율 != null) {
                    foreach (var f in onCalcATKSpeed비율.GetInvocationList().Cast<Func<float>>()) {
                        float 값 = f();
                        if (값 <= 0) continue;
                        증감비율 *= 값;
                    }
                }
                return 스텟ATKSpeed * 증감비율 + 증감수치;
            }
        }

        // 모든 계산식의 원본이 되는 수치. 
        // 이 수치는 게임 상에서 다른 증감수치에 의해서 변동되선 안된다.

        protected int _HP;
        [Header("- Status")]
        [SerializeField] protected int 스텟HP;
        [SerializeField] protected int 스텟ATK;
        [SerializeField] protected int 스텟DEF;
        [SerializeField] protected float 스텟Speed;
        [SerializeField] protected float 스텟ATKSpeed;
        [SerializeField] protected float 스텟Poise;
        [SerializeField] protected float 스텟Weight;
        

        // 스텟의 값을 결정하는데 사용될 추가 요소들.

        public Func<int> onCalcMaxHP수치;
        public Func<int> onCalcATK수치;
        public Func<int> onCalcDEF수치;
        public Func<float> onCalcSpeed수치;
        public Func<float> onCalcPoise수치;
        public Func<float> onCalcWeight수치;
        public Func<float> onCalcATKSpeed수치;

        public Func<float> onCalcMaxHP비율;
        public Func<float> onCalcATK비율;
        public Func<float> onCalcDEF비율;
        public Func<float> onCalcSpeed비율;
        public Func<float> onCalcPoise비율;
        public Func<float> onCalcWeight비율;
        public Func<float> onCalcATKSpeed비율;

        // DataEntity에서 사용하는 Callback

        public On이벤트 on피해예정;
        public On이벤트 on피해;

        public On이벤트 on추가피해예정;
        public On이벤트 on추가피해;

        public On이벤트 onHP회복예정;
        public On이벤트 onHP회복;

        public On이벤트 onArtifact획득예정;
        public On이벤트 onArtifact획득;

        

        public On이벤트 onEffect부여예정;
        public On이벤트 onEffect부여;

        public On이벤트 onEffect회수예정;
        public On이벤트 onEffect회수;

        public On이벤트 onEffect제거예정;
        public On이벤트 onEffect제거;

        // UI등에서 사용할 Callback

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

        public virtual void Damage(int value, float 경직 = 0f, float 넉백 = 0f) {
            HP -= value;
        }
    }
}

