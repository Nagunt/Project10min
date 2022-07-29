using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TenMinute.Event;

namespace TenMinute {
    public class Stage : MonoBehaviour {
        [SerializeField]
        private int stageIndex;
        [SerializeField]
        private int roomIndex = 0;
        [SerializeField]
        private int maxRoomIndex = 9;
        [SerializeField]
        private Room_Base[] rooms;

        public Room_Base Current { get; private set; }

        private int nextRoomIndex = 0;

        public void Init() {
            StartCoroutine(StageRoutine());
        }

        private void OnPortalArrived(int index) {
            nextRoomIndex = index;
        }

        IEnumerator StageRoutine() {
            Global_EventSystem.Game.CallOnStageStarted(stageIndex);

            Global_EventSystem.Game.onPortalArrived += OnPortalArrived;

            while (roomIndex < maxRoomIndex) {
                if (Current.gameObject != null) {
                    Destroy(Current.gameObject);
                }
                roomIndex++;
                Current = Instantiate(rooms[nextRoomIndex == 0 ? Random.Range(0, rooms.Length) : nextRoomIndex - 1], transform);
                nextRoomIndex = 0;

                Current.Init();

                Global_EventSystem.Game.CallOnRoomStarted(roomIndex);

                yield return new WaitUntil(() => Current.IsCleared);

                Global_EventSystem.Game.CallOnRoomCleared(roomIndex);

                yield return new WaitUntil(() => nextRoomIndex > 0);
            }

            Global_EventSystem.Game.CallOnStageCleared(stageIndex);
        }

        private void OnDestroy() {
            Global_EventSystem.Game.onPortalArrived -= OnPortalArrived;
        }
    }
}
