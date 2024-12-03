using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond_Brick : Brick
{
    //
    public float test_hp;
    //
    protected override void Start()
    {
        curHp = hp = 4;
        //
        if (test_hp != 0)
            curHp = hp = test_hp;
        //
        block_name = "Diamond";
        base.Start();

    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ball") || other.collider.CompareTag("Ninja"))
            Hit(1);
        else if (other.collider.CompareTag("Fire"))
        {
            Hit(1);
            fire_ball_hit_count++;
        }
    }
}
