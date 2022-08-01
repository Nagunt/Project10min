using System.Collections;
using System.Collections.Generic;
using TenMinute.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TenMinute {
    public class Global_Entry : MonoBehaviour {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void FirstLoad() {
            Debug.Log("��Ʈ�� �ʱ�ȭ");
            Application.targetFrameRate = 60;
            Global_EventSystem.Init();
            SceneManager.LoadScene("Entry");
        }
    }
}
