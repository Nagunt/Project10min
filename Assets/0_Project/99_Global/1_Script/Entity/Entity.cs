using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TenMinute.Data;
using TenMinute.Graphics;
using UnityEngine;

namespace TenMinute.Combat {

    public enum EntityID {
        None = 0,






        시간파편,





        BowMan타격,
    }

    public sealed partial class Entity {
        private EntityID _id;
        private Vector2 _position;

        private Character _주체캐릭터;
        private Artifact _주체유물;
        private Effect _주체효과;

        private Character _대상캐릭터;

        public bool Has주체 => _주체캐릭터 != null || _주체유물 != null || _주체효과 == null;
        public bool Has대상 => _대상캐릭터 != null;

        public EntityID ID => _id;
        public Vector2 발생위치 => _position;
        public Character 주체캐릭터 => _주체캐릭터;
        public Artifact 주체유물 => _주체유물;
        public Effect 주체효과 => _주체효과;
        public Character 대상캐릭터 => _대상캐릭터;

        public bool IsRoot => _root == null;

        private Entity _parent;
        private Entity _root;

        public Entity Parent => _parent;
        public Entity Root => _root;

        private List<DataEntity> _data;

        private void AddData(DataEntity data) {
            _data.Add(data);
            _서브엔티티.Add(new List<Entity>());
        }

        private List<List<Entity>> _서브엔티티;

        public DataEntity GetData(int index) => _data[index];

        public int EventCount => _data.Count;

        public ReadOnlyCollection<Entity> Get서브엔티티(int index) => _서브엔티티[index].AsReadOnly();

        private void SetRoot(Entity parent) {
            _parent = parent;
            _root = parent.IsRoot ? parent : parent.Root;
        }

        public void Execute() {
            Graphic_Entity graphics = Global_GraphicManager.Instance.GetGraphic(ID);
            for (int i = 0; i < _data.Count; ++i) {
                graphics.onEvent += ExecuteData;
            }
            graphics.Build(this);
        }

        private void ExecuteData(int index) {
            if (_data[index] != null && Has대상) {
                ApplyData(index);
            }
            for (int i = 0; i < _서브엔티티[index].Count; ++i) {
                _서브엔티티[index][i].SetRoot(this);
                _서브엔티티[index][i].Execute();
            }
        }

        private void ApplyData(int index) {
            switch (_data[index].Type) {
                case EntityType.HP지정:
                    대상캐릭터.ApplyHP지정(this, index);
                    break;
                case EntityType.HP회복:
                    대상캐릭터.ApplyHP회복(this, index);
                    break;
                case EntityType.피해:
                    대상캐릭터.Apply피해(this, index);
                    break;
                case EntityType.추가피해:
                    대상캐릭터.Apply추가피해(this, index);
                    break;
                case EntityType.효과부여:
                case EntityType.효과회수:
                case EntityType.효과제거:
                    대상캐릭터.Apply효과(this, index);
                    break;
                case EntityType.경직:
                    대상캐릭터.Apply경직(this, index);
                    break;
                case EntityType.넉백:
                    대상캐릭터.Apply넉백(this, index);
                    break;
            }
            BuildGraphic(index);
        }

        private void BuildGraphic(int index) {
            switch (_data[index].Type) {
                case EntityType.피해:
                    Global_GraphicManager.Instance.GetText().
                        SetText($"{_data[index].총피해량}").
                        SetColor(Color.red).
                        SetPosition(발생위치).
                        Play();
                    break;
                case EntityType.HP회복:
                    Global_GraphicManager.Instance.GetText().
                        SetText($"{_data[index].총회복량}").
                        SetColor(Color.green).
                        SetPosition(발생위치).
                        Play();
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
            AddData(data);
            return this;
        }

        public Entity Add고정피해(int 피해량) {
            DataEntity data = DataEntity.피해(피해량, EntityProperty.고정수치);
            AddData(data);
            return this;
        }

        public Entity Add방어무시피해(int 피해량) {
            DataEntity data = DataEntity.피해(피해량, EntityProperty.방어무시);
            AddData(data);
            return this;
        }

        public Entity Add방어무시고정피해(int 피해량) {
            DataEntity data = DataEntity.피해(피해량, EntityProperty.방어무시 | EntityProperty.고정수치);
            AddData(data);
            return this;
        }

        public Entity Add추가피해(int 피해량) {
            DataEntity data = DataEntity.추가피해(피해량);
            AddData(data);
            return this;
        }

        public Entity Add효과부여(EffectID id, int 부여량 = 1, float 지속시간 = 0f) {
            DataEntity data = DataEntity.효과부여(id, 부여량, 지속시간);
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

        public Entity Add경직(int 경직도) {
            DataEntity data = DataEntity.경직(경직도);
            AddData(data);
            return this;
        }

        public Entity Add넉백(int 넉백도) {
            DataEntity data = DataEntity.넉백(넉백도);
            AddData(data);
            return this;
        }

        public Entity Add추상이벤트() {
            AddData(null);
            return this;
        }

        public Entity Add고유데이터(DataEntity data) {
            AddData(data);
            return this;
        }

        public Entity Add서브엔티티(Entity entity, int index = 0) {
            _서브엔티티[index].Add(entity);
            return this;
        }

        #endregion
    }
}
