using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Stop_Ball : Ball
{
    bool flag;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Destroy_Ball();
        Time_Stop();
    }

    void Time_Stop()
    {
        if (GameManager.manager._state == State.Shoot && flag == false)
        {
            if (Input.GetMouseButton(0))    // preview ball
            {
                Debug.Log("Working");
                rigid.velocity = Vector3.zero;

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
                // perspective 환경에서는 z값이 중요
                Vector3 screenToWorld = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                mousePos = miniCam.ScreenToWorldPoint(screenToWorld);
                direction = (mousePos - (Vector2)transform.position).normalized;

                // 일반적인 직선이 아닌 원을 발사함
                if (Physics.SphereCast(transform.position, transform.localScale.x / 2, direction, out hit, rayDistance, layerMask) == false)
                    return;

                // CircleCast가 충돌했을때 원의 중심
                previewBall.transform.position = (Vector2)hit.point - direction.normalized * 0.25f;

                // 반사각
                reflectDirection = Vector2.Reflect(direction, (Vector2)hit.normal);


                // 두 직선에 필요한 3개의 점
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, (Vector2)hit.point);
                lineRenderer.SetPosition(2, (Vector2)hit.point + reflectDirection.normalized * 3);

                previewBall.SetActive(true);
                lineRenderer.enabled = true;
            }
            else if (Input.GetMouseButtonUp(0)) // real ball
            {
                rigid.velocity = direction * 10f;
                miniCam.transform.position = new Vector3(0, 0.9f, -10);
                GameManager.manager.Change_State(State.Shoot);
                flag = true;
            }
            else
            {
                lineRenderer.enabled = false;
                previewBall.SetActive(false);
            }
        }
    }
}
