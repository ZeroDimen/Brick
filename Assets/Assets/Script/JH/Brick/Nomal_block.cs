using UnityEngine;

public class Nomal_block : Brick
{
    //
    public float test_hp;
    //
    protected override void Start()
    {
        block_name = "Nomal";
        hp = curHp = 30;
        //
        if (test_hp != 0)
            curHp = hp = test_hp;
        //
        base.Start();
    }
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}
