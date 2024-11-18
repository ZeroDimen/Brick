using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond_Brick : Brick
{
    protected override void Start()
    {
        curHp = hp = 4;
        block_name = "Diamond";
        base.Start();
    }

    public override void Hit(float dmg = 0)
    {
        curHp -= 1;
        if (curHp <= 0)
            Destroy(gameObject);
        else
        {
            tMP_Text.text = $"{curHp}";
            //scrollbar.size = curHp / hp;
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ball") || other.collider.CompareTag("Ninja"))
            Hit();
    }
}
