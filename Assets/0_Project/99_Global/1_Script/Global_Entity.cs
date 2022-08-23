using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TenMinute.Data;
using UnityEngine;

namespace TenMinute.ASDF {
    public delegate void On이벤트(DataEntity entity);
    public delegate void On이벤트2형식(DataEntity entity, int value);

    public class DataEntity {
        public enum EntityDataKey {
            None,
            데이터,
            경직,
            넉백,
            효과,
            지속시간,
            유물
        }
        public enum DataEntityType {
            None = 0,
            HP회복,
            피해,               // '공격 시', '피격 시' 효과가 발생
            추가피해,           // '공격 시', '피격 시' 효과가 발생하지 않음
            효과부여,           // 효과를 부여
            효과회수,           // 효과를 일정 부분 회수
            효과제거,           // 효과를 아예 제거
            유물획득,
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
        public EffectID 효과 {
            get {
                try {
                    return (EffectID)Get커스텀데이터(EntityDataKey.효과);
                }
                catch (Exception) {
                    return EffectID.None;
                }
            }
        }
        public float 지속시간 {
            get {
                try {
                    return Convert.ToSingle(Get커스텀데이터(EntityDataKey.지속시간));
                }
                catch (Exception) {
                    return 0;
                }
            }
        }
        public ArtifactID 유물 {
            get {
                try {
                    return (ArtifactID)Get커스텀데이터(EntityDataKey.유물);
                }
                catch (Exception) {
                    return ArtifactID.None;
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

        public T Get커스텀데이터<T>(EntityDataKey key) {
            if(_커스텀데이터.TryGetValue(key, out object value)) {
                return (T)value;
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
            Debug.Log($"Data Entity 실행 타입:{_type} 주체:{주체} 대상:{대상}");
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
                        float 방어력계산데이터 = 실수데이터 - 대상.DEF;

                        float 증감수치 = 0;
                        float 증감비율 = 1.0f;
                        if (onCalc커스텀데이터수치[EntityDataKey.데이터] != null) {
                            foreach (var f in onCalc커스텀데이터수치[EntityDataKey.데이터].GetInvocationList().Cast<Func<float>>()) {
                                증감수치 += f();
                            }
                        }
                        if (onCalc커스텀데이터비율[EntityDataKey.데이터] != null) {
                            foreach (var f in onCalc커스텀데이터비율[EntityDataKey.데이터].GetInvocationList().Cast<Func<float>>()) {
                                float 값 = f();
                                if (값 <= 0) continue;
                                증감비율 *= 값;
                            }
                        }
                        Set커스텀데이터(EntityDataKey.데이터, Mathf.RoundToInt(방어력계산데이터 * 증감비율 + 증감수치));

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
                case DataEntityType.유물획득: {
                        ExecuteCallback(대상.onArtifact획득예정);
                        if (유물 != ArtifactID.None) {
                            Artifact newArtifact = Artifact.
                                Create(유물).
                                SetOwner(대상);
                            if (newArtifact != null) {
                                대상.AddArtifact(newArtifact);
                            }
                            ExecuteCallback(대상.onArtifact획득);
                        }
                    }
                    break;
                case DataEntityType.효과부여: {
                        if (대상.IsImmune(효과)) return;
                        if (주체 != null) {
                            ExecuteCallback(주체.onEffect부여예정);
                        }
                        ExecuteCallback(대상.onEffect부여예정);
                        if (정수데이터 > 0) {
                            Effect newEffect = Effect.
                                Create(효과).
                                SetOwner(대상).
                                SetValue(정수데이터).
                                SetDuration(지속시간);

                            if (대상.HasEffect(효과)) {
                                대상.GetEffect(효과).Merge(newEffect);
                            }
                            else {
                                대상.AddEffect(newEffect);
                                newEffect.OnEnable();
                            }
                            if (주체 != null) {
                                ExecuteCallback(주체.onEffect부여);
                            }
                            ExecuteCallback(대상.onEffect부여);
                        }
                    }
                    break;
                case DataEntityType.효과회수: {
                        if (대상.IsImmune(효과)) return;
                        if (대상.HasEffect(효과) == false) return;
                        if (주체 != null) {
                            ExecuteCallback(주체.onEffect회수예정);
                        }
                        ExecuteCallback(대상.onEffect회수예정);
                        if (정수데이터 > 0) {
                            Effect newEffect = Effect.
                                Create(효과).
                                SetOwner(대상).
                                SetValue(정수데이터).
                                SetDuration(지속시간);

                            대상.GetEffect(효과).Subtract(newEffect);
                            if (주체 != null) {
                                ExecuteCallback(주체.onEffect회수);
                            }
                            ExecuteCallback(대상.onEffect회수);
                        }
                    }
                    break;
                case DataEntityType.효과제거: {
                        if (대상.IsImmune(효과)) return;
                        if (대상.HasEffect(효과) == false) return;
                        if (주체 != null) {
                            ExecuteCallback(주체.onEffect제거예정);
                        }
                        ExecuteCallback(대상.onEffect제거예정);
                        if (정수데이터 > 0) {
                            Effect targetEffect = 대상.GetEffect(효과);
                            targetEffect.OnDisable();
                            대상.RemoveEffect(효과);
                            if (주체 != null) {
                                ExecuteCallback(주체.onEffect제거);
                            }
                            ExecuteCallback(대상.onEffect제거);
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

        public Entity Add효과부여(EffectID id, int amount = 1, float duration = 0f) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.효과부여, _주체, _대상);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.효과, id);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.데이터, amount);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.지속시간, duration);
            _dataEntity.Add(dataEntity);
            return this;
        }

        public Entity Add유물획득(ArtifactID id) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.유물획득, _주체, _대상);
            dataEntity.Set커스텀데이터(DataEntity.EntityDataKey.유물, id);
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

        public Entity Execute() {
            for (int i = 0; i < _dataEntity.Count; ++i) {
                _dataEntity[i].Execute();
            }
            for (int i = 0; i < _하위엔티티.Count; ++i) {
                _하위엔티티[i].Execute();
            }
            return this;
        }
    }
}
