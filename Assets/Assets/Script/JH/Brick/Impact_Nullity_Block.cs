using UnityEngine;

public class Impact_Nullity_Block : Brick
{
    protected override void Start()
    {
        block_name = "Impact_Nullity";
        hp = curHp = 30;
        base.Start();
    }

    protected override void OnCollisionEnter(Collision other)
    {

    }
}
