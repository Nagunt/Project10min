using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {
    public class Enemy_Dummy : Enemy {
        public override void Init() {
            base.Init();
            ����HP = 1;
            _HP = 1;
        }
    }
}
