using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twice_Heal_Block : Brick
{
    protected override void Start()
    {
        block_name = "Twice_Heal";
        hp = 50;
        curHp = 5;
        base.Start();
        StartCoroutine(Heal());
    }
    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.manager._state == State.Shoot);
            yield return new WaitUntil(() => GameManager.manager._state == State.Play);
            Heal_Twice();
        }
    }
    void Heal_Twice()
    {
        if (curHp * 2 <= 50)
            curHp *= 2;
        else
            curHp = 50;
        tMP_Text.text = $"{curHp}";
    }
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}
