using JetBrains.Annotations;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public GameObject ball;
    Camera miniCam;
    Vector3 mousePos;
    Vector3 currentMousePos;
    Vector3 newPos;
    Vector3 viewportPos;
    float newPosY;
    float movement;
    bool isDrag;
    public static bool thresholdFlag;
    public static bool camSetFlag;
    public static bool isTurn = true;
    private void Awake()
    {
        miniCam = GetComponent<Camera>();
    }
    void Update()
    {
        if (isTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 마우스가 뷰포트 내부에 있는지
                mousePos = Input.mousePosition;
                viewportPos = miniCam.ScreenToViewportPoint(mousePos);
                if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                    isDrag = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
                isDrag = false;


            // 화면 스크롤
            if (Input.GetMouseButton(0) && isDrag)
            {
                // 스크롤 거리
                currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                movement = currentMousePos.y - mousePos.y;

                // 스크롤 범위
                newPosY = Mathf.Clamp(transform.position.y - movement, 0.9f, 21.5f);

                // 스크롤 이동
                newPos = transform.position;
                newPos.y = newPosY;
                transform.position = newPos;

                mousePos = currentMousePos;
            }
        }
        else if (Ball.isShoot)
        {
            if (!camSetFlag)
            {
                transform.position = new Vector3(0, 0.9f, -10);
                camSetFlag = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (camSetFlag)
        {
            // 공이 아래로 떨어질때
            if (ball.transform.position.y <= -4f)
            {
                // ball.transform.position = new Vector3(0, -4f, 0);
                // transform.position으로 하면 오차가 생김...
                ball.GetComponent<Rigidbody2D>().MovePosition(new Vector3(0, -4f, 0));
                transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, 0.9f, 0.05f), -10);
                if (Mathf.Abs(transform.position.y - 0.9f) <= 0.01f)
                {
                    ball.GetComponent<Ball>().Ball_Reset();
                }
                return;
            }
            Vector3 ballPos = miniCam.WorldToViewportPoint(ball.transform.position);

            if (ballPos.y > 0.6f)
                thresholdFlag = true;

            if ((ballPos.y > 0.6f || ballPos.y < 0.4f) && thresholdFlag)
            {
                Vector3 newVec = transform.position;
                newVec.y = Mathf.Lerp(transform.position.y, ball.transform.position.y, 0.05f);
                transform.position = newVec;
            }

        }
    }
    public void My_Turn()
    {
        Ball.isTurn = false;
        isTurn = true;
        Create_Map.isTurn = false;
    }
}
