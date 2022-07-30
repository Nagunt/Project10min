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
                return Mathf.RoundToInt(스텟Speed * 증감비율 + 증감수치);
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
                return Mathf.RoundToInt(스텟Poise * 증감비율 + 증감수치);
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
                return Mathf.RoundToInt(스텟Weight * 증감비율 + 증감수치);
            }
        }

        // 모든 계산식의 원본이 되는 수치. 
        // 이 수치는 게임 상에서 다른 증감수치에 의해서 변동되선 안된다.

        protected int 스텟HP;
        protected int 스텟ATK;
        protected int 스텟DEF;
        protected float 스텟Speed;
        protected float 스텟Poise;
        protected float 스텟Weight;

        // 스텟의 값을 결정하는데 사용될 추가 요소들.

        public Func<int> onCalcMaxHP수치;
        public Func<int> onCalcATK수치;
        public Func<int> onCalcDEF수치;
        public Func<float> onCalcSpeed수치;
        public Func<float> onCalcPoise수치;
        public Func<float> onCalcWeight수치;

        public Func<float> onCalcMaxHP비율;
        public Func<float> onCalcATK비율;
        public Func<float> onCalcDEF비율;
        public Func<float> onCalcSpeed비율;
        public Func<float> onCalcPoise비율;
        public Func<float> onCalcWeight비율;

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

