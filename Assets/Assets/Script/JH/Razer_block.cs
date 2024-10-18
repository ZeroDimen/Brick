using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer_block : Brick
{
    protected override void Start()
    {
        curHp = hp = 50;
        base.Start();
    }

    public override void Hit()
    {
        base.Hit();
        foreach (var brick in Bricks)
        {
            if (brick != null)
            {
                if (brick != this && ((brick.transform.position.y == transform.position.y) ||
                 (brick.transform.position.x == transform.position.x)))
                    brick.Hit();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ball"))
            Hit();
    }
}
