using System.Collections;
using System.Collections.Generic;
using TenMinute.Combat;
using UnityEngine;
using DG.Tweening;

namespace TenMinute.Graphics {
    public class Graphic_시간파편 : Graphic_Entity {
        protected override IEnumerator Routine(Entity entity) {
            Character source = entity.주체캐릭터;
            Character target = entity.대상캐릭터;

            Vector2 처음위치 = entity.Parent.발생위치;

            float timer = 0f;
            float maxTime = 0.5f;
            while (timer < maxTime) {
                transform.position = Vector2.Lerp(처음위치, target.transform.position, timer / maxTime);
                timer += Time.deltaTime;
                yield return null;
            }


            for (int i = 0; i < entity.EventCount; ++i) {
                onEvent?.Invoke(i);
            }
        }
    }
}
