using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static List<Ball> balls = new List<Ball>();
    Camera miniCam;
    GameObject previewBall;     // 공의 동선을 보여줄 공
    protected Rigidbody rigid;
    RaycastHit hit;
    LayerMask layerMask;        // 공이 부딪힐 레이어 종류
    LineRenderer lineRenderer;  // 공의 동선을 나타낼 선
    Vector2 mousePos;
    Vector2 reflectDirection;
    public Vector2 direction;          // 공이 날아갈 방향
    protected Vector2 normal;
    Vector2 reflect;

    public float damage;
    float rayDistance = 40f;
    public string ball_Name;

    protected virtual void Start()
    {
        Brick.ball_Dmg = damage;
        miniCam = GameObject.Find("MiniCam").GetComponent<Camera>();
        lineRenderer = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody>();
        previewBall = transform.GetChild(0).gameObject;
        balls.Add(this);

        layerMask = 1 << LayerMask.NameToLayer("box") | 1 << LayerMask.NameToLayer("wall") | 1 << LayerMask.NameToLayer("bbox");
    }
    protected virtual void Update()
    {
        if (GameManager.manager._state == State.Play || GameManager.manager._state == State.Shoot)
        {
            if (Input.GetMouseButton(0) && GameManager.manager._state == State.Play)    // preview ball
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
                // perspective 환경에서는 z값이 중요
                Vector3 screenToWorld = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                mousePos = miniCam.ScreenToWorldPoint(screenToWorld);
                direction = (mousePos - (Vector2)transform.position).normalized;
                if (direction.y <= 0.1f)
                {
                    previewBall.SetActive(false);
                    lineRenderer.enabled = false;
                    return;
                }

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
            else if (Input.GetMouseButtonUp(0) && direction.y > 0.1f && GameManager.manager._state == State.Play) // real ball
            {
                rigid.velocity = direction * 10f;
                miniCam.transform.position = new Vector3(0, 0.9f, -10);
                GameManager.manager.Change_State(State.Shoot);
                if (ball_Name == "Ninja")
                {
                    Ninja_Ball.die_count = 0;
                }
            }
            else
            {
                lineRenderer.enabled = false;
                previewBall.SetActive(false);
            }

            if (transform.position.y < -4f)
            {
                if (ball_Name == "Ninja")
                {
                    Ninja_Ball.die_count++;

                    if (GameManager.manager.player == gameObject)
                    {
                        for (int i = 1; i < Ninja_Ball.Ninja.Count; i++)
                        {
                            if (Ninja_Ball.Ninja[i] != null && Ninja_Ball.Ninja[i] != gameObject)
                            {
                                miniCam.transform.position = new Vector3(0, Ninja_Ball.Ninja[i].transform.position.y, -10);
                                GameManager.manager.player = Ninja_Ball.Ninja[i];
                                break;
                            }
                        }
                    }

                    if (Ninja_Ball.die_count == Ninja_Ball.Ninja.Count)
                    {
                        Ninja_Ball.Ninja.Clear();
                        MiniCam.posReset = true;
                        GameManager.manager.Change_State(State.Standby);
                    }
                    Destroy(gameObject);
                }
                else
                {
                    MiniCam.posReset = true;
                    GameManager.manager.Change_State(State.Standby);
                    Destroy(gameObject);
                }
            }
        }
    }
    protected virtual void OnCollisionEnter(Collision other)
    {
        normal = other.contacts[0].normal;
        reflect = Vector2.Reflect(direction, normal);

        if (rigid.velocity.magnitude != 10)
            rigid.velocity = reflect * 10;
        direction = rigid.velocity.normalized;
    }
}