using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Spell_Range : MonoBehaviour
{
    Color color;

    public void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("box"))
            {
                foreach (var block in Brick.Bricks)
                {
                    if (block != null && block.gameObject == collider.gameObject)
                    {
                        block.Hit(40);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("box"))
        {
            color = other.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            other.GetComponent<SpriteRenderer>().color = color;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("box"))
        {
            color = other.GetComponent<SpriteRenderer>().color;
            color.a = 255;
            other.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
