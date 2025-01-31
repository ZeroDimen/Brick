using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mojaram_Ball : Ball
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Destroy_Ball();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        if (other.collider.CompareTag("box"))
        {
            Brick.ball_Dmg += 1;
        }
    }
}
