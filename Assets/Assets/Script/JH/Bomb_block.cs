using UnityEngine;

public class Bomb_block : Brick
{
    protected override void Start()
    {
        curHp = hp = 50;
        base.Start();
    }

    public override void Hit(float dmg = 0)
    {
        base.Hit();
        foreach (var brick in Bricks)
        {
            if (brick != null)
            {
                if (brick != this && Vector2.Distance(brick.transform.position, transform.position) < 2)
                    brick.Hit();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ball"))
            Hit();
    }
}
