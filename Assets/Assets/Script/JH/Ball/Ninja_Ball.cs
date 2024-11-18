using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_Ball : Ball
{
    static public int die_count;
    public GameObject collision;
    static public List<GameObject> Ninja = new List<GameObject>();
    protected override void Start()
    {
        base.Start();
        Ninja.Add(this.gameObject);
        StartCoroutine(Collision_Destroy());
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Collision_Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        collision = null;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (collision != other.gameObject)
        {
            base.OnCollisionEnter(other);
            if (Ninja.Count < 5 && other.gameObject.CompareTag("box"))
            {
                float angle = Mathf.Min(Vector2.Angle(direction, normal), 90 - Vector2.Angle(direction, normal)) / 2;

                GameObject obj = Instantiate(gameObject, transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 0, -angle) * rigid.velocity;
                obj.GetComponent<Ninja_Ball>().direction = obj.GetComponent<Rigidbody>().velocity.normalized;
                obj.GetComponent<Ninja_Ball>().collision = other.gameObject;

                rigid.velocity = Quaternion.Euler(0, 0, angle) * rigid.velocity;
                direction = rigid.velocity.normalized;
            }
        }
    }
}
