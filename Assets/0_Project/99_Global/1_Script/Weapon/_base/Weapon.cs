using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Data;
using UnityEngine;
using UnityEngine.Events;

namespace TenMinute {
    public partial class Weapon {
        private WeaponID _id;
        private Character _owner;
        private int _value;
        private int[] _weaponValues;
        private int _maxCombo;
        private int _currentCombo;

        public WeaponID ID => _id;
        public Character Owner => _owner;
        public int Value => _value;
        public int[] WeaponValues => _weaponValues;
        public bool IsAction => _weaponSequence.IsActive();

        protected Sequence _weaponSequence;

        protected Weapon(WeaponID id) {
            _id = id;
        }
        public virtual void OnObtain(Character owner) {
            Debug.Log(owner + " ¹«±â È¹µæ");
            _owner = owner;
            _owner.Graphic.SetWeaponID(ID);
        }

        public virtual void AddCombo() {
            if (_currentCombo < _maxCombo) {
                _currentCombo++;
            }
        }

        public virtual void Execute(UnityAction onComplete) {}

        public virtual void Clear() {
            _currentCombo = 0;
        }
        public virtual void Cancel() {
            if (_weaponSequence.IsActive()) {
                _weaponSequence.Kill();
            }
            Clear();
        }
    }
}
