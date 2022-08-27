using System;
using System.Collections;
using System.Collections.Generic;
using TenMinute.Combat;
using UnityEngine;

namespace TenMinute.Graphics {
    public delegate void GraphicEvent(int index);

    public class Graphic_Entity : MonoBehaviour {

        public GraphicEvent onEvent;

        public Action<EntityID, Graphic_Entity> onComplete;

        public Graphic_Entity SetActive(bool state) {
            gameObject.SetActive(state);
            return this;
        }

        public Graphic_Entity Build(Entity entity) {
            transform.position = entity.발생위치;
            StartCoroutine(BuildRoutine(entity));
            return this;
        }

        private IEnumerator BuildRoutine(Entity entity) {
            yield return StartCoroutine(Routine(entity));
            onEvent = null;
            onComplete?.Invoke(entity.ID, this);
        }

        protected virtual IEnumerator Routine(Entity entity) {
            for (int i = 0; i < entity.EventCount; ++i) {
                onEvent?.Invoke(i);
            }
            yield break;
        }
    }
}
