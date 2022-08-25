using System;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Data;
using UnityEngine;

namespace TenMinute {
    public partial class Effect {
        public static Effect Create(EffectID id, int value = 1) {
            Effect target = null;
            try {
                string name = $"TenMinute.Effect_{id}";
                Type effectType = System.Type.GetType(name);
                if (Activator.CreateInstance(effectType, value) is Effect newEffect) {
                    target = newEffect;
                }
            }
            catch (Exception) {
                Debug.Log($"{id} �� �ش��ϴ� ȿ���� �������� ����.");
                return null;
            }
            return target;
        }
    }
}