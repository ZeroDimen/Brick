using System.Collections;
using UnityEngine;

public class Surrounding_Heal_Block : Brick
{
    protected override void Start()
    {
        block_name = "Surrounding";
        curHp = hp = 30;
        base.Start();
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitUntil(() => Ball.isShoot);
            yield return new WaitUntil(() => !Ball.isShoot);
            Heal_Surrounding();
        }
    }

    void Heal_Surrounding()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.4f);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("box"))
            {
                foreach (var block in Bricks)
                {
                    if (block != null && block.gameObject == collider.gameObject)
                    {
                        if (block.block_name == "Diamond" && block.curHp + 1 <= block.hp)
                            block.curHp += 1;
                        else if (block.curHp + 10 <= block.hp)
                            block.curHp += 10;

                        block.tMP_Text.text = $"{block.curHp}";
                    }
                }
            }
        }
        tMP_Text.text = $"{curHp}";
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
            Hit();
    }
}
