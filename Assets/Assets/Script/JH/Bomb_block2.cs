using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_block2 : Brick
{
    protected override void Start()
    {
        curHp = hp = 50;
        base.Start();
    }

    public override void Hit()
    {
        base.Hit();
        if (curHp <= 0)
        {
            foreach (var brick in Bricks)
            {
                if (brick != null)
                {
                    if (brick != this && Vector2.Distance(brick.transform.position, transform.position) < 2)
                        Destroy(brick.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ball"))
            Hit();
    }
}
