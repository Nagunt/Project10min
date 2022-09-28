using System;
using TenMinute.Data;
using UnityEngine;

namespace TenMinute {
    public partial class Weapon {
        public static Weapon Create(WeaponID id) {
            Weapon value = null;
            try {
                string name = $"TenMinute.Weapon_{id}";
                Type weaponType = Type.GetType(name);
                if (Activator.CreateInstance(weaponType) is Weapon newWeapon) {
                    value = newWeapon;
                }
            }
            catch (Exception) {
                Debug.Log($"{id} �� �ش��ϴ� ���Ⱑ �������� ����.");
                return null;
            }
            return value;
        }
    }
}
