using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TenMinute.Data;
using TenMinute.Event;
using TenMinute.Combat;
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
                _HP = value;
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

        #region Artifact, Effect
        protected Dictionary<ArtifactID, Artifact> _artifacts;
        protected EffectList _effectList;
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
        #endregion

        public virtual void Init() {
            _effectList = new EffectList(this);
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

        #region Entity 액션

        public void Apply피해(Entity 피해Entity, int dataIndex) {
            if (IsDead) return;

            Global_EventSystem.Combat.CallOn피해입힐예정(피해Entity, dataIndex);        // 이벤트를 전역으로 두는 이유는, 주체와 대상 말고도 다른 존재가 일련의 행동에 영향을 줄 수 있을 가능성이 있기 때문이다.
                                                                                        // EX) 오라
            DataEntity 피해Data = 피해Entity.GetData(dataIndex);

            if (피해Data.Property.HasFlag(EntityProperty.방어무시) == false) {
                피해Data.Add추가량(-DEF);
            }

            Global_EventSystem.Combat.CallOn피해입을예정(피해Entity, dataIndex);

            int 피해량 = 피해Data.데이터;
            if (피해량 < 0) 피해량 = 0;

            int 기존HP = HP;

            HP -= 피해량;

            if (HP < 0) {
                HP = 0;
            }   

            피해Data.SetResultData_HP(기존HP, HP);

            if (피해량 > 0) {
                Global_EventSystem.Combat.CallOn피해입음(피해Entity, dataIndex);
            }

            if (HP <= 0 && 피해량 > 0) {
                Dead();
                if (IsDead) return;
            }

            if (피해량 > 0) {
                Global_EventSystem.Combat.CallOn피해입고생존(피해Entity, dataIndex);
            }

        }

        public void Apply경직(Entity 경직Entity, int dataIndex) {
            DataEntity 경직Data = 경직Entity.GetData(dataIndex);

        }

        public void Apply넉백(Entity 넉백Entity, int dataIndex) {
            DataEntity 넉백Data = 넉백Entity.GetData(dataIndex);
        }

        public void Apply효과(Entity 효과Entity, int dataIndex) {
            DataEntity 효과Data = 효과Entity.GetData(dataIndex);

            int 기존Effect = _effectList.GetEffectValue(효과Data.효과.ID);

            switch (효과Data.Type) {
                case EntityType.효과부여: {
                        Global_EventSystem.Combat.CallOn효과부여예정(효과Entity, dataIndex);
                        if (효과Data.Is효과무효 == false) {
                            _effectList.Effect부여(효과Data.효과);
                            Global_EventSystem.Combat.CallOn효과부여(효과Entity, dataIndex);
                        }
                    }
                    break;
                case EntityType.효과회수: {
                        Global_EventSystem.Combat.CallOn효과회수예정(효과Entity, dataIndex);
                        if (효과Data.Is효과무효 == false) {
                            if (효과Data.효과.Attribute.HasFlag(EffectAttribute.음수가능) == false &&
                                _effectList.GetEffectValue(효과Data.효과.ID) <= 효과Data.효과.Value) {
                                Global_EventSystem.Combat.CallOn효과회수후제거(효과Entity, dataIndex);
                            }
                            _effectList.Effect회수(효과Data.효과);
                            Global_EventSystem.Combat.CallOn효과회수(효과Entity, dataIndex);
                        }
                    }
                    break;
                case EntityType.효과제거: {
                        if (기존Effect != 0) {
                            Global_EventSystem.Combat.CallOn효과회수후제거(효과Entity, dataIndex);
                            _effectList.Effect제거(효과Data.효과.ID);
                            Global_EventSystem.Combat.CallOn효과제거(효과Entity, dataIndex);
                        }
                    }
                    break;
            }

            효과Data.SetResultData_Effect(기존Effect, _effectList.GetEffectValue(효과Data.효과.ID));
        }

        public void ApplyHP지정(Entity entity, int dataIndex) {
            DataEntity 지정Data = entity.GetData(dataIndex);
            int 기존HP = HP;
            HP = 지정Data.데이터;
            지정Data.SetResultData_HP(기존HP, HP);
        }

        public void ApplyHP회복(Entity 회복Entity, int dataIndex) {
            if (IsDead) return;

            Global_EventSystem.Combat.CallOn회복시킬예정(회복Entity, dataIndex);
            Global_EventSystem.Combat.CallOn회복받을예정(회복Entity, dataIndex);

            DataEntity 회복Data = 회복Entity.GetData(dataIndex);
            int 회복량 = 회복Data.데이터;
            if (회복량 < 0) 회복량 = 0;

            int 기존HP = HP;

            HP += 회복량;

            if (HP > MaxHP) {
                HP = MaxHP;
            }

            회복Data.SetResultData_HP(기존HP, HP);

            if (회복량 > 0) {
                Global_EventSystem.Combat.CallOn회복(회복Entity, dataIndex);
            }
        }

        #endregion
    }
}

