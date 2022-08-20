using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {

    public sealed class Artifact_��Ƽ1 : Artifact {
        public Artifact_��Ƽ1() :
            base(ArtifactID.��Ƽ1) { }

        public override void OnEnable() {
            Owner.onCalcSpeed��ġ += OnCalcSpeed��ġ;
        }

        public override void OnDisable() {
            Owner.onCalcSpeed��ġ -= OnCalcSpeed��ġ;
        }

        private float OnCalcSpeed��ġ() {
            return 1.5f;
        }
    }

    public sealed class Artifact_��Ƽ2 : Artifact {
        public Artifact_��Ƽ2() :
            base(ArtifactID.��Ƽ2) { }

        public override void OnEnable() {
            Owner.onCalcATK��ġ += OnCalcATK��ġ;
        }

        public override void OnDisable() {
            Owner.onCalcATK��ġ -= OnCalcATK��ġ;
        }

        private int OnCalcATK��ġ() {
            return 5;
        }
    }



}