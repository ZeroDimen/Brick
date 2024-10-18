using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect_block : Brick
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
            Rigidbody2D ballRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 incomingVelocity = other.relativeVelocity;
            ballRigidbody.velocity = -incomingVelocity.normalized * ballRigidbody.velocity.magnitude;
        }
    }
}
