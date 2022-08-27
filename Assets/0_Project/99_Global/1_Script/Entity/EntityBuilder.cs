using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Combat {
    public sealed partial class Entity {

        private Entity(EntityID id, Character ��üĳ����, Artifact ��ü����, Effect ��üȿ��, Character ���ĳ����, Vector2 �߻���ġ) {
            _id = id;
            _��üĳ���� = ��üĳ����;
            _��ü���� = ��ü����;
            _��üȿ�� = ��üȿ��;
            _���ĳ���� = ���ĳ����;
            _position = �߻���ġ;
            _data = new List<DataEntity>();
            _���꿣ƼƼ = new List<List<Entity>>();
        }

        // ��ü�� ���� ȿ�� (���� ���� ��)
        public static Entity Create(Character target) {
            return new Entity(EntityID.None, null, null, null, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Character target) {
            return new Entity(id, null, null, null, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Character target, Vector2 position) {
            return new Entity(id, null, null, null, target, position);
        }

        // ��ü�� ĳ���� (ĳ���� ���� ���� ��)
        public static Entity Create(Character source, Character target) {
            return new Entity(EntityID.None, source, null, null, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Character source, Character target) {
            return new Entity(id, source, null, null, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Character source, Character target, Vector2 position) {
            return new Entity(id, source, null, null, target, position);
        }

        // ��ü�� ���� (�Ļ� ȿ�� ��)
        public static Entity Create(Artifact source, Character target) {
            return new Entity(EntityID.None, source.Owner, source, null, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Artifact source, Character target) {
            return new Entity(id, source.Owner, source, null, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Artifact source, Character target, Vector2 position) {
            return new Entity(id, source.Owner, source, null, target, position);
        }

        // ��ü�� ȿ�� (��Ʈ ���� ��)
        // �ٿ���ü �ý��� ����. ȿ���� ������ / ȿ���� �� ĳ���͸� �����ؾƵǼ�..
        public static Entity Create(Effect source, Character target, Character �ٿ���ü = null) {
            return new Entity(EntityID.None, �ٿ���ü, null, source, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Effect source, Character target, Character �ٿ���ü = null) {
            return new Entity(id, �ٿ���ü, null, source, target, target.transform.position);
        }
        public static Entity Create(EntityID id, Effect source, Character target, Vector2 position, Character �ٿ���ü = null) {
            return new Entity(id, �ٿ���ü, null, source, target, position);
        }
    }
}
