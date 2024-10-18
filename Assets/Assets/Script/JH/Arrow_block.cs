using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_block : Brick
{
    protected override void Start()
    {
        curHp = hp = 50;
        base.Start();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ball"))
        {
            Hit();
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 10;
        }
    }
}
