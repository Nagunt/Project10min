using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TenMinute.UI;
using UnityEngine;

namespace TenMinute.Event {
    public static class Global_EventSystem {

        public static void Init() {
            UI.Init();
        }
        public static class UI {

            public delegate void UIEvent_0차원();
            public delegate void UIEvent_1차원<T>(T t);
            public delegate void UIEvent_2차원<T, K>(T t, K k);
            public delegate void UIEvent_3차원<T, K, L>(T t, K k, L l);

            private class UIEvent {
                public UIEvent_0차원 callback;
            }
            private class UIEvent<T> : UIEvent {
                public new UIEvent_1차원<T> callback;
            }

            private class UIEvent<T, K> : UIEvent {
                public new UIEvent_2차원<T, K> callback;
            }

            private class UIEvent<T, K, L> : UIEvent {
                public new UIEvent_3차원<T, K, L> callback;
            }

            private static Dictionary<UIEventID, UIEvent> localUIEvent_0;
            private static Dictionary<UIEventID, UIEvent> localUIEvent_1;
            private static Dictionary<UIEventID, UIEvent> localUIEvent_2;
            private static Dictionary<UIEventID, UIEvent> localUIEvent_3;


            private static Dictionary<UIEventID, UIEvent> globalUIEvent_0;
            private static Dictionary<UIEventID, UIEvent> globalUIEvent_1;
            private static Dictionary<UIEventID, UIEvent> globalUIEvent_2;
            private static Dictionary<UIEventID, UIEvent> globalUIEvent_3;



            public static void Call(UIEventID id) {
                Dictionary<UIEventID, UIEvent> targetDictionary;
                targetDictionary = globalUIEvent_0;
                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent).callback?.Invoke();
                }
                targetDictionary = localUIEvent_0;
                if (targetDictionary.TryGetValue(id, out UIEvent value2)) {
                    (value2 as UIEvent).callback?.Invoke();
                }
            }
            public static void Call<T>(UIEventID id, T t) {
                Dictionary<UIEventID, UIEvent> targetDictionary;
                targetDictionary = globalUIEvent_1;
                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent<T>).callback?.Invoke(t);
                }
                targetDictionary = localUIEvent_1;
                if (targetDictionary.TryGetValue(id, out UIEvent value2)) {
                    (value2 as UIEvent<T>).callback?.Invoke(t);
                }
            }
            public static void Call<T, K>(UIEventID id, T t, K k) {
                Dictionary<UIEventID, UIEvent> targetDictionary;
                targetDictionary = globalUIEvent_2;
                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent<T, K>).callback?.Invoke(t, k);
                }
                targetDictionary = localUIEvent_2;
                if (targetDictionary.TryGetValue(id, out UIEvent value2)) {
                    (value2 as UIEvent<T, K>).callback?.Invoke(t, k);
                }
            }
            public static void Call<T, K, L>(UIEventID id, T t, K k, L l) {
                Dictionary<UIEventID, UIEvent> targetDictionary;
                targetDictionary = globalUIEvent_3;
                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent<T, K, L>).callback?.Invoke(t, k, l);
                }
                targetDictionary = localUIEvent_3;
                if (targetDictionary.TryGetValue(id, out UIEvent value2)) {
                    (value2 as UIEvent<T, K, L>).callback?.Invoke(t, k, l);
                }
            }

            public static void Register(UIEventID id, UIEvent_0차원 action, bool isGlobal = false) {
                Dictionary<UIEventID, UIEvent> targetDictionary;

                if (isGlobal) {
                    targetDictionary = globalUIEvent_0;
                }
                else {
                    targetDictionary = localUIEvent_0;
                }

                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent).callback += action;
                }
                else {
                    UIEvent newEvent = new UIEvent();
                    newEvent.callback += action;
                    targetDictionary.Add(id, newEvent);
                }
            }
            public static void Register<T>(UIEventID id, UIEvent_1차원<T> action, bool isGlobal = false) {
                Dictionary<UIEventID, UIEvent> targetDictionary;

                if (isGlobal) {
                    targetDictionary = globalUIEvent_1;
                }
                else {
                    targetDictionary = localUIEvent_1;
                }

                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent<T>).callback += action;
                }
                else {
                    UIEvent<T> newEvent = new UIEvent<T>();
                    newEvent.callback += action;
                    targetDictionary.Add(id, newEvent);
                }
            }
            public static void Register<T, K>(UIEventID id, UIEvent_2차원<T, K> action, bool isGlobal = false) {
                Dictionary<UIEventID, UIEvent> targetDictionary;

                if (isGlobal) {
                    targetDictionary = globalUIEvent_2;
                }
                else {
                    targetDictionary = localUIEvent_2;
                }

                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent<T, K>).callback += action;
                }
                else {
                    UIEvent<T, K> newEvent = new UIEvent<T, K>();
                    newEvent.callback += action;
                    targetDictionary.Add(id, newEvent);
                }
            }
            public static void Register<T, K, L>(UIEventID id, UIEvent_3차원<T, K, L> action, bool isGlobal = false) {
                Dictionary<UIEventID, UIEvent> targetDictionary;

                if (isGlobal) {
                    targetDictionary = globalUIEvent_3;
                }
                else {
                    targetDictionary = localUIEvent_3;
                }

                if (targetDictionary.TryGetValue(id, out UIEvent value)) {
                    (value as UIEvent<T, K, L>).callback += action;
                }
                else {
                    UIEvent<T, K, L> newEvent = new UIEvent<T, K, L>();
                    newEvent.callback += action;
                    targetDictionary.Add(id, newEvent);
                }
            }

            public static void Init() {
                localUIEvent_0 = new Dictionary<UIEventID, UIEvent>();
                localUIEvent_1 = new Dictionary<UIEventID, UIEvent>();
                localUIEvent_2 = new Dictionary<UIEventID, UIEvent>();
                localUIEvent_3 = new Dictionary<UIEventID, UIEvent>();
                globalUIEvent_0 = new Dictionary<UIEventID, UIEvent>();
                globalUIEvent_1 = new Dictionary<UIEventID, UIEvent>();
                globalUIEvent_2 = new Dictionary<UIEventID, UIEvent>();
                globalUIEvent_3 = new Dictionary<UIEventID, UIEvent>();

                SceneManager.activeSceneChanged += (current, next) => {
                    localUIEvent_0.Clear();
                    localUIEvent_1.Clear();
                    localUIEvent_2.Clear();
                    localUIEvent_3.Clear();
                };
            }
        }

        public static class Game {
            public delegate void VoidEvent();
            public delegate void BoolEvent(bool state);
            public delegate void IntEvent(int value);
            public delegate void CharEvent(Character target);


            public static IntEvent onRoomStarted;
            public static IntEvent onRoomCleared;
            public static IntEvent onStageStarted;
            public static IntEvent onStageCleared;
            public static BoolEvent onGameStateChanged;
            public static IntEvent onPortalArrived;
            public static CharEvent onEnemySpawned;
            public static CharEvent onEnemyDead;
            public static CharEvent onEnemyDisposed;
            
            public static void CallOnRoomStarted(int index) {
                onRoomStarted?.Invoke(index);
            }
            public static void CallOnRoomCleared(int index) {
                onRoomCleared?.Invoke(index);
            }
            public static void CallOnStageStarted(int index) {
                onStageStarted?.Invoke(index);
            }
            public static void CallOnStageCleared(int index) {
                onStageCleared?.Invoke(index);
            }
            public static void CallOnGameStateChanged(bool state) {
                onGameStateChanged?.Invoke(state);
            }
            public static void CallOnPortalArrived(int value) {
                onPortalArrived?.Invoke(value);
            }
            public static void CallOnEnemySpawned(Character target) {
                onEnemySpawned?.Invoke(target);
            }
            public static void CallOnEnemyDead(Character target) {
                onEnemyDead?.Invoke(target);
            }
            public static void CallOnEnemyDisposed(Character target) {
                onEnemyDisposed?.Invoke(target);
            }
        }
    }
}