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

        public WeaponID ID => _id;
        public Character Owner => _owner;
        public int Value => _value;
        public int[] WeaponValues => _weaponValues;

        protected Weapon(WeaponID id) {
            _id = id;
        }
        public virtual void OnObtain(Character owner) {
            Debug.Log(owner + " ¹«±â È¹µæ");
            _owner = owner;
        }
        public virtual void OnAttack(UnityAction onComplete) { }
    }
}
