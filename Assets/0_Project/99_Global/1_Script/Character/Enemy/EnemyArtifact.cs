using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TenMinute {
    public class EnemyArtifact : MonoBehaviour {
        [SerializeField]
        private Enemy enemy;
        // Start is called before the first frame update
        void Start() {
            enemy.Artifact.Artifact추가(Data.ArtifactID.시간파편);
        }
    }
}
