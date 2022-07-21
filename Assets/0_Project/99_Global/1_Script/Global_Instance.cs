using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TenMinute {
    public class Global_Instance : MonoBehaviour {

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            SceneManager.LoadScene("Title");
        }
    }
}