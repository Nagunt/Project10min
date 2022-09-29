using System.Collections;
using System.Collections.Generic;
using TenMinute.Data;
using TenMinute.Physics;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace TenMinute {
    public sealed class Weapon_단검 : Weapon {
        public Weapon_단검() : 
            base(WeaponID.단검) {
        }

        public override void Execute(UnityAction onComplete) {
            if (_weaponSequence.IsActive()) return;
            float unitDelay = 1 / Owner.ATKSpeed;
            _weaponSequence = DOTween.Sequence(Owner);
            _weaponSequence.
                OnStart(() => {
                    Owner.Graphic.SetMotionTime_Attack(unitDelay);
                    Owner.Graphic.SetState_Attack(true);
                }).
                AppendInterval(unitDelay).
                AppendCallback(() => {
                    Owner.Graphic.SetState_Attack(false);
                }).
                OnComplete(() => onComplete?.Invoke()).
                OnKill(() => { _weaponSequence = null; }).
                Play();
        }
    }
}