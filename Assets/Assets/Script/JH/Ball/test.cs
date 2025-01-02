using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class test : MonoBehaviour
{
    protected Camera miniCam;
    protected GameObject previewBall;     // 공의 동선을 보여줄 공
    protected Rigidbody2D rigid;
    protected List<Vector2> trajectoryPoints = new List<Vector2>();
    protected List<Vector2> trajectoryDirection = new List<Vector2>();
    int collision_count;
    protected LayerMask layerMask;        // 공이 부딪힐 레이어 종류
    protected LineRenderer lineRenderer;  // 공의 동선을 나타낼 선
    protected Vector2 mousePos;
    protected Vector2 reflectDirection;
    [SerializeField]
    public Vector2 direction;          // 공이 날아갈 방향
    protected Vector2 normal;
    protected Vector2 reflect;
    protected Vector2 trajectoryPoint;
    [SerializeField]
    protected Vector2 rayDirection;
    protected Vector2 previewBallPos;
    protected Vector2 previousMousePos;

    public float damage;
    protected float rayDistance = 30f;
    public string ball_Name;

    public float a;
    bool f;

    protected virtual void Start()
    {
        Brick.ball_Dmg = damage;
        miniCam = GameObject.Find("MiniCam").GetComponent<Camera>();
        lineRenderer = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        previewBall = transform.GetChild(0).gameObject;

        layerMask = 1 << LayerMask.NameToLayer("box") | 1 << LayerMask.NameToLayer("wall") | 1 << LayerMask.NameToLayer("bbox");
    }
    protected virtual void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
            Time.timeScale = a;
        if (Input.GetKeyDown(KeyCode.A))
            f = true;
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

                RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.26f, direction, rayDistance, layerMask);
                if (hit)
                {
                    previewBall.transform.position = hit.centroid;
                    reflectDirection = Vector2.Reflect(direction, hit.normal).normalized;
                    
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.centroid);
                    lineRenderer.SetPosition(2, hit.centroid + reflectDirection * 2f);
                }
                
                
                previewBall.SetActive(true);
                lineRenderer.enabled = true;
                previousMousePos = mousePos;
            }
            else if (Input.GetMouseButtonUp(0) && direction.y > 0.1f && GameManager.manager._state == State.Play) // real ball
            {
                direction *= 10f;
                rigid.velocity = direction;
                miniCam.transform.position = new Vector3(0, 0.9f, -10);
                GameManager.manager.Change_State(State.Shoot);
            }
            else
            {
                lineRenderer.enabled = false;
                previewBall.SetActive(false);
            }
        }
    }
    protected virtual void Destroy_Ball()
    {
        if (transform.position.y < -4f)
        {
            MiniCam.posReset = true;
            GameManager.manager.Change_State(State.Standby);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        normal = other.contacts[0].normal;
        reflect = Vector2.Reflect(direction, normal).normalized;
        
        if (rigid.velocity.magnitude != 10)
            rigid.velocity = reflect * 10;

        direction = rigid.velocity;
    }
}
