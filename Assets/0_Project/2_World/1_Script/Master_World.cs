using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;
using System.Collections.ObjectModel;

namespace TenMinute {
    public class Master_World : MonoBehaviour {
        public static Master_World Instance { get; private set; }
        public static bool GameState { get; private set; }

        public static int EnemyCount {
            get {
                if (Instance.enemyData == null) {
                    return 0;
                }
                return Instance.enemyData.Count;
            }
        }

        private bool isRoomClear = false;
        private bool isStageClear = false;

        private int roomIndex = 0;
        private int stageIndex = 1;

        private WaitUntil waitForStageClear;
        private ReadOnlyDictionary<int, Stage> StageData => StageLoader.Data;

        private HashSet<Character> enemyData;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            Global_EventSystem.Game.onGameStateChanged += OnGameStateChanged;
            Global_EventSystem.Game.onRoomCleared += OnRoomCleared;
            Global_EventSystem.Game.onStageCleared += OnStageCleared;
            Global_EventSystem.Game.onEnemySpawned += OnEnemySpawned;
            Global_EventSystem.Game.onEnemyDead += OnEnemyDead;
        }

        private void Start() {
            StartCoroutine(Routine());
        }

        private void Update() {
            if (isRoomClear == false) {
                // ���⼭ ü�� �����...
            }
        }

        IEnumerator Routine() {
            yield return new WaitForEndOfFrame();
            // �ݹ� ȣ��

            Global_EventSystem.Game.CallOnGameStateChanged(true);

            // ���̺� �ε� ��� �����Ҳ��� ���⼭ ���� �ε�

            waitForStageClear = new WaitUntil(() => isStageClear);

            while (true) {
                if (StageData.TryGetValue(stageIndex, out Stage stage) == false) {
                    Debug.Log($"{stageIndex} : �ش��ϴ� ���������� �������� �ʽ��ϴ�.");
                    break;
                }

                Stage currentStage = Instantiate(stage, transform);

                isRoomClear = false;
                isStageClear = false;

                roomIndex = 0;

                currentStage.Init();

                yield return waitForStageClear;

                // ���⼭ ���� �������� �ε����� ���ϴ� ���� �Ұ�.
                stageIndex++;
                Destroy(currentStage.gameObject);
            }
        }

        #region �ݹ�

        private void OnGameStateChanged(bool state) {
            if (state != GameState) {
                GameState = state;
                Time.timeScale = GameState ? 1f : 0;
            }
        }

        private void OnRoomCleared(int index) {
            isRoomClear = true;
        }

        private void OnStageCleared(int index) {
            isStageClear = true;
        }

        private void OnEnemySpawned(Character target) {
            if (enemyData == null) {
                enemyData = new HashSet<Character>();
            }
            enemyData.Add(target);
        }

        private void OnEnemyDead(Character target) {
            if (enemyData != null) {
                enemyData.Remove(target);
            }
        }

        #endregion

        private void OnDestroy() {
            Global_EventSystem.Game.onGameStateChanged -= OnGameStateChanged;
            StopAllCoroutines();
        }
    }
}

