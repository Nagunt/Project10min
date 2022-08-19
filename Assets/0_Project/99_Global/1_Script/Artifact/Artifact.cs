using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {

    [System.Flags]
    public enum ArtifactAttribute {
        None = 0,
    }
    public partial class Artifact {
        private ArtifactID _id;
        private ArtifactAttribute _attribute;
        private Character _owner;
        public ArtifactID ID => _id;
        public ArtifactAttribute Attribute => _attribute;
        public Character Owner => _owner;
        protected Artifact(ArtifactID id, ArtifactAttribute attribute = ArtifactAttribute.None) {
            _id = id;
            _attribute = attribute;
        }
        public Artifact SetOwner(Character owner) {
            _owner = owner;
            return this;
        }
        public virtual void OnObtain() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}
