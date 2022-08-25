using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TenMinute.Data;
using UnityEngine;

namespace TenMinute.Combat {

    public enum EntityID {
        None = 0,
    }

    public sealed partial class Entity {
        private EntityID _id;

        private Character _��üĳ����;
        private Artifact _��ü����;
        private Effect _��üȿ��;

        private Character _���ĳ����;

        public bool Has��ü => _��üĳ���� != null || _��ü���� != null || _��üȿ�� == null;
        public bool Has��� => _���ĳ���� != null;

        public EntityID ID => _id;
        public Character ��üĳ���� => _��üĳ����;
        public Artifact ��ü���� => _��ü����;
        public Effect ��üȿ�� => _��üȿ��;
        public Character ���ĳ���� => _���ĳ����;

        public bool IsRoot => _root == null;

        private Entity _parent;
        private Entity _root;

        public Entity Parent => _parent;
        public Entity Root => _root;

        private List<DataEntity> _data;

        private void AddData(DataEntity data) {
            _data.Add(data);
            _���꿣ƼƼ.Add(new List<Entity>());
        }

        private List<List<Entity>> _���꿣ƼƼ;

        public DataEntity GetData(int index) => _data[index];

        public ReadOnlyCollection<Entity> Get���꿣ƼƼ(int index) => _���꿣ƼƼ[index].AsReadOnly();

        private void SetRoot(Entity parent) {
            _parent = parent;
            _root = parent.IsRoot ? parent : parent.Root;
        }

        public void Execute() {
            ExecuteData();
        }

        private void ExecuteData() {
            for(int i = 0; i < _data.Count; ++i) {
                if (_data[i] != null && Has���) {
                    ApplyData(i);
                }

                for(int j = 0; j < _���꿣ƼƼ[i].Count; ++j) {
                    _���꿣ƼƼ[i][j].SetRoot(this);
                    _���꿣ƼƼ[i][j].ExecuteData();
                }
            }
        }

        private void ApplyData(int index) {
            switch (_data[index].Type) {
                case EntityType.HP����:
                    ���ĳ����.ApplyHP����(this, index);
                    break;
                case EntityType.HPȸ��:
                    ���ĳ����.ApplyHPȸ��(this, index);
                    break;
                case EntityType.����:
                    ���ĳ����.Apply����(this, index);
                    break;
                case EntityType.ȿ���ο�:
                case EntityType.ȿ��ȸ��:
                case EntityType.ȿ������:
                    ���ĳ����.Applyȿ��(this, index);
                    break;
                case EntityType.����:
                    ���ĳ����.Apply����(this, index);
                    break;
                case EntityType.�˹�:
                    ���ĳ����.Apply�˹�(this, index);
                    break;
            }
        }

        #region Entity �׼�

        public Entity AddHPȸ��(int ȸ����) {
            DataEntity data = DataEntity.HPȸ��(ȸ����);
            AddData(data);
            return this;
        }

        public Entity AddHP����(int ������) {
            DataEntity data = DataEntity.HP����(������);
            AddData(data);
            return this;
        }

        public Entity Add����(int ���ط�) {
            DataEntity data = DataEntity.����(���ط�);
            AddData(data);
            return this;
        }

        public Entity Add��������(int ���ط�) {
            DataEntity data = DataEntity.����(���ط�, EntityProperty.������ġ);
            AddData(data);
            return this;
        }

        public Entity Add��������(int ���ط�) {
            DataEntity data = DataEntity.����(���ط�, EntityProperty.����);
            AddData(data);
            return this;
        }

        public Entity Add���ð�������(int ���ط�) {
            DataEntity data = DataEntity.����(���ط�, EntityProperty.���� | EntityProperty.������ġ);
            AddData(data);
            return this;
        }

        public Entity Addȿ���ο�(EffectID id, int �ο��� = 1, float ���ӽð� = 0f) {
            DataEntity data = DataEntity.ȿ���ο�(id, �ο���, ���ӽð�);
            AddData(data);
            return this;
        }

        public Entity Addȿ��ȸ��(EffectID id, int ȸ����) {
            DataEntity data = DataEntity.ȿ��ȸ��(id, ȸ����);
            AddData(data);
            return this;
        }

        public Entity Addȿ������(EffectID id) {
            DataEntity data = DataEntity.ȿ������(id);
            AddData(data);
            return this;
        }

        public Entity Add����(int ������) {
            DataEntity data = DataEntity.����(������);
            AddData(data);
            return this;
        }

        public Entity Add�˹�(int �˹鵵) {
            DataEntity data = DataEntity.�˹�(�˹鵵);
            AddData(data);
            return this;
        }

        public Entity Add����������(DataEntity data) {
            AddData(data);
            return this;
        }

        public Entity Add���꿣ƼƼ(Entity entity, int index = 0) {
            _���꿣ƼƼ[index].Add(entity);
            return this;
        }

        #endregion
    }
}
