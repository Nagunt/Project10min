using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute
{
    public class PlayerAttack : MonoBehaviour
    {
        
        public float stat���� { get; set; }
        
        public float stat�˹� { get; set; }

        public float statATKPercent { get; set; }

        public float statATKSpeedPercent { get; set; }

        public virtual IEnumerator PlayerFire()
        {
            yield return null;
        }
    }

}
