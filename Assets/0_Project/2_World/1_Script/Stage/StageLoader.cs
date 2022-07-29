using System.Collections.ObjectModel;
using TenMinute.Data;

namespace TenMinute {
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

        private void Awake() {
            m_instance = this;
            m_data = ToData();
        }
    }
}

