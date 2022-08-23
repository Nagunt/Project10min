using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {
    public sealed class Effect_ȿ��1 : Effect {
        public Effect_ȿ��1(int value) : base(value,
            EffectID.ȿ��1) { }

        public override void Active(Character owner) {
            base.Active(owner);
            Owner.onCalcDEF��ġ += OnCalcDEF��ġ;
        }

        public override void Dispose() {
            base.Dispose();
            Owner.onCalcDEF��ġ -= OnCalcDEF��ġ;
            Debug.Log("ȿ��1����");
        }

        private int OnCalcDEF��ġ() {
            return 10;
        }
    }

    public sealed class Effect_ȿ��2 : Effect {
        public Effect_ȿ��2(int value) : base(value,
            EffectID.ȿ��2) { }

        public override void Active(Character owner) {
            base.Active(owner);
            Owner.onCalcATKSpeed��ġ += OnCalcATKSpeed��ġ;
        }

        public override void Dispose() {
            base.Dispose();
            Owner.onCalcATKSpeed��ġ -= OnCalcATKSpeed��ġ;
        }

        private float OnCalcATKSpeed��ġ() {
            return 0.5f;
        }
    }
}
