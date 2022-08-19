using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {
    public sealed class Effect_효과1 : Effect {
        public Effect_효과1() :
            base(EffectID.효과1) { }

        public override void OnEnable() {
            Owner.onCalcDEF수치 += OnCalcDEF수치;
            base.OnEnable();
        }

        public override void OnDisable() {
            Owner.onCalcDEF수치 -= OnCalcDEF수치;
            base.OnDisable();
        }

        private int OnCalcDEF수치() {
            return 10;
        }
    }

    public sealed class Effect_효과2 : Effect {
        public Effect_효과2() :
            base(EffectID.효과2) { }

        public override void OnEnable() {
            Owner.onCalcATKSpeed수치 += OnCalcATKSpeed수치;
            base.OnEnable();
        }

        public override void OnDisable() {
            Owner.onCalcATKSpeed수치 -= OnCalcATKSpeed수치;
            base.OnDisable();
        }

        private float OnCalcATKSpeed수치() {
            return 0.5f;
        }
    }
}
