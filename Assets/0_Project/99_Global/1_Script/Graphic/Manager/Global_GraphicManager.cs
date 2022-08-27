using System;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Combat;
using UnityEngine;

namespace TenMinute.Graphics {

    [Serializable]
    public class EntityGraphicDataDictionary : SerializableDictionary<EntityID, Graphic_Entity> { };

    public class Global_GraphicManager : MonoBehaviour {
        public static Global_GraphicManager Instance { get; private set; } = null;

        private static readonly int _default_text_count = 100;
        private static readonly int _default_entity_count = 10;

        private Queue<Graphic_Text> _textQueue;
        private Dictionary<EntityID, Queue<Graphic_Entity>> _entityData;

        [SerializeField]
        private Graphic_Text _prefab_Text;
        [SerializeField]
        private EntityGraphicDataDictionary _prefab_Entity;

        private void Awake() {
            if (Instance == null) {
                Init();
            }
        }

        private void Init() {
            Instance = this;
            _textQueue = new Queue<Graphic_Text>();
            _entityData = new Dictionary<EntityID, Queue<Graphic_Entity>>();
            for (int i = 0; i < _default_text_count; ++i) {
                _textQueue.Enqueue(CreateText());
            }
            foreach (KeyValuePair<EntityID, Graphic_Entity> kv in _prefab_Entity) {
                Queue<Graphic_Entity> queue = new Queue<Graphic_Entity>();
                for (int j = 0; j < _default_entity_count; ++j) {
                    queue.Enqueue(CreateEntity(kv.Key));
                }
                _entityData.Add(kv.Key, queue);
            }
        }

        private Graphic_Text CreateText() {
            Graphic_Text newObject = Instantiate(_prefab_Text, transform).SetActive(false);
            newObject.onComplete += (obj) => _textQueue.Enqueue(obj.SetActive(false));
            return newObject;
        }

        private Graphic_Entity CreateEntity(EntityID id) {
            Graphic_Entity newObject = Instantiate(_prefab_Entity[id], transform).SetActive(false);
            newObject.onComplete += (id, obj) => _entityData[id].Enqueue(obj.SetActive(false));
            return newObject;
        }

        public Graphic_Text GetText() {
            if (_textQueue.Count <= 0) {
                _textQueue.Enqueue(CreateText());
            }
            return _textQueue.Dequeue().SetActive(true);
        }

        public Graphic_Entity GetGraphic(EntityID id) {
            if (_entityData[id].Count <= 0) {
                _entityData[id].Enqueue(CreateEntity(id));
            }
            return _entityData[id].Dequeue().SetActive(true);
        }
    }
}
