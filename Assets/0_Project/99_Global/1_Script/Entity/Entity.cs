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

        private Character _주체캐릭터;
        private Artifact _주체유물;
        private Effect _주체효과;

        private Character _대상캐릭터;

        public bool Has주체 => _주체캐릭터 != null || _주체유물 != null || _주체효과 == null;
        public bool Has대상 => _대상캐릭터 != null;

        public EntityID ID => _id;
        public Character 주체캐릭터 => _주체캐릭터;
        public Artifact 주체유물 => _주체유물;
        public Effect 주체효과 => _주체효과;
        public Character 대상캐릭터 => _대상캐릭터;

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
                case EntityType.HP지정:
                    break;
                case EntityType.HP회복:
                    break;
                case EntityType.피해:
                    break;
                case EntityType.효과부여:
                    break;
                case EntityType.효과회수:
                    break;
                case EntityType.효과제거:
                    break;
            }
        }

        #region Entity 액션

        public Entity AddHP회복(int 회복량) {
            DataEntity data = DataEntity.HP회복(회복량);
            AddData(data);
            return this;
        }

        public Entity AddHP지정(int 지정량) {
            DataEntity data = DataEntity.HP지정(지정량);
            AddData(data);
            return this;
        }

        public Entity Add피해(int 피해량) {
            DataEntity data = DataEntity.피해(피해량);
            if (Has대상) {
                if (Has주체) {
                    주체캐릭터.onCalc_피해량?.Invoke(주체캐릭터, 대상캐릭터, data);
                }
                대상캐릭터.onCalc_피해량?.Invoke(주체캐릭터, 대상캐릭터, data);
            }
            AddData(data);
            return this;
        }

        public Entity Add고정피해(int 고정피해량) {
            DataEntity data = DataEntity.피해(고정피해량, EntityProperty.고정수치);
            AddData(data);
            return this;
        }

        public Entity Add효과부여(EffectID id, int 부여량) {
            DataEntity data = DataEntity.효과부여(id, 부여량);
            AddData(data);
            return this;
        }

        public Entity Add효과회수(EffectID id, int 회수량) {
            DataEntity data = DataEntity.효과회수(id, 회수량);
            AddData(data);
            return this;
        }

        public Entity Add효과제거(EffectID id) {
            DataEntity data = DataEntity.효과제거(id);
            AddData(data);
            return this;
        }

        public Entity Add고유데이터(DataEntity data) {
            AddData(data);
            return this;
        }

        #endregion
    }
}
