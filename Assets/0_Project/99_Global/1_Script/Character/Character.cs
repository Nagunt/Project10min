using System;
using System.Linq;
using UnityEngine;

namespace TenMinute {
    public class Character : MonoBehaviour {

        [SerializeField]
        protected Rigidbody2D rb2D;

        #region Status

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
                return Mathf.RoundToInt(����Speed * �������� + ������ġ);
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
                return Mathf.RoundToInt(����Poise * �������� + ������ġ);
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
                return Mathf.RoundToInt(����Weight * �������� + ������ġ);
            }
        }

        // ��� ������ ������ �Ǵ� ��ġ. 
        // �� ��ġ�� ���� �󿡼� �ٸ� ������ġ�� ���ؼ� �����Ǽ� �ȵȴ�.

        protected int ����HP;
        protected int ����ATK;
        protected int ����DEF;
        protected float ����Speed;
        protected float ����Poise;
        protected float ����Weight;

        // ������ ���� �����ϴµ� ���� �߰� ��ҵ�.

        public Func<int> onCalcMaxHP��ġ;
        public Func<int> onCalcATK��ġ;
        public Func<int> onCalcDEF��ġ;
        public Func<float> onCalcSpeed��ġ;
        public Func<float> onCalcPoise��ġ;
        public Func<float> onCalcWeight��ġ;

        public Func<float> onCalcMaxHP����;
        public Func<float> onCalcATK����;
        public Func<float> onCalcDEF����;
        public Func<float> onCalcSpeed����;
        public Func<float> onCalcPoise����;
        public Func<float> onCalcWeight����;

        #endregion

        public bool IsAlive => IsInit && IsDead == false && IsDispose == false;
        public bool IsInit { get; protected set; }
        public bool IsDead { get; protected set; }
        public bool IsDispose { get; protected set; }

        public virtual void Init() {
            IsInit = true;
        }

        public virtual void Dispose() {
            IsDispose = true;
        }
    }
}

