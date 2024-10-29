using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heal_Twice_Brick : Brick
{
    protected override void Start()
    {
        curHp = hp = 5;
        base.Start();
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitUntil(() => Ball.isShoot);
            yield return new WaitUntil(() => !Ball.isShoot);
            Heal_Twice();
        }
    }

    void Heal_Twice()
    {
        curHp = hp = curHp * 2;
        tMP_Text.text = $"{curHp}";
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
            Hit();
    }
}
