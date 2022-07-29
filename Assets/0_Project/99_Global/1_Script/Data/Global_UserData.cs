using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Data {

    public enum DataID {
        Setting,
    }

    public static class Global_UserData {
        public static void Reset() {
            PlayerPrefs.DeleteAll();
        }

        public static string GetData(DataID dataID, string defaultValue = "") {
            return PlayerPrefs.GetString(dataID.ToString(), defaultValue);
        }
        public static void SaveData(DataID dataID, string data) {
            PlayerPrefs.SetString(dataID.ToString(), data);
            PlayerPrefs.Save();
        }

        public static class Setting {
            [System.Serializable]
            public class SettingData {
                public float bgm;
                public float sfx;

                public SettingData() {
                    bgm = 0.5f;
                    sfx = 0.5f;
                }

                public override string ToString() {
                    return $"BGM : {bgm}\nSFX : {sfx}";
                }
            }

            private static SettingData data;

            public static void Save() {
                string json = JsonUtility.ToJson(data, true);
                Global_UserData.SaveData(DataID.Setting, json);
            }

            public static void Load() {
                string json = Global_UserData.GetData(DataID.Setting);
                data = JsonUtility.FromJson<SettingData>(json);
            }
        }
    }
}
