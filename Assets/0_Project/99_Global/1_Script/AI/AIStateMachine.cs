using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.AI {

    [System.Serializable]
    public struct AIStateData {
        public string key;
        public AIState state;
        public bool isWaitForEnd;
        public AIConditionData[] conditions;
    }

    [System.Serializable]
    public struct AIConditionData {
        public string target;
        public string checker;
        public bool value;
    }
    public class AIStateMachine : MonoBehaviour {
        [SerializeField]
        private string _defaultKey;
        [SerializeField]
        private AIStateData[] stateData;
        private Dictionary<string, int> indexData;
        private Dictionary<string, AICondition> condData;
        public AIState Current => CurrentData.state;
        [field:SerializeField]
        public AIStateData CurrentData { get; private set; }
        public Character Owner { get; private set; }
        [field:SerializeField]
        public Transform Target { get; private set; }

        public void SetTarget(Transform target) {
            Target = target;
        }

        protected int GetIndex(string key) {
            if (indexData != null &&
                indexData.TryGetValue(key, out int value)) {
                return value;
            }
            return 0;
        }

        protected AICondition GetChecker(string key) {
            if (condData != null &&
                condData.TryGetValue(key, out AICondition value)) {
                return value;
            }
            return null;
        }

        public virtual void Init(Character owner) {
            Owner = owner;
            condData = new Dictionary<string, AICondition>();
            AICondition[] conds = GetComponents<AICondition>();
            for (int j = 0; j < conds.Length; ++j) {
                conds[j].Init(this);
                if (condData.ContainsKey(conds[j].Key) == false) {
                    condData.Add(conds[j].Key, conds[j]);
                }
            }
            if (stateData.Length > 0) {
                indexData = new Dictionary<string, int>();
                for (int i = 0; i < stateData.Length; ++i) {
                    stateData[i].state.Init(this);
                    if (indexData.ContainsKey(stateData[i].key) == false) {
                        indexData.Add(stateData[i].key, i);
                    }
                }
                CurrentData = string.IsNullOrEmpty(_defaultKey) ? stateData[0] : stateData[GetIndex(_defaultKey)];
                StartCoroutine(AIRoutine());
            }
        }

        private IEnumerator AIRoutine() {
            while (true) {
                Current.OnStart();
                while (true) {
                    string target = string.Empty;

                    for (int i = 0; i < CurrentData.conditions.Length; ++i) {
                        if (GetChecker(CurrentData.conditions[i].checker).Check() ==
                            CurrentData.conditions[i].value) {
                            target = CurrentData.conditions[i].target;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(target)) {
                        if (Current.IsRunning == false) {
                            Current.Execute();
                        }
                        if (CurrentData.isWaitForEnd) {
                            yield return new WaitUntil(() => Current.IsRunning == false);
                        }
                    }
                    else {
                        Current.OnExit();
                        CurrentData = stateData[GetIndex(target)];
                        break;
                    }

                    yield return null;
                }
            }
        }
    }
}
