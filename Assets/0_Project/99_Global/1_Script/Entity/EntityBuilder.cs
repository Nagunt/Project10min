using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Combat {
    public sealed partial class Entity {

        private Entity(EntityID id, Character 주체캐릭터, Artifact 주체유물, Effect 주체효과, Character 대상캐릭터) {
            _id = id;
            _주체캐릭터 = 주체캐릭터;
            _주체유물 = 주체유물;
            _주체효과 = 주체효과;
            _대상캐릭터 = 대상캐릭터;
            _data = new List<DataEntity>();
        }

        // 주체가 없는 효과 (지형 피해 등)
        public static Entity Create(Character target) {
            return new Entity(EntityID.None, null, null, null, target);
        }
        public static Entity Create(EntityID id, Character target) {
            return new Entity(id, null, null, null, target);
        }

        // 주체가 캐릭터 (캐릭터 직접 공격 등)
        public static Entity Create(Character source, Character target) {
            return new Entity(EntityID.None, source, null, null, target);
        }

        public static Entity Create(EntityID id, Character source, Character target) {
            return new Entity(id, source, null, null, target);
        }

        // 주체가 유물 (파생 효과 등)
        public static Entity Create(Artifact source, Character target) {
            return new Entity(EntityID.None, null, source, null, target);
        }
        public static Entity Create(EntityID id, Artifact source, Character target) {
            return new Entity(id, null, source, null, target);
        }

        // 주체가 효과 (도트 피해 등)
        public static Entity Create(Effect source, Character target) {
            return new Entity(EntityID.None, null, null, source, target);
        }
        public static Entity Create(EntityID id, Effect source, Character target) {
            return new Entity(id, null, null, source, target);
        }
    }
}
