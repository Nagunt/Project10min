using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Graphics {

    public class Graphic_Character : MonoBehaviour {
        [SerializeField]
        private Animator _animator;

        public void SetState_Move(bool state) {
            _animator.SetBool(0, state);
        }

        public void SetState_Attack(bool state) {
            _animator.SetBool(1, state);
        }

        public void SetState_KnockDown(bool state) {
            _animator.SetBool(2, state);
        }

        public void SetState_Dead(bool state) {
            _animator.SetBool(3, state);
        }

        public void SetMotionTime_Attack(float time) {
            _animator.SetFloat(4, 1 / time);
        }
    }
}
