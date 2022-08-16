using System;
using System.Collections.ObjectModel;
using TenMinute.Data;

namespace TenMinute {

    [Serializable]
    public class StageDataDictionary : SerializableDictionary<int, Stage> { };

    public class StageLoader : Global_DataLoader<int, Stage> {
        private static StageLoader m_instance = null;
       
        public static ReadOnlyDictionary<int, Stage> Data {
            get {
                if (m_instance != null) {
                    return m_instance.m_data;
                }
                return null;
            }
        }

        public StageDataDictionary data;

        private void Awake() {
            m_instance = this;
            m_data = new ReadOnlyDictionary<int, Stage>(data);
        }
    }
}

