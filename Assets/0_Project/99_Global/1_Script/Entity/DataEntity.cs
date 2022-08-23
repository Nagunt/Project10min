using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Combat {

    public enum EntityType {
        None = 0,
        HPȸ��,
        HP����,
        ����,
        ȿ���ο�,           // ȿ���� �ο�
        ȿ��ȸ��,           // ȿ���� ���� �κ� ȸ��
        ȿ������,           // ȿ���� �ƿ� ����
    }

    [System.Flags]
    public enum EntityProperty {
        None = 0,
        ������ġ = 1 << 0,
        ���� = 1 << 1,
        ũ��Ƽ�� = 1 << 2,
        ȿ���鿪 = 1 << 3,
    }

    public sealed partial class DataEntity  {

        private EntityType _type;
        private EntityProperty _property;

        public EntityType Type => _type;
        public EntityProperty Property => _property;

        public void AddProperty(EntityProperty property) => _property |= property;
        public void RemoveProperty(EntityProperty property) => _property &= ~property;

        #region ��ġ���

        private int _������ = 0;            // �⺻ ������.
        private float _�⺻��� = 1f;       // ũ��Ƽ�� � ���. (�⺻ ������ 1.5�� ������)
        private int _������ = 0;           // ������.     (������ 10 '����' ������)
        private float _��� = 1f;          // ���� ���. (������ 20% '����' ������)
        private int _�߰��� = 0;           // ��� � ������� �ʴ� ���� �߰���. (������ 10 '�߰�' ������)
        public void Add�⺻���(float value) => _�⺻��� += value - 1f;
        public void Add������(int value) => _������ += value;
        public void Add���(float value) => _��� += value - 1f;
        public void Add�߰���(int value) => _�߰��� += value;
        public void ��ġ�ʱ�ȭ(int ������) {
            _������ = ������;
            _�⺻��� = 1f;
            _������ = 0;
            _��� = 1f;
            _�߰��� = 0;
        }
        public int ������ => Mathf.FloorToInt((_������ * _�⺻��� + _������) * _��� + _�߰���);             // ������ ������.
        #endregion

        #region ȿ��

        private Effect _ȿ�� = null;
        private bool isȿ����ȿ = false;

        public Effect ȿ�� => _ȿ��;
        public bool Isȿ����ȿ => isȿ����ȿ;

        public void ȿ���鿪() {
            isȿ����ȿ = true;
            AddProperty(EntityProperty.ȿ���鿪);
        }
        public void ȿ����ȿ() {
            isȿ����ȿ = true;
        }
        public void ȿ����ȿ���() {
            isȿ����ȿ = false;
            RemoveProperty(EntityProperty.ȿ���鿪);
        }

        #endregion

        #region ����

        public void SetResultData_����(int prev, int next) {
            Apply������_HP = prev;
            Apply���İ�_HP = next;
        }

        public int Apply������_HP { get; private set; }
        public int Apply���İ�_HP { get; private set; }

        public int �����ط� => Apply������_HP - Apply���İ�_HP;

        #endregion

    }
}
