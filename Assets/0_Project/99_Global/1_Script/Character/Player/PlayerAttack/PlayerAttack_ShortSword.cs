using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute
{
    public class PlayerAttack_ShortSword : PlayerAttack
    { 
        [SerializeField]
        float _stat°æÁ÷ = 0;
        [SerializeField]
        float _stat³Ë¹é = 0;
        [SerializeField]
        float _statATKPercent = 100;
        [SerializeField]
        float _statATKSpeedPercent = 100;
        private void Awake()
        {
            stat°æÁ÷ = _stat°æÁ÷;
            stat³Ë¹é = _stat³Ë¹é;
            statATKPercent = _statATKPercent;
            statATKSpeedPercent = _statATKSpeedPercent;
        }

    }

}
