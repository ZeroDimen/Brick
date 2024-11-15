using System.Collections;
using UnityEngine;

public class Fire_Spell : MonoBehaviour
{
    public GameObject fire;
    Camera miniCam;
    Vector2 mousePos;
    Color color;
    bool dead;

    void Start()
    {
        miniCam = GameObject.Find("MiniCam").GetComponent<Camera>();
        transform.position = new Vector2(0, -10);
    }

    void Update()
    {
        if (!dead)
        {
            Vector2 viewportPos = miniCam.ScreenToViewportPoint(Input.mousePosition);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 screenToWorld = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                    mousePos = miniCam.ScreenToWorldPoint(screenToWorld);
                    transform.position = mousePos;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Instantiate(fire, new Vector3(mousePos.x, mousePos.y, -10), Quaternion.identity);
                    transform.position = new Vector2(0, -10);
                    GameManager.manager.Change_State(State.Shoot);
                    dead = true;
                    Destroy(gameObject, 1);
                }
            }
            else
            {
                transform.position = new Vector2(0, -10);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("box"))
        {
            color = other.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            other.GetComponent<SpriteRenderer>().color = color;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("box"))
        {
            color = other.GetComponent<SpriteRenderer>().color;
            color.a = 255;
            other.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
