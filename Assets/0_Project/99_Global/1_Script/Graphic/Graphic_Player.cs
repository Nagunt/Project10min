using System.Collections;
using System.Collections.Generic;
using TenMinute.Data;
using UnityEngine;

namespace TenMinute.Graphics {
    public class Graphic_Player : Graphic_Character {
        private readonly int ID_WEAPON = Animator.StringToHash("Weapon");
        public void SetWeaponID(WeaponID id) {
            _animator.SetInteger(ID_WEAPON, (int)id);
        }
    }
}

