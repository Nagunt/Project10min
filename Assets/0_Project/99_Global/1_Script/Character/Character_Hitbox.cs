using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute {

    [RequireComponent(typeof(Collider2D))]
    public class Character_Hitbox : MonoBehaviour {
        [SerializeField]
        private Character character;
        [SerializeField]
        private Collider2D col2D;

        private void Awake() {
            if (character == null) {
                character = GetComponentInParent<Character>();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            character.onHitboxTriggerEnter?.Invoke(collision);
        }
        private void OnTriggerExit2D(Collider2D collision) {
            character.onHitboxTriggerExit?.Invoke(collision);
        }
        private void OnTriggerStay2D(Collider2D collision) {
            character.onHitboxTriggerStay?.Invoke(collision);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            character.onHitboxCollisionEnter?.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision) {
            character.onHitboxCollisionExit?.Invoke(collision);
        }

        private void OnCollisionStay2D(Collision2D collision) {
            character.onHitboxCollisionStay?.Invoke(collision);
        }
    }


}

