using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {
    public class Enemy_SwordMan : Enemy {

        public override void Init() {
            base.Init();
            ½ºÅÝHP = 10;
            _HP = 10;
            ½ºÅÝATK = 10;
            ½ºÅÝDEF = 1;
            ½ºÅÝSpeed = 2;
            ½ºÅÝATKSpeed = 0.25f;
        }



    }
}
