using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Deck : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    public static List<GameObject> Deck_List = new List<GameObject>();
    GraphicRaycaster graphic;
    PointerEventData pointer;
    List<RaycastResult> results;
    public GameObject card;
    GameObject obj;
    Camera miniCam;
    public Vector3 defalutPos;
    Vector2 curPos;
    Vector3 viewportPos;
    Image img;
    public int n;
    public int cost;
    bool viewportFlag;
    bool touchFlag;
    bool clickFlag;

    bool IsNextCard;
    private RectTransform rt;
    private void Start()
    {
        Deck_List.Add(gameObject);
        graphic = GameObject.Find("UI").GetComponent<GraphicRaycaster>();
        pointer = new PointerEventData(null);
        results = new List<RaycastResult>();

        miniCam = GameObject.Find("MiniCam").GetComponent<Camera>();
        defalutPos = transform.position;
        img = transform.GetComponent<Image>();
    }
    private void Update()
    {
        Is_Next_Card();
        if (IsNextCard == false)
            Click();
    }

    void Is_Next_Card()
    {
        if (transform.parent.name == "Next_Card") IsNextCard = true;
        else IsNextCard = false;
    }

    // 클릭으로 카드 선택 후 행동
    void Click()
    {
        if (GameManager.manager._state == State.Play || GameManager.manager._state == State.Shoot)
        {
            if (clickFlag == false)
                return;

            // 공을 발사 했을때 해당 카드 제거
            if (GameManager.manager._state == State.Shoot && obj != null && GameManager.manager.player == obj)
            {
                UI_Manager.manager.Gauge(cost);
                UI_Manager.manager.UsedCard = transform.parent.GetComponent<Card>().n;
                GameManager.manager.UsedDeck = n;
                Destroy(gameObject);
            }

            // 해당 카드를 클릭 후 다른 카드를 클릭 했을때 카드 되돌리기
            if (GameManager.manager.player != obj && img.color.a == 0)
            {
                transform.position = defalutPos;
                Destroy(obj);
                img.color = new Color(1, 1, 1, 1);
                viewportFlag = false;
                touchFlag = false;
            }

            // 카드 선택 후 아무 곳 클릭 했을때 공, 스펠, 덱으로 돌리기
            if (clickFlag && Input.GetMouseButtonDown(0))
            {
                viewportPos = miniCam.ScreenToViewportPoint(Input.mousePosition);
                if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
                {
                    // 슬라이더 바 클릭을 위해서... 개선 필요
                    if (GameManager.manager.player != null && GameManager.manager.player.name == "Slide_Ball(Clone)")
                        return;
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
    }

    // 클릭으로 카드 선택
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.manager._state == State.Play && !touchFlag && IsNextCard == false)
        {
            if (UI_Manager.manager.Gauge_Check(cost) == false)
                return;
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

    // 드래그로 카드 선택시 드래그 시작부분
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.manager._state == State.Play && IsNextCard == false)
        {
            if (UI_Manager.manager.Gauge_Check(cost) == false)
                return;
            touchFlag = true;
        }
    }

    // 드래그로 카드 선택시 드래그중
    public void OnDrag(PointerEventData eventData)
    {
        if (touchFlag)
        {
            curPos = eventData.position;
            // transform.position = curPos;
            Vector2 pos;
            Camera cam = GameObject.Find("UI Camera").GetComponent<Camera>();
            rt = transform.parent.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, curPos, cam, out pos);
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
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

    // 드래그로 카드 선택시 드래그 끝날때
    public void OnEndDrag(PointerEventData eventData)
    {
        if (touchFlag)
        {
            if (viewportFlag)   // 미니맵 안
            {
                Vector3 screenToWorld = new Vector3(curPos.x, curPos.y, 10);
                if (miniCam.ScreenToWorldPoint(screenToWorld).y >= -3.5f) // 정상적으로 발사
                {
                    UI_Manager.manager.Gauge(cost);
                    UI_Manager.manager.UsedCard = transform.parent.GetComponent<Card>().n;
                    GameManager.manager.UsedDeck = n;
                    Destroy(gameObject);
                }
                else // 공의 발사각이 너무 낮아 발사 안되는 경우
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
}
