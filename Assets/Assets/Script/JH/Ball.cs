using UnityEngine;

public class Ball : MonoBehaviour
{
    public static int power = 10;

    GameObject previewBall;     // 공의 동선을 보여줄 공
    Rigidbody2D rigid;
    RaycastHit2D hit;
    LayerMask layerMask;        // 공이 부딪힐 레이어 종류
    LineRenderer lineRenderer;  // 공의 동선을 나타낼 선
    Vector2 mousePos;
    Vector2 direction;          // 공이 날아갈 방향

    float rayDistance = 20f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        previewBall = transform.GetChild(0).gameObject;

        layerMask = 1 << LayerMask.NameToLayer("box") | 1 << LayerMask.NameToLayer("wall") | 1 << LayerMask.NameToLayer("bbox");
    }
    void Update()
    {
        if (Input.GetMouseButton(0))    // preview ball
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - (Vector2)transform.position).normalized;

            // 일반적인 직선이 아닌 원을 발사함
            hit = Physics2D.CircleCast(transform.position, 0.25f, direction, rayDistance, layerMask);

            // CircleCast가 충돌했을때 원의 중심
            previewBall.transform.position = hit.centroid;

            // 반사각
            Vector2 reflectDirection = Vector2.Reflect(direction, hit.normal);

            // 두 직선에 필요한 3개의 점
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.centroid);
            lineRenderer.SetPosition(2, hit.centroid + reflectDirection.normalized * 3);

            previewBall.SetActive(true);
            lineRenderer.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0)) // real ball
        {
            rigid.velocity = direction * 10f;
        }
        else
        {
            lineRenderer.enabled = false;
            previewBall.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(0, -4.5f, 0);
            rigid.velocity = Vector3.zero;
        }
    }
}