using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {
    public sealed class Effect_효과1 : Effect {
        public Effect_효과1(int value) : base(value,
            EffectID.효과1) { }

        public override void Active(Character owner) {
            base.Active(owner);
            Owner.onCalcDEF수치 += OnCalcDEF수치;
        }

        public override void Dispose() {
            base.Dispose();
            Owner.onCalcDEF수치 -= OnCalcDEF수치;
            Debug.Log("효과1해제");
        }

        private int OnCalcDEF수치() {
            return 10;
        }
    }

    public sealed class Effect_효과2 : Effect {
        public Effect_효과2(int value) : base(value,
            EffectID.효과2) { }

        public override void Active(Character owner) {
            base.Active(owner);
            Owner.onCalcATKSpeed수치 += OnCalcATKSpeed수치;
        }

        public override void Dispose() {
            base.Dispose();
            Owner.onCalcATKSpeed수치 -= OnCalcATKSpeed수치;
        }

        private float OnCalcATKSpeed수치() {
            return 0.5f;
        }
    }
}
