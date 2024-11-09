using UnityEngine;

public class Fire_Spell : MonoBehaviour
{
    Camera miniCam;
    Vector2 mousePos;
    Color color;

    void Start()
    {
        miniCam = GameObject.Find("MiniCam").GetComponent<Camera>();
        transform.position = new Vector2(0, -10);
    }

    void Update()
    {
        Vector2 viewportPos = miniCam.ScreenToViewportPoint(Input.mousePosition);
        if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
        {
            if (Input.GetMouseButton(0))
            {
                mousePos = miniCam.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Attack();
                GameManager.manager.player = null;
                GameManager.manager.count++;
                if (GameObject.Find("Fire_Spell"))
                    Destroy(GameObject.Find("Fire_Spell"));
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = new Vector2(0, -10);
        }
    }

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
