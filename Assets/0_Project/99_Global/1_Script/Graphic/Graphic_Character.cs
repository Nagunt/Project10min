using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Graphics {

    public class Graphic_Character : MonoBehaviour {
        [SerializeField]
        private Animator _animator;

        private readonly int ID_ISMOVE = Animator.StringToHash("IsMove");
        private readonly int ID_ISATTACK = Animator.StringToHash("IsAttack");
        private readonly int ID_ISKNOCKDOWN = Animator.StringToHash("IsKnockDown");
        private readonly int ID_ISDEAD = Animator.StringToHash("IsDead");
        private readonly int ID_ATKSPEED = Animator.StringToHash("AttackSpeed");
        private readonly int ID_DIRX = Animator.StringToHash("dirX");
        private readonly int ID_DIRY = Animator.StringToHash("dirY");

        public void SetDirection(Vector2 dir) {
            _animator.SetFloat(ID_DIRX, dir.x);
            _animator.SetFloat(ID_DIRY, dir.y);
        }

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
