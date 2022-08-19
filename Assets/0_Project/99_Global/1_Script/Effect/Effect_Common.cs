using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {
    public sealed class Effect_ȿ��1 : Effect {
        public Effect_ȿ��1() :
            base(EffectID.ȿ��1) { }

        public override void OnEnable() {
            Owner.onCalcDEF��ġ += OnCalcDEF��ġ;
            base.OnEnable();
        }

        public override void OnDisable() {
            Owner.onCalcDEF��ġ -= OnCalcDEF��ġ;
            base.OnDisable();
        }

        private int OnCalcDEF��ġ() {
            return 10;
        }
    }

    public sealed class Effect_ȿ��2 : Effect {
        public Effect_ȿ��2() :
            base(EffectID.ȿ��2) { }

        public override void OnEnable() {
            Owner.onCalcATKSpeed��ġ += OnCalcATKSpeed��ġ;
            base.OnEnable();
        }

        public override void OnDisable() {
            Owner.onCalcATKSpeed��ġ -= OnCalcATKSpeed��ġ;
            base.OnDisable();
        }

        private float OnCalcATKSpeed��ġ() {
            return 0.5f;
        }
    }
}
