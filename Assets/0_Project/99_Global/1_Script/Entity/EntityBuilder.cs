using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Combat {
    public sealed partial class Entity {

        private Entity(EntityID id, Character ��üĳ����, Artifact ��ü����, Effect ��üȿ��, Character ���ĳ����) {
            _id = id;
            _��üĳ���� = ��üĳ����;
            _��ü���� = ��ü����;
            _��üȿ�� = ��üȿ��;
            _���ĳ���� = ���ĳ����;
            _data = new List<DataEntity>();
            _���꿣ƼƼ = new List<List<Entity>>();
        }

        // ��ü�� ���� ȿ�� (���� ���� ��)
        public static Entity Create(Character target) {
            return new Entity(EntityID.None, null, null, null, target);
        }
        public static Entity Create(EntityID id, Character target) {
            return new Entity(id, null, null, null, target);
        }

        // ��ü�� ĳ���� (ĳ���� ���� ���� ��)
        public static Entity Create(Character source, Character target) {
            return new Entity(EntityID.None, source, null, null, target);
        }

        public static Entity Create(EntityID id, Character source, Character target) {
            return new Entity(id, source, null, null, target);
        }

        // ��ü�� ���� (�Ļ� ȿ�� ��)
        public static Entity Create(Artifact source, Character target) {
            return new Entity(EntityID.None, null, source, null, target);
        }
        public static Entity Create(EntityID id, Artifact source, Character target) {
            return new Entity(id, null, source, null, target);
        }

        // ��ü�� ȿ�� (��Ʈ ���� ��)
        public static Entity Create(Effect source, Character target) {
            return new Entity(EntityID.None, null, null, source, target);
        }
        public static Entity Create(EntityID id, Effect source, Character target) {
            return new Entity(id, null, null, source, target);
        }
    }
}
