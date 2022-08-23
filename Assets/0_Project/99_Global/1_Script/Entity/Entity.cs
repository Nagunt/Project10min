using System.Collections;
using System.Collections.Generic;
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

        public bool IsRoot;

        private Entity _parent;
        private Entity _root;

        public Entity Parent => _parent;
        public Entity Root => _root;

        private List<DataEntity> _data;

        private void AddData(DataEntity data) {
            _data.Add(data);
        }

        public DataEntity GetData(int index) => _data[index];

        private void ApplyData(int index) {
            switch (_data[index].Type) {
                case EntityType.HP����:
                    break;
                case EntityType.HPȸ��:
                    break;
                case EntityType.����:
                    break;
                case EntityType.ȿ���ο�:
                    break;
                case EntityType.ȿ��ȸ��:
                    break;
                case EntityType.ȿ������:
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
            if (Has���) {
                if (Has��ü) {
                    ��üĳ����.onCalc_���ط�?.Invoke(��üĳ����, ���ĳ����, data);
                }
                ���ĳ����.onCalc_���ط�?.Invoke(��üĳ����, ���ĳ����, data);
            }
            AddData(data);
            return this;
        }

        public Entity Add��������(int �������ط�) {
            DataEntity data = DataEntity.����(�������ط�, EntityProperty.������ġ);
            AddData(data);
            return this;
        }

        public Entity Addȿ���ο�(EffectID id, int �ο���) {
            DataEntity data = DataEntity.ȿ���ο�(id, �ο���);
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

        public Entity Add����������(DataEntity data) {
            AddData(data);
            return this;
        }

        #endregion
    }
}
