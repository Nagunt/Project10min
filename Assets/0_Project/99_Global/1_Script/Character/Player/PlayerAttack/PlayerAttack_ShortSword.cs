using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute
{
    public class PlayerAttack_ShortSword : PlayerAttack
    { 
        [SerializeField]
        float _stat���� = 0;
        [SerializeField]
        float _stat�˹� = 0;
        [SerializeField]
        float _statATKPercent = 100;
        [SerializeField]
        float _statATKSpeedPercent = 100;
        private void Awake()
        {
            stat���� = _stat����;
            stat�˹� = _stat�˹�;
            statATKPercent = _statATKPercent;
            statATKSpeedPercent = _statATKSpeedPercent;
        }

    }

}
