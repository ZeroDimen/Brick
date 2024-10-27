using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    Camera miniCam;
    Vector3 mousePos;
    Vector3 currentMousePos;
    Vector3 newPos;
    Vector3 viewportPos;
    float newPosY;
    float movement;
    bool isDrag;
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
                newPosY = Mathf.Clamp(transform.position.y - movement, 0, 22);

                // 스크롤 이동
                newPos = transform.position;
                newPos.y = newPosY;
                transform.position = newPos;

                mousePos = currentMousePos;
            }
        }
    }
    public void My_Turn()
    {
        Ball.isTurn = false;
        isTurn = true;
    }
}
