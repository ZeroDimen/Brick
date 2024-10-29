using UnityEngine;

public class Ball : MonoBehaviour
{
    public Camera miniCam;
    GameObject previewBall;     // 공의 동선을 보여줄 공
    Rigidbody2D rigid;
    RaycastHit2D hit;
    LayerMask layerMask;        // 공이 부딪힐 레이어 종류
    LineRenderer lineRenderer;  // 공의 동선을 나타낼 선
    Vector2 mousePos;
    Vector2 direction;          // 공이 날아갈 방향
    public static bool isTurn;
    public static bool isShoot;

    float rayDistance = 40f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        previewBall = transform.GetChild(0).gameObject;

        layerMask = 1 << LayerMask.NameToLayer("box") | 1 << LayerMask.NameToLayer("wall") | 1 << LayerMask.NameToLayer("bbox");
    }
    void Update()
    {
        if (isTurn)
        {
            if (Input.GetMouseButton(0) && !isShoot)    // preview ball
            {
                // 마우스가 뷰포트 안인지 체크
                mousePos = Input.mousePosition;
                Vector3 viewportPos = miniCam.ScreenToViewportPoint(mousePos);
                if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
                {
                    lineRenderer.enabled = false;
                    previewBall.SetActive(false);
                    direction = Vector3.zero;
                    return;
                }

                mousePos = miniCam.ScreenToWorldPoint(mousePos);
                direction = (mousePos - (Vector2)transform.position).normalized;

                if (direction.y <= 0.1f)
                {
                    previewBall.SetActive(false);
                    lineRenderer.enabled = false;
                    return;
                }

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
            else if (Input.GetMouseButtonUp(0) && direction.y > 0.1f && !isShoot) // real ball
            {
                rigid.velocity = direction * 10f;
                isShoot = true;
            }
            else
            {
                lineRenderer.enabled = false;
                previewBall.SetActive(false);
            }
        }
    }

    public void Ball_Reset()
    {
        transform.position = new Vector3(0, -4f, 0);
        rigid.velocity = Vector3.zero;
        direction = Vector3.zero;
        isShoot = false;
        Scroll.camSetFlag = false;
        Scroll.thresholdFlag = false;
        miniCam.transform.position = new Vector3(0, 0.9f, -10f);
    }

    public void My_Turn()
    {
        Scroll.isTurn = false;
        isTurn = true;
        Create_Map.isTurn = false;
    }

    public void Clear_Bricks()
    {
        foreach (var blcok in Brick.Bricks)
            if (blcok != null)
                Destroy(blcok.gameObject);
    }
}