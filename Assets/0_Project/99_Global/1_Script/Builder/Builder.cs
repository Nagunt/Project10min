using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Data;
using System;

namespace TenMinute {
    public partial class Artifact {
        public static Artifact Create(ArtifactID id) {
            Artifact value = null;
            try {
                string name = $"TenMinute.Artifact_{id}";
                Type artifactType = Type.GetType(name);
                if (Activator.CreateInstance(artifactType) is Artifact newArtifact) {
                    value = newArtifact;
                }
            }
            catch (Exception) {
                Debug.Log($"{id} 에 해당하는 유물이 존재하지 않음.");
                return null;
            }
            return value;
        }
    }

    public partial class Effect {
        public static Effect Create(EffectID id) {
            Effect value = null;
            try {
                string name = $"TenMinute.Effect_{id}";
                Type effectType = Type.GetType(name);
                if (Activator.CreateInstance(effectType) is Effect newEffect) {
                    value = newEffect;
                }
            }
            catch (Exception) {
                Debug.Log($"{id} 에 해당하는 효과가 존재하지 않음.");
                return null;
            }
            return value;
        }
    }
}