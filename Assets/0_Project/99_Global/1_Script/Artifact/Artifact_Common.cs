using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {

    public sealed class Artifact_아티1 : Artifact {
        public Artifact_아티1() :
            base(ArtifactID.아티1) { }

        public override void OnEnable() {
            Owner.onCalcSpeed수치 += OnCalcSpeed수치;
        }

        public override void OnDisable() {
            Owner.onCalcSpeed수치 -= OnCalcSpeed수치;
        }

        private float OnCalcSpeed수치() {
            return 1.5f;
        }
    }

    public sealed class Artifact_아티2 : Artifact {
        public Artifact_아티2() :
            base(ArtifactID.아티2) { }

        public override void OnEnable() {
            Owner.onCalcATK수치 += OnCalcATK수치;
        }

        public override void OnDisable() {
            Owner.onCalcATK수치 -= OnCalcATK수치;
        }

        private int OnCalcATK수치() {
            return 5;
        }
    }



}