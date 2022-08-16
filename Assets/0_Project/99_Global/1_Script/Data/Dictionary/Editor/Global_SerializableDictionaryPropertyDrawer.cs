using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TenMinute.Data {

    [CustomPropertyDrawer(typeof(StageDataDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

    public class AnySerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }

}