using System;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Combat;
using UnityEngine;

namespace TenMinute.Graphics {
    public delegate void GraphicEvent(int index);

    public class Graphic_Entity : MonoBehaviour {

        public GraphicEvent onEvent;

        public Graphic_Entity Build(Entity entity) {
            transform.position = entity.발생위치;
            StartCoroutine(Routine(entity));
            return this;
        }

        private IEnumerator Routine(Entity entity) {
            switch (entity.ID) {
                case EntityID.None:
                    // None 타입은 Animation 없이 즉각 하위 항목들을 적용.
                    for (int i = 0; i < entity.EventCount; ++i) {
                        onEvent?.Invoke(i);
                    }
                    break;
            }
            yield break;
        }
    }
}
