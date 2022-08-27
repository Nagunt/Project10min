using System.Collections;
using System.Collections.Generic;
using TenMinute.Data;
using UnityEngine;

namespace TenMinute.Combat {
    public sealed partial class DataEntity {

        private DataEntity(EntityType type, Effect effect) {
            _type = type;
            _ȿ�� = effect;
        }
        private DataEntity(EntityType type, int ������, EntityProperty properties = EntityProperty.None) {
            _type = type;
            _������ = ������;
            _property = properties;
        }

        public static DataEntity ����(int ������, EntityProperty properties = EntityProperty.None) {
            EntityProperty property =
                (properties.HasFlag(EntityProperty.������ġ) ? EntityProperty.������ġ : EntityProperty.None) |
                (properties.HasFlag(EntityProperty.����) ? EntityProperty.���� : EntityProperty.None);
            return new DataEntity(EntityType.����, ������, property);
        }

        public static DataEntity �߰�����(int ������, EntityProperty properties = EntityProperty.None) {
            EntityProperty property =
                (properties.HasFlag(EntityProperty.������ġ) ? EntityProperty.������ġ : EntityProperty.None) |
                (properties.HasFlag(EntityProperty.����) ? EntityProperty.���� : EntityProperty.None);
            return new DataEntity(EntityType.�߰�����, ������, property);
        }

        public static DataEntity ����(int ������) {
            return new DataEntity(EntityType.����, ������);
        }
        public static DataEntity �˹�(int ������) {
            return new DataEntity(EntityType.�˹�, ������);
        }

        public static DataEntity ȿ���ο�(EffectID id, int �ο��� = 1, float ���ӽð� = 0f) {
            return new DataEntity(EntityType.ȿ���ο�, Effect.Create(id, �ο���).SetDuration(���ӽð�));
        }
        public static DataEntity ȿ��ȸ��(EffectID id, int ȸ���� = 1) {
            return new DataEntity(EntityType.ȿ��ȸ��, Effect.Create(id, ȸ����));
        }
        public static DataEntity ȿ������(EffectID id) {
            return new DataEntity(EntityType.ȿ������, Effect.Create(id));
        }

        public static DataEntity HPȸ��(int ������, EntityProperty properties = EntityProperty.None) {
            EntityProperty property =
                (properties.HasFlag(EntityProperty.������ġ) ? EntityProperty.������ġ : EntityProperty.None);
            return new DataEntity(EntityType.HPȸ��, ������, property);
        }
        public static DataEntity HP����(int ������) {
            return new DataEntity(EntityType.HP����, ������);
        }
        public static DataEntity ����������(int ������) {
            return new DataEntity(EntityType.None, ������);
        }
    }
}

