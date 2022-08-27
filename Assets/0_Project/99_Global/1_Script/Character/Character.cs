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

namespace TenMinute {
    public class Character : MonoBehaviour {
        private static Dictionary<Rigidbody2D, Character> rb2DData = new Dictionary<Rigidbody2D, Character>();

        public static Character GetCharacter(Rigidbody2D rb2D) {
            if (rb2DData.TryGetValue(rb2D, out Character value)) {
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

        // UI��� ����� Callback

        public Action<int, int> onHPValueChanged;

        #endregion

        [Header("- AI")]
        [SerializeField] protected bool _isNPC;
        [SerializeField] protected BehaviorTree _behaviorTree;

        public CharacterID ID => _id;
        public Graphic_Character Graphic => _graphic;
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
            Graphic.SetState_Move(true);
        }
        public virtual void Stop() {
            RB2D.velocity = Vector2.zero;
            Graphic.SetState_Move(false);
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
            rb2DData.Add(RB2D, this);
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

        #region Entity �ݹ�

        public OnExecute��ƼƼ on���ؿ���;
        public void CallOn���ؿ���(Entity entity, int dataIndex) {
            on���ؿ���?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ on����;
        public void CallOn����(Entity entity, int dataIndex) {
            on����?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ on�����԰����;
        public void CallOn�����԰����(Entity entity, int dataIndex) {
            on�����԰����?.Invoke(entity, dataIndex);
        }

        public OnExecute��ƼƼ onȸ������;
        public void CallOnȸ������(Entity entity, int dataIndex) {
            onȸ������?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ onȸ��;
        public void CallOnȸ��(Entity entity, int dataIndex) {
            onȸ��?.Invoke(entity, dataIndex);
        }

        public OnExecute��ƼƼ onȿ���ο�����_��ó��;
        public OnExecute��ƼƼ onȿ���ο�����;
        public OnExecute��ƼƼ onȿ���ο�����_�鿪üũ;
        public OnExecute��ƼƼ onȿ���ο�����_��ó��;
        public void CallOnȿ���ο�����(Entity entity, int dataIndex) {
            onȿ���ο�����_��ó��?.Invoke(entity, dataIndex);
            onȿ���ο�����_�鿪üũ?.Invoke(entity, dataIndex);
            onȿ���ο�����?.Invoke(entity, dataIndex);
            onȿ���ο�����_��ó��?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ onȿ��ȸ������;
        public void CallOnȿ��ȸ������(Entity entity, int dataIndex) {
            onȿ��ȸ������?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ onȿ���ο�;
        public void CallOnȿ���ο�(Entity entity, int dataIndex) {
            onȿ���ο�?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ onȿ��ȸ��;
        public void CallOnȿ��ȸ��(Entity entity, int dataIndex) {
            onȿ��ȸ��?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ onȿ������;
        public void CallOnȿ������(Entity entity, int dataIndex) {
            onȿ������?.Invoke(entity, dataIndex);
        }
        public OnExecute��ƼƼ onȿ��ȸ��������;
        public void CallOnȿ��ȸ��������(Entity entity, int dataIndex) {
            onȿ��ȸ��������?.Invoke(entity, dataIndex);
        }

        #endregion

        #region Entity �׼�

        public void Apply����(Entity ����Entity, int dataIndex) {
            if (IsDead) return;

            if (����Entity.Has��ü) {
                ����Entity.��üĳ����.CallOn���ؿ���(����Entity, dataIndex);
            }

            DataEntity ����Data = ����Entity.GetData(dataIndex);

            if (����Data.Property.HasFlag(EntityProperty.����) == false) {
                ����Data.Add�߰���(-DEF);
            }

            CallOn���ؿ���(����Entity, dataIndex);

            int ���ط� = ����Data.������;
            if (���ط� < 0) ���ط� = 0;

            int ����HP = HP;

            HP -= ���ط�;

            if (HP < 0) {
                HP = 0;
            }   

            ����Data.SetResultData_HP(����HP, HP);
            onHPValueChanged?.Invoke(����HP, HP);

            if (���ط� > 0) {
                if (����Entity.Has��ü) {
                    ����Entity.��üĳ����.CallOn����(����Entity, dataIndex);
                }
                CallOn����(����Entity, dataIndex);
            }

            if (HP <= 0 && ���ط� > 0) {
                Dead();
                if (IsDead) return;
            }
        }

        public void Apply����(Entity ����Entity, int dataIndex) {
            DataEntity ����Data = ����Entity.GetData(dataIndex);
        }

        public void Apply�˹�(Entity �˹�Entity, int dataIndex) {
            DataEntity �˹�Data = �˹�Entity.GetData(dataIndex);
        }

        public void Applyȿ��(Entity ȿ��Entity, int dataIndex) {
            DataEntity ȿ��Data = ȿ��Entity.GetData(dataIndex);

            int ����Effect = _effectList.GetEffectValue(ȿ��Data.ȿ��.ID);

            switch (ȿ��Data.Type) {
                case EntityType.ȿ���ο�: {
                        if (ȿ��Entity.Has��ü) {
                            ȿ��Entity.��üĳ����.CallOnȿ���ο�����(ȿ��Entity, dataIndex);
                        }
                        CallOnȿ���ο�����(ȿ��Entity, dataIndex);
                        if (ȿ��Data.Isȿ����ȿ == false) {
                            _effectList.Effect�ο�(ȿ��Data.ȿ��);
                            if (ȿ��Entity.Has��ü) {
                                ȿ��Entity.��üĳ����.CallOnȿ���ο�(ȿ��Entity, dataIndex);
                            }
                            CallOnȿ���ο�(ȿ��Entity, dataIndex);
                        }
                    }
                    break;
                case EntityType.ȿ��ȸ��: {
                        if (ȿ��Entity.Has��ü) {
                            ȿ��Entity.��üĳ����.CallOnȿ��ȸ������(ȿ��Entity, dataIndex);
                        }
                        CallOnȿ��ȸ������(ȿ��Entity, dataIndex);
                        if (ȿ��Data.Isȿ����ȿ == false) {
                            if (ȿ��Data.ȿ��.Attribute.HasFlag(EffectAttribute.��������) == false &&
                                _effectList.GetEffectValue(ȿ��Data.ȿ��.ID) <= ȿ��Data.ȿ��.Value) {
                                if (ȿ��Entity.Has��ü) {
                                    ȿ��Entity.��üĳ����.CallOnȿ��ȸ��������(ȿ��Entity, dataIndex);
                                }
                                CallOnȿ��ȸ��������(ȿ��Entity, dataIndex);
                            }
                            _effectList.Effectȸ��(ȿ��Data.ȿ��);
                            if (ȿ��Entity.Has��ü) {
                                ȿ��Entity.��üĳ����.CallOnȿ��ȸ��(ȿ��Entity, dataIndex);
                            }
                            CallOnȿ��ȸ��(ȿ��Entity, dataIndex);
                        }
                    }
                    break;
                case EntityType.ȿ������: {
                        if (����Effect != 0) {
                            if (ȿ��Entity.Has��ü) {
                                ȿ��Entity.��üĳ����.CallOnȿ��ȸ��������(ȿ��Entity, dataIndex);
                            }
                            CallOnȿ��ȸ��������(ȿ��Entity, dataIndex);
                            _effectList.Effect����(ȿ��Data.ȿ��.ID);
                            if (ȿ��Entity.Has��ü) {
                                ȿ��Entity.��üĳ����.CallOnȿ������(ȿ��Entity, dataIndex);
                            }
                            CallOnȿ������(ȿ��Entity, dataIndex);
                        }
                    }
                    break;
            }

            ȿ��Data.SetResultData_Effect(����Effect, _effectList.GetEffectValue(ȿ��Data.ȿ��.ID));
        }

        public void ApplyHP����(Entity entity, int dataIndex) {
            DataEntity ����Data = entity.GetData(dataIndex);
            int ����HP = HP;
            HP = ����Data.������;
            ����Data.SetResultData_HP(����HP, HP);
        }

        public void ApplyHPȸ��(Entity ȸ��Entity, int dataIndex) {
            if (IsDead) return;

            if (ȸ��Entity.Has��ü) {
                ȸ��Entity.��üĳ����.CallOnȸ������(ȸ��Entity, dataIndex);
            }
            CallOnȸ������(ȸ��Entity, dataIndex);

            DataEntity ȸ��Data = ȸ��Entity.GetData(dataIndex);
            int ȸ���� = ȸ��Data.������;
            if (ȸ���� < 0) ȸ���� = 0;

            int ����HP = HP;

            HP += ȸ����;

            if (HP > MaxHP) {
                HP = MaxHP;
            }

            ȸ��Data.SetResultData_HP(����HP, HP);

            if (ȸ���� > 0) {
                if (ȸ��Entity.Has��ü) {
                    ȸ��Entity.��üĳ����.CallOnȸ��(ȸ��Entity, dataIndex);
                }
                CallOnȸ��(ȸ��Entity, dataIndex);
            }
        }

        #endregion

        private void OnDestroy() {
            rb2DData.Remove(RB2D);
        }
    }
}

