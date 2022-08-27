using System.Collections;
using System.Collections.Generic;
using TenMinute.Data;
using UnityEngine;

namespace TenMinute.Combat {
    public sealed partial class DataEntity {

        private DataEntity(EntityType type, Effect effect) {
            _type = type;
            _효과 = effect;
        }
        private DataEntity(EntityType type, int 데이터, EntityProperty properties = EntityProperty.None) {
            _type = type;
            _데이터 = 데이터;
            _property = properties;
        }

        public static DataEntity 피해(int 데이터, EntityProperty properties = EntityProperty.None) {
            EntityProperty property =
                (properties.HasFlag(EntityProperty.고정수치) ? EntityProperty.고정수치 : EntityProperty.None) |
                (properties.HasFlag(EntityProperty.방어무시) ? EntityProperty.방어무시 : EntityProperty.None);
            return new DataEntity(EntityType.피해, 데이터, property);
        }

        public static DataEntity 추가피해(int 데이터, EntityProperty properties = EntityProperty.None) {
            EntityProperty property =
                (properties.HasFlag(EntityProperty.고정수치) ? EntityProperty.고정수치 : EntityProperty.None) |
                (properties.HasFlag(EntityProperty.방어무시) ? EntityProperty.방어무시 : EntityProperty.None);
            return new DataEntity(EntityType.추가피해, 데이터, property);
        }

        public static DataEntity 경직(int 데이터) {
            return new DataEntity(EntityType.경직, 데이터);
        }
        public static DataEntity 넉백(int 데이터) {
            return new DataEntity(EntityType.넉백, 데이터);
        }

        public static DataEntity 효과부여(EffectID id, int 부여량 = 1, float 지속시간 = 0f) {
            return new DataEntity(EntityType.효과부여, Effect.Create(id, 부여량).SetDuration(지속시간));
        }
        public static DataEntity 효과회수(EffectID id, int 회수량 = 1) {
            return new DataEntity(EntityType.효과회수, Effect.Create(id, 회수량));
        }
        public static DataEntity 효과제거(EffectID id) {
            return new DataEntity(EntityType.효과제거, Effect.Create(id));
        }

        public static DataEntity HP회복(int 데이터, EntityProperty properties = EntityProperty.None) {
            EntityProperty property =
                (properties.HasFlag(EntityProperty.고정수치) ? EntityProperty.고정수치 : EntityProperty.None);
            return new DataEntity(EntityType.HP회복, 데이터, property);
        }
        public static DataEntity HP지정(int 데이터) {
            return new DataEntity(EntityType.HP지정, 데이터);
        }
        public static DataEntity 고유데이터(int 데이터) {
            return new DataEntity(EntityType.None, 데이터);
        }
    }
}

