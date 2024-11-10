using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Deck : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    GraphicRaycaster graphic;
    PointerEventData pointer;
    List<RaycastResult> results;
    public GameObject card;
    GameObject obj;
    Camera miniCam;
    Vector2 defalutPos;
    Vector2 curPos;
    Vector3 viewportPos;
    Image img;
    bool viewportFlag;
    bool touchFlag;
    bool clickFlag;

    private void Start()
    {
        graphic = GameObject.Find("UI").GetComponent<GraphicRaycaster>();
        pointer = new PointerEventData(null);
        results = new List<RaycastResult>();
        miniCam = GameObject.Find("MiniCam").GetComponent<Camera>();
        defalutPos = transform.position;
        img = transform.GetComponent<Image>();
    }
    private void Update()
    {
        if (Ball.isShoot && obj != null && GameManager.manager.player == obj)
            Destroy(gameObject);

        if (GameManager.manager.player != obj && img.color.a == 0)
        {
            transform.position = defalutPos;
            Destroy(obj);
            img.color = new Color(1, 1, 1, 1);
            viewportFlag = false;
            touchFlag = false;
        }

        // 아무 곳 클릭 했을때 공, 스펠, 덱으로 돌리기
        if (clickFlag && Input.GetMouseButtonDown(0))
        {
            viewportPos = miniCam.ScreenToViewportPoint(Input.mousePosition);
            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
            {
                // 자기 자신 클릭 안되도록
                pointer.position = Input.mousePosition;
                graphic.Raycast(pointer, results);
                if (results.Count != 0)
                {
                    results.Clear();
                    return;
                }

                transform.position = defalutPos;
                Destroy(obj);
                img.color = new Color(1, 1, 1, 1);
                viewportFlag = false;
                touchFlag = false;
                GameManager.manager.player = null;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Ball.isShoot && GameManager.manager._state == State.Play && !touchFlag)
        {
            if (GameManager.manager.player != null)
                Destroy(GameManager.manager.player);
            obj = Instantiate(card, new Vector3(0, -4f, 0), Quaternion.identity);
            GameManager.manager.player = obj;
            touchFlag = true;
            viewportFlag = true;
            img.color = new Color(1, 1, 1, 0);
            clickFlag = true;
        }
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Ball.isShoot && GameManager.manager._state == State.Play)
            touchFlag = true;
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (touchFlag)
        {
            curPos = eventData.position;
            transform.position = curPos;

            viewportPos = miniCam.ScreenToViewportPoint(curPos);

            // 미니맵에 들어간 경우
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                viewportFlag = true;
                if (obj == null)
                {
                    img.color = new Color(1, 1, 1, 0);
                    obj = Instantiate(card, new Vector3(0, -4f, 0), Quaternion.identity);
                    GameManager.manager.player = obj;
                }
            }

            // 미니맵에서 나오는 경우
            if (viewportFlag)
            {
                // 옆으로 나간 경우
                if ((viewportPos.x < 0 || viewportPos.x > 1) && viewportPos.y >= 0)
                    viewportFlag = false;

                // 아래로 나간 경우
                if (viewportPos.y <= 0)
                {
                    img.color = new Color(1, 1, 1, 1);
                    Destroy(obj);
                    viewportFlag = false;
                    GameManager.manager.player = null;
                }
            }
        }
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        if (viewportFlag)   // 미니맵 안
        {
            Vector3 screenToWorld = new Vector3(curPos.x, curPos.y, 10);
            if (miniCam.ScreenToWorldPoint(screenToWorld).y >= -3.5f)  // 공의 발사각이 너무 낮아 발사 안되는 경우
                Destroy(gameObject);
            else    // 정상적으로 발사
            {
                transform.position = defalutPos;
                img.color = new Color(1, 1, 1, 1);
                Destroy(obj);
                viewportFlag = false;
                touchFlag = false;
                GameManager.manager.player = null;
            }
        }
        else    // 미니맵 밖
        {
            transform.position = defalutPos;
            img.color = new Color(1, 1, 1, 1);
            Destroy(obj);
            viewportFlag = false;
            touchFlag = false;
            GameManager.manager.player = null;
        }
    }
}
