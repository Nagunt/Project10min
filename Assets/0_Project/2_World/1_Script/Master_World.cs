using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;
using System.Collections.ObjectModel;

namespace TenMinute {
    public class Master_World : MonoBehaviour {

        public static bool GameState { get; private set; }

        private bool isRoomClear = false;
        private bool isStageClear = false;

        private int roomIndex = 0;
        private int stageIndex = 0;

        private WaitUntil waitForStageClear;
        private ReadOnlyDictionary<int, Stage> StageData => StageLoader.Data;

        private void Start() {
            StartCoroutine(Routine());
        }

        private void Update() {
            if (isRoomClear == false) {
                // 여기서 체력 까던지...
            }
        }

        IEnumerator Routine() {
            yield return new WaitForEndOfFrame();

            // 콜백 등록

            Global_EventSystem.Game.onGameStateChanged += OnGameStateChanged;
            Global_EventSystem.Game.onRoomCleared += OnRoomCleared;
            Global_EventSystem.Game.onStageCleared += OnStageCleared;

            // 콜백 호출

            Global_EventSystem.Game.CallOnGameStateChanged(true);

            // 세이브 로드 기능 구현할꺼면 여기서 정보 로드

            waitForStageClear = new WaitUntil(() => isStageClear);

            while (true) {
                if (StageData.TryGetValue(stageIndex, out Stage stage) == false) {
                    Debug.Log($"{stageIndex} : 해당하는 스테이지가 존재하지 않습니다.");
                    break;
                }

                Stage currentStage = Instantiate(stage, transform);

                isRoomClear = false;
                isStageClear = false;

                roomIndex = 0;

                currentStage.Init();

                yield return waitForStageClear;

                // 여기서 다음 스테이지 인덱스를 정하던 말던 할것.
                stageIndex++;
                Destroy(currentStage.gameObject);
            }
        }

        #region 콜백

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

        #endregion

        private void OnDestroy() {
            Global_EventSystem.Game.onGameStateChanged -= OnGameStateChanged;
            StopAllCoroutines();
        }
    }
}

