using UnityEngine;

public class MiniCam : MonoBehaviour
{
    Camera miniCam;
    Vector3 mousePos;
    Vector3 currentMousePos;
    Vector3 newPos;
    Vector3 viewportPos;
    float newPosY;
    float movement;
    bool isDrag;
    public static bool posReset;
    public static bool thresholdFlag;
    private void Awake()
    {
        miniCam = GetComponent<Camera>();
    }
    void Update()
    {
        if (GameManager.manager.player == null && GameManager.manager._state == State.Play)
        {
            Scroll();
        }
    }
    private void FixedUpdate()
    {
        Follow_Ball();
        Camera_Position_Reset();
    }

    void Scroll()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.manager._state == State.Play)
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

    void Follow_Ball()
    {
        if (GameManager.manager._state == State.Shoot)
        {
            if (GameManager.manager.player == null)
                return;

            Vector3 ballPos = miniCam.WorldToViewportPoint(GameManager.manager.player.transform.position);

            if (ballPos.y > 0.6f)
                thresholdFlag = true;

            if ((ballPos.y > 0.6f || ballPos.y < 0.4f) && thresholdFlag && transform.position.y >= 0.9f)
            {
                Vector3 newVec = transform.position;
                newVec.y = Mathf.Lerp(transform.position.y, GameManager.manager.player.transform.position.y, 0.05f);
                transform.position = newVec;
            }
        }
    }
    void Camera_Position_Reset()
    {
        if (posReset)
        {
            transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, 0.9f, 0.2f), -10);
            if (Mathf.Abs(transform.position.y - 0.9f) <= 0.01f)
            {
                posReset = false;
                thresholdFlag = false;
                GameManager.manager.Change_State(State.Play);
            }
        }
    }
}
