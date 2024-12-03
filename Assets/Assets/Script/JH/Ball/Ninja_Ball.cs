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
        Ninja.Add(gameObject);
        StartCoroutine(Collision_Destroy());
    }

    protected override void Update()
    {
        base.Update();
        Destroy_Ball();
    }

    protected override void Destroy_Ball()
    {
        if (transform.position.y < -4f)
        {
            die_count++;

            if (GameManager.manager.player == gameObject)
            {
                for (int i = 1; i < Ninja.Count; i++)
                {
                    if (Ninja[i] != null && Ninja[i] != gameObject)
                    {
                        miniCam.transform.position = new Vector3(0, Ninja[i].transform.position.y, -10);
                        GameManager.manager.player = Ninja[i];
                        break;
                    }
                }
            }

            if (die_count == Ninja.Count)
            {
                Ninja.Clear();
                die_count = 0;
                MiniCam.posReset = true;
                GameManager.manager.Change_State(State.Standby);
            }
            Destroy(gameObject);
        }
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
