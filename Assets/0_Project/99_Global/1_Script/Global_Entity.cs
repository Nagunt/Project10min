using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {
    public delegate void On이벤트(DataEntity entity);
    public delegate void On이벤트2형식(DataEntity entity, int value);

    public class DataEntity {
        public enum EntityDataKey {
            None,
            데이터,
            경직,
            넉백,
            효과
        }
        public enum DataEntityType {
            None = 0,
            HP회복,
            피해,               // '공격 시', '피격 시' 효과가 발생
            추가피해,           // '공격 시', '피격 시' 효과가 발생하지 않음
            효과부여,
            효과회수,
            효과제거,
        }

        Entity _parent;
        List<Entity> _하위엔티티;

        DataEntityType _type;

        Character _주체;
        Character _대상;

        Dictionary<EntityDataKey, object> _커스텀데이터;
        Dictionary<EntityDataKey, Func<float>> onCalc커스텀데이터수치;
        Dictionary<EntityDataKey, Func<float>> onCalc커스텀데이터비율;

        public Character 주체 => _주체;
        public Character 대상 => _대상;

        public int 정수데이터 {
            get {
                try {
                    return Mathf.RoundToInt(실수데이터);
                }
                catch (Exception) {
                    return 0;
                }
            }
        }
        public float 실수데이터 {
            get {
                try {
                    return Convert.ToSingle(Get커스텀데이터(EntityDataKey.데이터));
                }
                catch (Exception) {
                    return 0;
                }
            }
        }

        public float 경직 {
            get {
                try {
                    return Convert.ToSingle(Get커스텀데이터(EntityDataKey.경직));
                }
                catch (Exception) {
                    return 0;
                }
            }
        }

        public float 넉백 {
            get {
                try {
                    return Convert.ToSingle(Get커스텀데이터(EntityDataKey.넉백));
                }
                catch (Exception) {
                    return 0;
                }
            }
        }

        public DataEntity(Entity parent, DataEntityType type, Character source, Character target) {
            _parent = parent;
            _type = type;
            _주체 = source;
            _대상 = target;
            _하위엔티티 = new List<Entity>();
            _커스텀데이터 = new Dictionary<EntityDataKey, object>();
            onCalc커스텀데이터비율 = new Dictionary<EntityDataKey, Func<float>>();
            onCalc커스텀데이터수치 = new Dictionary<EntityDataKey, Func<float>>();
        }

        public Entity Add하위엔티티(Entity entity) {
            entity.Set상위엔티티(_parent);
            _하위엔티티.Add(entity);
            return _parent;
        }

        public void Add데이터수치(float value) {
            Add커스텀데이터수치(EntityDataKey.데이터, value);
        }

        public void Add데이터비율(float value) {
            Add커스텀데이터비율(EntityDataKey.데이터, value);
        }

        public object Get커스텀데이터(EntityDataKey key) {
            if (_커스텀데이터.TryGetValue(key, out object value)) {
                return value;
            }
            return default;
        }

        public void Set커스텀데이터<T>(EntityDataKey key, T value) {
            if (_커스텀데이터.TryGetValue(key, out _)) {
                _커스텀데이터[key] = value;
            }
            else {
                _커스텀데이터.Add(key, value);
            }
        }

        public void Add커스텀데이터수치(EntityDataKey key, float value) {
            if (onCalc커스텀데이터수치.ContainsKey(key)) {
                onCalc커스텀데이터수치[key] += () => value;
            }
            else {
                onCalc커스텀데이터수치.Add(key, default);
            }
        }

        public void Add커스텀데이터비율(EntityDataKey key, float value) {
            if (onCalc커스텀데이터비율.ContainsKey(key)) {
                onCalc커스텀데이터비율[key] += () => value;
            }
            else {
                onCalc커스텀데이터비율.Add(key, default);
            }
        }

        public void Execute() {
            Debug.Log($"Data Entity 실행\n타입:{_type}\n주체:{주체}\n대상:{대상}");
            if (대상 == null) {
                Debug.Log("대상은 null이 될 수 없습니다.");
                return;
            }
            switch (_type) {
                case DataEntityType.피해: {
                        if (주체 != null) {
                            ExecuteCallback(주체.on피해예정);
                        }

                        ExecuteCallback(대상.on피해예정);
                        if (정수데이터 > 0) {
                            대상.Damage(정수데이터);
                            if (주체 != null) {
                                ExecuteCallback(주체.on피해);
                            }
                            ExecuteCallback(대상.on피해);
                        }
                        else {
                            // 피해 무효
                        }
                    }
                    break;
                case DataEntityType.추가피해: {
                        if (주체 != null) {
                            ExecuteCallback(주체.on추가피해예정);
                        }

                        ExecuteCallback(대상.on추가피해예정);
                        if (정수데이터 > 0) {
                            대상.Damage(정수데이터, 경직, 넉백);
                            if (주체 != null) {
                                ExecuteCallback(주체.on추가피해);
                            }
                            ExecuteCallback(대상.on추가피해);
                        }
                        else {
                            // 피해 무효
                        }
                    }
                    break;
                case DataEntityType.HP회복: {
                        if (주체 != null) {
                            ExecuteCallback(주체.onHP회복예정);
                        }
                        ExecuteCallback(대상.onHP회복예정);
                        if (정수데이터 > 0) {
                            대상.Heal(정수데이터);
                            if (주체 != null) {
                                ExecuteCallback(주체.onHP회복);
                            }
                            ExecuteCallback(대상.onHP회복);
                        }
                        else {
                            // 회복 무효
                        }
                    }
                    break;
            }

            void ExecuteCallback(On이벤트 이벤트) {
                이벤트?.Invoke(this);
                foreach (Entity entity in _하위엔티티) {
                    entity.Execute();
                }
                _하위엔티티.Clear();
            }
        }
    }
    public class Entity {

        public enum EntityType {
            None = 0,
        }

        EntityType _type;
        Character _주체;
        Character _대상;

        List<DataEntity> _dataEntity;

        Entity _상위엔티티;
        List<Entity> _하위엔티티;

        public Character 주체 => _주체;
        public Character 대상 => _대상;

        private Entity(EntityType entityType, Character source, Character target) {
            _type = entityType;
            _주체 = source;
            _대상 = target;
            _dataEntity = new List<DataEntity>();

            _상위엔티티 = null;
            _하위엔티티 = new List<Entity>();
        }

        public static Entity Create(Character source, Character target) {
            return new Entity(EntityType.None, source, target);
        }

        public static Entity Create(EntityType entityType, Character source, Character target) {
            return new Entity(entityType, source, target);
        }

        public Entity AddHP회복(int value) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.HP회복, _주체, _대상);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.데이터, value);
            _dataEntity.Add(dataEntity);
            return this;
        }

        public Entity Add피해(int value, float 경직 = 0f, float 넉백 = 0f) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.피해, _주체, _대상);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.데이터, value);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.경직, 경직);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.넉백, 넉백);
            _dataEntity.Add(dataEntity);
            return this;
        }

        public Entity Add추가피해(int value, float 경직 = 0f, float 넉백 = 0f) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.추가피해, _주체, _대상);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.데이터, value);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.경직, 경직);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.넉백, 넉백);
            _dataEntity.Add(dataEntity);
            return this;
        }

        public Entity Add하위엔티티(Entity entity) {
            entity._상위엔티티 = this;
            _하위엔티티.Add(entity);
            return this;
        }

        public Entity Set상위엔티티(Entity entity) {
            _상위엔티티 = entity;
            return this;
        }

        public Entity Get최상위엔티티() {
            Entity 최상위엔티티 = this;
            while (최상위엔티티._상위엔티티 != null) {
                최상위엔티티 = 최상위엔티티._상위엔티티;
            }
            return 최상위엔티티;
        }

        public void Execute() {
            for (int i = 0; i < _dataEntity.Count; ++i) {
                _dataEntity[i].Execute();
            }
            for (int i = 0; i < _하위엔티티.Count; ++i) {
                _하위엔티티[i].Execute();
            }
        }
    }
}
