using System.Collections;
using UnityEngine;

public class Rotation_Hit_Block : Brick
{
    protected override void Start()
    {
        curHp = hp = 30;
        block_name = "Rotation_Hit";
        base.Start();

        StartCoroutine(Rotation_Collider());
    }

    IEnumerator Rotation_Collider()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.manager._state == State.Shoot);
            yield return new WaitUntil(() => GameManager.manager._state == State.Play);
            Rotation();
        }
    }

    void Rotation()
    {
        transform.Rotate(0, 0, -90);
        tMP_Text.transform.Rotate(0, 0, 90);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Fire"))
            fire_ball_hit_count++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
            Hit();
        else if (other.CompareTag("Ninja"))
            Hit(1);
        else if (other.CompareTag("Fire"))
        {
            Hit();
            fire_ball_hit_count++;
        }
    }
}
