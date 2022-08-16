using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEditor;

namespace TenMinute.Data {
    public class Global_DataLoader<K, V> : MonoBehaviour {
        protected ReadOnlyDictionary<K, V> m_data;
    }
}