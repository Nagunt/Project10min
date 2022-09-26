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
using TenMinute.Graphics;
using DG.Tweening;

namespace TenMinute {
    public class Character : MonoBehaviour {
        private static Dictionary<Rigidbody2D, Character> rb2DData = new Dictionary<Rigidbody2D, Character>();
        private static Dictionary<Collider2D, Character> colData = new Dictionary<Collider2D, Character>();

        public static Character GetCharacter(Rigidbody2D rb2D) {
            if (rb2DData.TryGetValue(rb2D, out Character value)) {
                return value;
            }
            return null;
        }
        public static Character GetCharacter(Collider2D col2D) {
            if(colData.TryGetValue(col2D, out Character value)) {
                return value;
            }
            return null;
        }

        [Header("- Base")]
        [SerializeField]
        protected CharacterID _id;
        [SerializeField]
        protected Graphic_Character _graphic;
        [SerializeField]
        protected Rigidbody2D _rb2D;
        [SerializeField]
        protected Collider2D _col2D;

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
        public Graphic_Character Graphic => _graphic;
        public Rigidbody2D RB2D => _rb2D;
        public Collider2D Col2D => _col2D;
        public bool IsAlive => IsInit && IsDead == false && IsDispose == false;
        public bool IsNPC => _isNPC;
        public bool IsInit { get; protected set; } = false;
        public bool IsDead { get; protected set; }
        public bool IsKnockDown { get; protected set; } = false;
        private float _knockdownTime = 0f;
        private Coroutine _knockdownRoutine;

        public bool IsKnockBack { get; protected set; } = false;
        private Sequence _knockbackSequence;
        public bool IsDispose { get; protected set; }

        public Vector2 LookDir { get; protected set; }

        private void Start() {
            if (IsInit == false) {
                Init();
            }
        }

        #region Physics
        public virtual void Move(Vector2 dir) {
            RB2D.velocity = dir.normalized * Speed;
            RB2D.velocity = new Vector2(RB2D.velocity.x, RB2D.velocity.y * .5f);
            Look(dir);
            Graphic.SetState_Move(true);
        }
        public virtual void Look(Vector2 dir) {
            LookDir = new Vector2(dir.x > 0 ? 1f : -1f, dir.y > 0 ? 1f : -1f);
            Graphic.SetDirection(LookDir);
        }
        public virtual void Stop() {
            RB2D.velocity = Vector2.zero;
            Graphic.SetState_Move(false);
        }
        public virtual void KnockBack(Vector2 dir, float force) {
            if (IsKnockBack &&
                _knockbackSequence.IsActive()) {
                _knockbackSequence.Kill();
            }

            Vector2 distance = dir * force;

            _knockbackSequence = DOTween.Sequence(this);
            _knockbackSequence.
                Append(RB2D.DOMove(transform.position + new Vector3(distance.x, distance.y * .5f, transform.position.z), 0.5f).SetEase(Ease.OutQuad)).
                OnComplete(() => IsKnockBack = false).
                OnKill(() => _knockbackSequence = null).
                Play();
        }
        #endregion

        protected EffectList _effectList;
        protected ArtifactList _artifactList;

        public EffectList Effect => _effectList;
        public ArtifactList Artifact => _artifactList;

        public virtual void Init() {
            _effectList = new EffectList(this);
            _artifactList = new ArtifactList(this);
            HP = MaxHP;
            colData.Add(Col2D, this);
            rb2DData.Add(RB2D, this);

            if (IsNPC) {
                _behaviorTree.EnableBehavior();
            }

            IsInit = true;
        }

        public virtual void Dead() {
            IsDead = true;
        }

        public virtual void Dispose() {
            IsDispose = true;
        }

        public virtual void AttackToTarget(Character target, UnityAction onComplete) {
            onComplete?.Invoke();
        }

        public virtual void Deadly(Entity 원인Entity, int dataIndex) {
            DataEntity 사망체크 = DataEntity.고유데이터(1);
            CallOn사망예정(this, 원인Entity, dataIndex, 사망체크);
            if (HP <= 0 && 사망체크.데이터 == 1) {
                HP = 0;
                Dead();
                Dispose();          // 언데드일경우 Dispose는 하지 않지만, 일단은 보류
                CallOn사망(this, 원인Entity, dataIndex);
            }
        }

        public virtual void KnockDown(float time) {
            if (IsKnockDown &&
                _knockdownTime > time) {
                return;
            }

            if (IsKnockDown) {
                StopCoroutine(_knockdownRoutine);
                _knockdownRoutine = null;
            }

            IsKnockDown = true;
            Graphic.SetState_KnockDown(IsKnockDown);
            _knockdownRoutine = StartCoroutine(KnockDownRoutine(time));

            IEnumerator KnockDownRoutine(float time) {
                _knockdownTime = time;
                while (_knockdownTime > 0) {
                    _knockdownTime -= Time.deltaTime;
                    yield return null;
                }
                _knockdownTime = 0;
                IsKnockDown = false;
                Graphic.SetState_KnockDown(IsKnockDown);
                _knockdownRoutine = null;
            }
        }

        #region Entity 콜백

        public OnExecute엔티티 on피해예정;
        public void CallOn피해예정(Entity entity, int dataIndex) {
            on피해예정?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on피해;
        public void CallOn피해(Entity entity, int dataIndex) {
            on피해?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on피해입고생존;
        public void CallOn피해입고생존(Entity entity, int dataIndex) {
            on피해입고생존?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on추가피해예정;
        public void CallOn추가피해예정(Entity entity, int dataIndex) {
            on추가피해예정?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on추가피해;
        public void CallOn추가피해(Entity entity, int dataIndex) {
            on추가피해?.Invoke(entity, dataIndex);
        }

        public OnExecute엔티티 on회복예정;
        public void CallOn회복예정(Entity entity, int dataIndex) {
            on회복예정?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on회복;
        public void CallOn회복(Entity entity, int dataIndex) {
            on회복?.Invoke(entity, dataIndex);
        }

        public OnExecute엔티티 on경직예정;
        public void CallOn경직예정(Entity entity, int dataIndex) {
            on경직예정?.Invoke(entity, dataIndex);
        }

        public OnExecute엔티티 on경직;
        public void CallOn경직(Entity entity, int dataIndex) {
            on경직?.Invoke(entity, dataIndex);
        }

        public OnExecute엔티티 on넉백예정;
        public void CallOn넉백예정(Entity entity, int dataIndex) {
            on넉백예정?.Invoke(entity, dataIndex);
        }

        public OnExecute엔티티 on넉백;
        public void CallOn넉백(Entity entity, int dataIndex) {
            on넉백?.Invoke(entity, dataIndex);
        }

        public OnExecute엔티티 on효과부여예정_선처리;
        public OnExecute엔티티 on효과부여예정;
        public OnExecute엔티티 on효과부여예정_면역체크;
        public OnExecute엔티티 on효과부여예정_후처리;
        public void CallOn효과부여예정(Entity entity, int dataIndex) {
            on효과부여예정_선처리?.Invoke(entity, dataIndex);
            on효과부여예정_면역체크?.Invoke(entity, dataIndex);
            on효과부여예정?.Invoke(entity, dataIndex);
            on효과부여예정_후처리?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on효과회수예정;
        public void CallOn효과회수예정(Entity entity, int dataIndex) {
            on효과회수예정?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on효과부여;
        public void CallOn효과부여(Entity entity, int dataIndex) {
            on효과부여?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on효과회수;
        public void CallOn효과회수(Entity entity, int dataIndex) {
            on효과회수?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on효과제거;
        public void CallOn효과제거(Entity entity, int dataIndex) {
            on효과제거?.Invoke(entity, dataIndex);
        }
        public OnExecute엔티티 on효과회수후제거;
        public void CallOn효과회수후제거(Entity entity, int dataIndex) {
            on효과회수후제거?.Invoke(entity, dataIndex);
        }

        public OnExecute엔티티_AffectData on사망예정;

        public void CallOn사망예정(Character target, Entity entity, int dataIndex, DataEntity data) {
            on사망예정?.Invoke(target, entity, dataIndex, data);
        }

        public OnExecute엔티티_Affect on사망;
        public void CallOn사망(Character target, Entity entity, int dataIndex) {
            on사망?.Invoke(target, entity, dataIndex);
        }

        #endregion

        #region Entity 액션

        public void Apply피해(Entity 피해Entity, int dataIndex) {
            if (IsDead) return;

            if (피해Entity.Has주체 &&
                피해Entity.주체캐릭터 != null) {
                피해Entity.주체캐릭터.CallOn피해예정(피해Entity, dataIndex);
            }

            DataEntity 피해Data = 피해Entity.GetData(dataIndex);

            if (피해Data.Property.HasFlag(EntityProperty.방어무시) == false) {
                피해Data.Add추가량(-DEF);
            }

            CallOn피해예정(피해Entity, dataIndex);

            int 피해량 = 피해Data.데이터;
            if (피해량 < 0) 피해량 = 0;

            int 기존HP = HP;

            HP -= 피해량;

            if (HP < 0) {
                HP = 0;
            }   

            피해Data.SetResultData_HP(기존HP, HP);
            onHPValueChanged?.Invoke(기존HP, HP);

            if (피해량 > 0) {
                if (피해Entity.Has주체 &&
                    피해Entity.주체캐릭터 != null) {
                    피해Entity.주체캐릭터.CallOn피해(피해Entity, dataIndex);
                }
                CallOn피해(피해Entity, dataIndex);
            }

            if (HP <= 0 && 피해량 > 0) {
                Deadly(피해Entity, dataIndex);
                if (IsDead) return;
            }
        }

        public void Apply추가피해(Entity 피해Entity, int dataIndex) {
            if (IsDead) return;

            if (피해Entity.Has주체 &&
                피해Entity.주체캐릭터 != null) {
                피해Entity.주체캐릭터.CallOn추가피해예정(피해Entity, dataIndex);
            }

            DataEntity 피해Data = 피해Entity.GetData(dataIndex);

            if (피해Data.Property.HasFlag(EntityProperty.방어무시) == false) {
                피해Data.Add추가량(-DEF);
            }

            CallOn추가피해예정(피해Entity, dataIndex);

            int 피해량 = 피해Data.데이터;
            if (피해량 < 0) 피해량 = 0;

            int 기존HP = HP;

            HP -= 피해량;

            if (HP < 0) {
                HP = 0;
            }

            피해Data.SetResultData_HP(기존HP, HP);
            onHPValueChanged?.Invoke(기존HP, HP);

            if (피해량 > 0) {
                if (피해Entity.Has주체 &&
                    피해Entity.주체캐릭터 != null) {
                    피해Entity.주체캐릭터.CallOn추가피해(피해Entity, dataIndex);
                }
                CallOn추가피해(피해Entity, dataIndex);
            }

            if (HP <= 0 && 피해량 > 0) {
                Deadly(피해Entity, dataIndex);
                if (IsDead) return;
            }
        }

        public void Apply경직(Entity 경직Entity, int dataIndex) {
            if (IsDead) return;

            if (경직Entity.Has주체 &&
                경직Entity.주체캐릭터 != null) {
                경직Entity.주체캐릭터.CallOn경직예정(경직Entity, dataIndex);
            }

            DataEntity 경직Data = 경직Entity.GetData(dataIndex);
            경직Data.Add추가량((int)-Poise);

            CallOn경직예정(경직Entity, dataIndex);

            int 경직도 = 경직Data.데이터;

            if (경직도 > 0) {
                KnockDown(경직Data.실수데이터);
                if (경직Entity.Has주체 &&
                    경직Entity.주체캐릭터 != null) {
                    경직Entity.주체캐릭터.CallOn경직(경직Entity, dataIndex);
                }
                CallOn경직(경직Entity, dataIndex);
            }
        }

        public void Apply넉백(Entity 넉백Entity, int dataIndex) {
            if (IsDead) return;

            if (넉백Entity.Has주체 &&
                넉백Entity.주체캐릭터 != null) {
                넉백Entity.주체캐릭터.CallOn넉백예정(넉백Entity, dataIndex);
            }

            DataEntity 넉백Data = 넉백Entity.GetData(dataIndex);
            넉백Data.Add실수추가량(-Weight);

            CallOn넉백예정(넉백Entity, dataIndex);

            float 넉백힘 = 넉백Data.실수데이터;

            if (넉백힘 > 0) {
                KnockBack(
                    new Vector2(넉백Entity.발생위치.x - transform.position.x, 넉백Entity.발생위치.y - transform.position.y),
                    넉백힘);
                if (넉백Entity.Has주체 &&
                    넉백Entity.주체캐릭터 != null) {
                    넉백Entity.주체캐릭터.CallOn넉백(넉백Entity, dataIndex);
                }
                CallOn넉백(넉백Entity, dataIndex);
            }
        }

        public void Apply효과(Entity 효과Entity, int dataIndex) {
            DataEntity 효과Data = 효과Entity.GetData(dataIndex);

            int 기존Effect = _effectList.GetEffectValue(효과Data.효과.ID);

            switch (효과Data.Type) {
                case EntityType.효과부여: {
                        if (효과Entity.Has주체) {
                            효과Entity.주체캐릭터.CallOn효과부여예정(효과Entity, dataIndex);
                        }
                        CallOn효과부여예정(효과Entity, dataIndex);
                        if (효과Data.Is효과무효 == false) {
                            _effectList.Effect부여(효과Data.효과);
                            if (효과Entity.Has주체) {
                                효과Entity.주체캐릭터.CallOn효과부여(효과Entity, dataIndex);
                            }
                            CallOn효과부여(효과Entity, dataIndex);
                        }
                    }
                    break;
                case EntityType.효과회수: {
                        if (효과Entity.Has주체) {
                            효과Entity.주체캐릭터.CallOn효과회수예정(효과Entity, dataIndex);
                        }
                        CallOn효과회수예정(효과Entity, dataIndex);
                        if (효과Data.Is효과무효 == false) {
                            if (효과Data.효과.Attribute.HasFlag(EffectAttribute.음수가능) == false &&
                                _effectList.GetEffectValue(효과Data.효과.ID) <= 효과Data.효과.Value) {
                                if (효과Entity.Has주체) {
                                    효과Entity.주체캐릭터.CallOn효과회수후제거(효과Entity, dataIndex);
                                }
                                CallOn효과회수후제거(효과Entity, dataIndex);
                            }
                            _effectList.Effect회수(효과Data.효과);
                            if (효과Entity.Has주체) {
                                효과Entity.주체캐릭터.CallOn효과회수(효과Entity, dataIndex);
                            }
                            CallOn효과회수(효과Entity, dataIndex);
                        }
                    }
                    break;
                case EntityType.효과제거: {
                        if (기존Effect != 0) {
                            if (효과Entity.Has주체) {
                                효과Entity.주체캐릭터.CallOn효과회수후제거(효과Entity, dataIndex);
                            }
                            CallOn효과회수후제거(효과Entity, dataIndex);
                            _effectList.Effect제거(효과Data.효과.ID);
                            if (효과Entity.Has주체) {
                                효과Entity.주체캐릭터.CallOn효과제거(효과Entity, dataIndex);
                            }
                            CallOn효과제거(효과Entity, dataIndex);
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

            if (회복Entity.Has주체) {
                회복Entity.주체캐릭터.CallOn회복예정(회복Entity, dataIndex);
            }
            CallOn회복예정(회복Entity, dataIndex);

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
                if (회복Entity.Has주체) {
                    회복Entity.주체캐릭터.CallOn회복(회복Entity, dataIndex);
                }
                CallOn회복(회복Entity, dataIndex);
            }
        }

        #endregion

        private void OnDestroy() {
            colData.Remove(Col2D);
            rb2DData.Remove(RB2D);
        }
    }
}

