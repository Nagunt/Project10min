using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Data {

    public class DataInt {

        private int _������ = 0;            // �⺻ ������.
        private float _�⺻��� = 1f;       // ũ��Ƽ�� � ���. (�⺻ ������ 1.5�� ������)
        private int _������ = 0;           // ������.     (������ 10 '����' ������)
        private float _��� = 1f;          // ���� ���. (������ 20% '����' ������)
        private int _�߰��� = 0;           // ��� � ������� �ʴ� ���� �߰���. (������ 10 '�߰�' ������)

        public int ������ => Mathf.FloorToInt((_������ * _�⺻��� + _������) * _��� + _�߰���);

        public DataInt(int �ʱⵥ���� = 0) {
            ��ġ�ʱ�ȭ(�ʱⵥ����);
        }

        public DataInt Add�⺻���(float value) {
            _�⺻��� += value - 1f;
            return this;
        }
        public DataInt Add������(int value) {
            _������ += value;
            return this;
        }
        public DataInt Add���(float value) {
            _��� += value - 1f;
            return this;
        }
        public DataInt Add�߰���(int value) {
            _�߰��� += value;
            return this;
        }
        public DataInt ��ġ�ʱ�ȭ(int ������) {
            _������ = ������;
            _�⺻��� = 1f;
            _������ = 0;
            _��� = 1f;
            _�߰��� = 0;
            return this;
        }
    }

    public class DataFloat {

        private float _������ = 0;            // �⺻ ������.
        private float _�⺻��� = 1f;       // ũ��Ƽ�� � ���. (�⺻ ������ 1.5�� ������)
        private int _������ = 0;           // ������.     (������ 10 '����' ������)
        private float _��� = 1f;          // ���� ���. (������ 20% '����' ������)
        private int _�߰��� = 0;           // ��� � ������� �ʴ� ���� �߰���. (������ 10 '�߰�' ������)

        public float ������ => (_������ * _�⺻��� + _������) * _��� + _�߰���;

        public DataFloat(int �ʱⵥ���� = 0) {
            ��ġ�ʱ�ȭ(�ʱⵥ����);
        }

        public DataFloat Add�⺻���(float value) {
            _�⺻��� += value - 1f;
            return this;
        }
        public DataFloat Add������(int value) {
            _������ += value;
            return this;
        }
        public DataFloat Add���(float value) {
            _��� += value - 1f;
            return this;
        }
        public DataFloat Add�߰���(int value) {
            _�߰��� += value;
            return this;
        }
        public DataFloat ��ġ�ʱ�ȭ(int ������) {
            _������ = ������;
            _�⺻��� = 1f;
            _������ = 0;
            _��� = 1f;
            _�߰��� = 0;
            return this;
        }
    }

}