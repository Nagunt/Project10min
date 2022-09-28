using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;

namespace TenMinute {

    [System.Flags]
    public enum ArtifactAttribute {
        None = 0,
    }
    public class ArtifactList {

        private Character _owner;
        private Dictionary<ArtifactID, Artifact> _data;
        public ArtifactList(Character owner) {
            _owner = owner;
            _data = new Dictionary<ArtifactID, Artifact>();
        }

        public bool HasÀ¯¹°(ArtifactID artifact) => _data.ContainsKey(artifact);
        public Artifact GetArtifact(ArtifactID artifact) {
            if (_data.TryGetValue(artifact, out Artifact target)) {
                return target;
            }
            return null;
        }
        private void AddArtifact(Artifact artifact) {
            _data.Add(artifact.ID, artifact);
            artifact.OnObtain(_owner);
            artifact.OnEnable();
        }
        private void RemoveArtifact(ArtifactID artifact) {
            Artifact target = GetArtifact(artifact);
            _data.Remove(artifact);
            target.OnDisable();
        }
        public void ArtifactÃß°¡(ArtifactID artifact) {
            if (HasÀ¯¹°(artifact)) return;
            AddArtifact(Artifact.Create(artifact));
        }
        public void ArtifactÁ¦°Å(ArtifactID artifact) {
            if (HasÀ¯¹°(artifact)) {
                RemoveArtifact(artifact);
            }
        }
    }

    public partial class Artifact {
        private ArtifactID _id;
        private ArtifactAttribute _attribute;
        private Character _owner;
        private int _value;
        private int[] _artifactValues;

        public ArtifactID ID => _id;
        public ArtifactAttribute Attribute => _attribute;
        public Character Owner => _owner;
        public int Value => _value;
        public int[] ArtifactValues => _artifactValues;
        protected Artifact(ArtifactID id, ArtifactAttribute attribute = ArtifactAttribute.None) {
            _id = id;
            _attribute = attribute;
        }
        public virtual void OnObtain(Character owner) {
            Debug.Log(owner + " ¾ÆÆ¼ÆÑÆ® È¹µæ");
            _owner = owner;
        }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}
