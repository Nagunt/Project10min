using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {
    public class Enemy_SwordMan : Enemy {

        public override void Init() {
            base.Init();
            ����HP = 10;
            _HP = 10;
            ����ATK = 10;
            ����DEF = 1;
            ����Speed = 2;
            ����ATKSpeed = 0.25f;
        }



    }
}
