using System.Collections;
using UnityEngine;

public class Surrounding_Heal_Block : Brick
{
    //
    public float test_hp;
    //
    protected override void Start()
    {
        block_name = "Surrounding";
        curHp = hp = 30;
        //
        if (test_hp != 0)
            curHp = hp = test_hp;
        //
        base.Start();
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.manager._state == State.Shoot);
            yield return new WaitUntil(() => GameManager.manager._state == State.Play);

            Heal_Surrounding();
        }
    }

    void Heal_Surrounding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.1f);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("box"))
            {
                foreach (var block in Bricks)
                {
                    if (block != null && block.gameObject == collider.gameObject)
                    {
                        if (block == this)
                            break;

                        if (block.block_name == "Diamond" && block.curHp + 1 <= block.hp)
                            block.curHp += 1;
                        else
                        {
                            if (block.curHp + 10 <= block.hp)
                                block.curHp += 10;
                            else
                                block.curHp = block.hp;
                        }

                        block.tMP_Text.text = $"{block.curHp}";
                    }
                }
            }
        }
        tMP_Text.text = $"{curHp}";
    }
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}
