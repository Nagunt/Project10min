using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    SpriteRenderer r;
    Rigidbody2D rb;

    private void Start()
    {
        r = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = Vector2.up * 1.5f;
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
