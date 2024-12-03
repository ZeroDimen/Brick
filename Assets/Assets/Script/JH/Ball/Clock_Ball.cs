using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_Ball : Ball
{
    int count;
    int gauge;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Destroy_Ball();
    }

    protected override void Destroy_Ball()
    {
        if (transform.position.y < -4f)
            UI_Manager.manager.Clock_Ball_Gauge(gauge);
        base.Destroy_Ball();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        if (other.collider.CompareTag("box"))
        {
            count++;
            if (count % 5 == 0)
                gauge += 10;
        }
    }
}
