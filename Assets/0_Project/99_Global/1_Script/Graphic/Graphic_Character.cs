using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Graphics {

    public class Graphic_Character : MonoBehaviour {
        [SerializeField]
        private Animator _animator;

        private readonly string ID_ISMOVE = "IsMove";
        private readonly string ID_ISATTACK = "IsAttack";
        private readonly string ID_ISKNOCKDOWN = "IsKnockDown";
        private readonly string ID_ISDEAD = "IsDead";
        private readonly string ID_ATKSPEED = "AttackSpeed";

        public void SetState_Move(bool state) {
            _animator.SetBool(ID_ISMOVE, state);
        }

        public void SetState_Attack(bool state) {
            _animator.SetBool(ID_ISATTACK, state);
        }

        public void SetState_KnockDown(bool state) {
            _animator.SetBool(ID_ISKNOCKDOWN, state);
        }

        public void SetState_Dead(bool state) {
            _animator.SetBool(ID_ISDEAD, state);
        }

        public void SetMotionTime_Attack(float time) {
            _animator.SetFloat(ID_ATKSPEED, 1 / time);
        }
    }
}
