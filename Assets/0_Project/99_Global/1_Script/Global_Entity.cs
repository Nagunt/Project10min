using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {
    public delegate void On�̺�Ʈ(DataEntity entity);
    public delegate void On�̺�Ʈ2����(DataEntity entity, int value);

    public class DataEntity {
        public enum EntityDataKey {
            None,
            ������,
            ����,
            �˹�,
            ȿ��
        }
        public enum DataEntityType {
            None = 0,
            HPȸ��,
            ����,               // '���� ��', '�ǰ� ��' ȿ���� �߻�
            �߰�����,           // '���� ��', '�ǰ� ��' ȿ���� �߻����� ����
            ȿ���ο�,
            ȿ��ȸ��,
            ȿ������,
        }

        Entity _parent;
        List<Entity> _������ƼƼ;

        DataEntityType _type;

        Character _��ü;
        Character _���;

        Dictionary<EntityDataKey, object> _Ŀ���ҵ�����;
        Dictionary<EntityDataKey, Func<float>> onCalcĿ���ҵ����ͼ�ġ;
        Dictionary<EntityDataKey, Func<float>> onCalcĿ���ҵ����ͺ���;

        public Character ��ü => _��ü;
        public Character ��� => _���;

        public int ���������� {
            get {
                try {
                    return Mathf.RoundToInt(�Ǽ�������);
                }
                catch (Exception) {
                    return 0;
                }
            }
        }
        public float �Ǽ������� {
            get {
                try {
                    return Convert.ToSingle(GetĿ���ҵ�����(EntityDataKey.������));
                }
                catch (Exception) {
                    return 0;
                }
            }
        }

        public float ���� {
            get {
                try {
                    return Convert.ToSingle(GetĿ���ҵ�����(EntityDataKey.����));
                }
                catch (Exception) {
                    return 0;
                }
            }
        }

        public float �˹� {
            get {
                try {
                    return Convert.ToSingle(GetĿ���ҵ�����(EntityDataKey.�˹�));
                }
                catch (Exception) {
                    return 0;
                }
            }
        }

        public DataEntity(Entity parent, DataEntityType type, Character source, Character target) {
            _parent = parent;
            _type = type;
            _��ü = source;
            _��� = target;
            _������ƼƼ = new List<Entity>();
            _Ŀ���ҵ����� = new Dictionary<EntityDataKey, object>();
            onCalcĿ���ҵ����ͺ��� = new Dictionary<EntityDataKey, Func<float>>();
            onCalcĿ���ҵ����ͼ�ġ = new Dictionary<EntityDataKey, Func<float>>();
        }

        public Entity Add������ƼƼ(Entity entity) {
            entity.Set������ƼƼ(_parent);
            _������ƼƼ.Add(entity);
            return _parent;
        }

        public void Add�����ͼ�ġ(float value) {
            AddĿ���ҵ����ͼ�ġ(EntityDataKey.������, value);
        }

        public void Add�����ͺ���(float value) {
            AddĿ���ҵ����ͺ���(EntityDataKey.������, value);
        }

        public object GetĿ���ҵ�����(EntityDataKey key) {
            if (_Ŀ���ҵ�����.TryGetValue(key, out object value)) {
                return value;
            }
            return default;
        }

        public void SetĿ���ҵ�����<T>(EntityDataKey key, T value) {
            if (_Ŀ���ҵ�����.TryGetValue(key, out _)) {
                _Ŀ���ҵ�����[key] = value;
            }
            else {
                _Ŀ���ҵ�����.Add(key, value);
            }
        }

        public void AddĿ���ҵ����ͼ�ġ(EntityDataKey key, float value) {
            if (onCalcĿ���ҵ����ͼ�ġ.ContainsKey(key)) {
                onCalcĿ���ҵ����ͼ�ġ[key] += () => value;
            }
            else {
                onCalcĿ���ҵ����ͼ�ġ.Add(key, default);
            }
        }

        public void AddĿ���ҵ����ͺ���(EntityDataKey key, float value) {
            if (onCalcĿ���ҵ����ͺ���.ContainsKey(key)) {
                onCalcĿ���ҵ����ͺ���[key] += () => value;
            }
            else {
                onCalcĿ���ҵ����ͺ���.Add(key, default);
            }
        }

        public void Execute() {
            Debug.Log($"Data Entity ����\nŸ��:{_type}\n��ü:{��ü}\n���:{���}");
            if (��� == null) {
                Debug.Log("����� null�� �� �� �����ϴ�.");
                return;
            }
            switch (_type) {
                case DataEntityType.����: {
                        if (��ü != null) {
                            ExecuteCallback(��ü.on���ؿ���);
                        }

                        ExecuteCallback(���.on���ؿ���);
                        if (���������� > 0) {
                            ���.Damage(����������);
                            if (��ü != null) {
                                ExecuteCallback(��ü.on����);
                            }
                            ExecuteCallback(���.on����);
                        }
                        else {
                            // ���� ��ȿ
                        }
                    }
                    break;
                case DataEntityType.�߰�����: {
                        if (��ü != null) {
                            ExecuteCallback(��ü.on�߰����ؿ���);
                        }

                        ExecuteCallback(���.on�߰����ؿ���);
                        if (���������� > 0) {
                            ���.Damage(����������, ����, �˹�);
                            if (��ü != null) {
                                ExecuteCallback(��ü.on�߰�����);
                            }
                            ExecuteCallback(���.on�߰�����);
                        }
                        else {
                            // ���� ��ȿ
                        }
                    }
                    break;
                case DataEntityType.HPȸ��: {
                        if (��ü != null) {
                            ExecuteCallback(��ü.onHPȸ������);
                        }
                        ExecuteCallback(���.onHPȸ������);
                        if (���������� > 0) {
                            ���.Heal(����������);
                            if (��ü != null) {
                                ExecuteCallback(��ü.onHPȸ��);
                            }
                            ExecuteCallback(���.onHPȸ��);
                        }
                        else {
                            // ȸ�� ��ȿ
                        }
                    }
                    break;
            }

            void ExecuteCallback(On�̺�Ʈ �̺�Ʈ) {
                �̺�Ʈ?.Invoke(this);
                foreach (Entity entity in _������ƼƼ) {
                    entity.Execute();
                }
                _������ƼƼ.Clear();
            }
        }
    }
    public class Entity {

        public enum EntityType {
            None = 0,
        }

        EntityType _type;
        Character _��ü;
        Character _���;

        List<DataEntity> _dataEntity;

        Entity _������ƼƼ;
        List<Entity> _������ƼƼ;

        public Character ��ü => _��ü;
        public Character ��� => _���;

        private Entity(EntityType entityType, Character source, Character target) {
            _type = entityType;
            _��ü = source;
            _��� = target;
            _dataEntity = new List<DataEntity>();

            _������ƼƼ = null;
            _������ƼƼ = new List<Entity>();
        }

        public static Entity Create(Character source, Character target) {
            return new Entity(EntityType.None, source, target);
        }

        public static Entity Create(EntityType entityType, Character source, Character target) {
            return new Entity(entityType, source, target);
        }

        public Entity AddHPȸ��(int value) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.HPȸ��, _��ü, _���);
            dataEntity.SetĿ���ҵ�����(DataEntity.EntityDataKey.������, value);
            _dataEntity.Add(dataEntity);
            return this;
        }

        public Entity Add����(int value, float ���� = 0f, float �˹� = 0f) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.����, _��ü, _���);
            dataEntity.SetĿ���ҵ�����(DataEntity.EntityDataKey.������, value);
            dataEntity.SetĿ���ҵ�����(DataEntity.EntityDataKey.����, ����);
            dataEntity.SetĿ���ҵ�����(DataEntity.EntityDataKey.�˹�, �˹�);
            _dataEntity.Add(dataEntity);
            return this;
        }

        public Entity Add�߰�����(int value, float ���� = 0f, float �˹� = 0f) {
            DataEntity dataEntity = new DataEntity(this, DataEntity.DataEntityType.�߰�����, _��ü, _���);
            dataEntity.SetĿ���ҵ�����(DataEntity.EntityDataKey.������, value);
            dataEntity.SetĿ���ҵ�����(DataEntity.EntityDataKey.����, ����);
            dataEntity.SetĿ���ҵ�����(DataEntity.EntityDataKey.�˹�, �˹�);
            _dataEntity.Add(dataEntity);
            return this;
        }

        public Entity Add������ƼƼ(Entity entity) {
            entity._������ƼƼ = this;
            _������ƼƼ.Add(entity);
            return this;
        }

        public Entity Set������ƼƼ(Entity entity) {
            _������ƼƼ = entity;
            return this;
        }

        public Entity Get�ֻ�����ƼƼ() {
            Entity �ֻ�����ƼƼ = this;
            while (�ֻ�����ƼƼ._������ƼƼ != null) {
                �ֻ�����ƼƼ = �ֻ�����ƼƼ._������ƼƼ;
            }
            return �ֻ�����ƼƼ;
        }

        public void Execute() {
            for (int i = 0; i < _dataEntity.Count; ++i) {
                _dataEntity[i].Execute();
            }
            for (int i = 0; i < _������ƼƼ.Count; ++i) {
                _������ƼƼ[i].Execute();
            }
        }
    }
}
