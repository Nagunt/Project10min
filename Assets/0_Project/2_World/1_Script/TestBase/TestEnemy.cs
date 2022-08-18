using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    SpriteRenderer r;

    private void Start()
    {
        r = GetComponent<SpriteRenderer>();
    }

    public void GetDamaged()
    {
        StartCoroutine(Damaged());
    }
    IEnumerator Damaged()
    {
        r.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        r.color = Color.white;
    }
    
}
