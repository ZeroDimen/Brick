using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nomal_block : Brick
{
    protected override void Start()
    {
        block_name = "Nomal";
        hp = curHp = 30;
        base.Start();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ball"))
            Hit();
    }
}
