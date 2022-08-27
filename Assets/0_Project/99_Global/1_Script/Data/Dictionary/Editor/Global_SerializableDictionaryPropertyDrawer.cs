using System.Collections;
using System.Collections.Generic;
using TenMinute.Graphics;
using UnityEditor;
using UnityEngine;

namespace TenMinute.Data {

    [CustomPropertyDrawer(typeof(StageDataDictionary))]
    [CustomPropertyDrawer(typeof(EntityGraphicDataDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

    public class AnySerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }

}