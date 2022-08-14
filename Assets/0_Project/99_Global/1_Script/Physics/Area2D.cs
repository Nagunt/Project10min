using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenMinute.Physics
{
    public class Area2D : MonoBehaviour
    {
        [SerializeField]
        private bool isArc = false;
        [SerializeField]
        private Arc2D arc2D;

        private HashSet<Collider2D> checker = new HashSet<Collider2D>();
        private HashSet<Collider2D> container = new HashSet<Collider2D>();

        public Action<Collider2D> onTriggerEnter2D;
        public Action<Collider2D> onTriggerStay2D;
        public Action<Collider2D> onTriggerExit2D;

        private void FixedUpdate()
        {
            if (isArc)
            {
                Collider2D[] cols = arc2D.GetTargets();
                Debug.Log(cols.Length);
                if(cols != null &&
                    cols.Length > 0)
                {
                    checker.Clear();
                    for(int i = 0; i < cols.Length; ++i)
                    {
                        if (container.Contains(cols[i]))
                        {
                            onTriggerStay2D?.Invoke(cols[i]);
                            container.Remove(cols[i]);
                        }
                        else
                        {
                            Debug.Log("Enter");
                            onTriggerEnter2D?.Invoke(cols[i]);
                        }
                        checker.Add(cols[i]);
                    }
                    if(container.Count > 0)
                    {
                        foreach(Collider2D col in container)
                        {
                            onTriggerExit2D?.Invoke(col);
                        }
                        container.Clear();
                    }
                    foreach(Collider2D target in checker)
                    {
                        container.Add(target);
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTriggerEnter2D?.Invoke(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            onTriggerStay2D?.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            onTriggerExit2D?.Invoke(collision);
        }

        public void Clear()
        {
            if (onTriggerEnter2D != null)
            {
                foreach (Delegate e in onTriggerEnter2D.GetInvocationList())
                {
                    onTriggerEnter2D -= (Action<Collider2D>)e;
                }
            }
            if (onTriggerStay2D != null)
            {
                foreach (Delegate e in onTriggerStay2D.GetInvocationList())
                {
                    onTriggerStay2D -= (Action<Collider2D>)e;
                }
            }
            if (onTriggerExit2D != null)
            {
                foreach (Delegate e in onTriggerExit2D.GetInvocationList())
                {
                    onTriggerExit2D -= (Action<Collider2D>)e;
                }
            }
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
