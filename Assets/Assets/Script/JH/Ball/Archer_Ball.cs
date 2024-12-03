using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Ball : Ball
{
    protected override void Start()
    {
        Brick.ball_Dmg = damage;
        miniCam = GameObject.Find("MiniCam").GetComponent<Camera>();
        lineRenderer = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody>();
        previewBall = transform.GetChild(0).gameObject;
        balls.Add(this);

        layerMask = 1 << LayerMask.NameToLayer("wall");
    }

    protected override void Update()
    {
        base.Update();
        Destroy_Ball();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            transform.position -= (Vector3)direction;
            if (Physics.Raycast(transform.position, direction, out hit, rayDistance, layerMask) == false)
                return;
            transform.position = (Vector2)hit.point - direction.normalized * 0.25f;
            reflectDirection = Vector2.Reflect(direction, (Vector2)hit.normal);
            rigid.velocity = reflectDirection * 10;
            direction = rigid.velocity.normalized;
        }
    }
}
