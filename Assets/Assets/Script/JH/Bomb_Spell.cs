using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Bomb_Spell : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject range;
    public Camera miniCam;
    Vector2 defalutPos;
    Vector2 curPos;
    Vector3 viewportPos;
    Image img;
    bool viewportFlag;
    bool touchFlag;
    bool isTurn;
    public static bool done;
    private void Start()
    {
        defalutPos = transform.position;
        img = transform.GetComponent<Image>();
    }
    private void Update()
    {
        isTurn = Scroll.isTurn;
        Range();
    }

    void Range()
    {
        if (viewportFlag)
        {
            Vector2 rangePos = miniCam.ScreenToWorldPoint(Input.mousePosition);
            range.transform.position = rangePos;
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (isTurn)
            touchFlag = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (touchFlag)
        {
            curPos = eventData.position;
            transform.position = curPos;

            viewportPos = miniCam.ScreenToViewportPoint(curPos);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                viewportFlag = true;
                img.color = new Color(255, 255, 255, 0);
                range.SetActive(true);
            }

            if (viewportFlag)
            {
                if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
                {
                    transform.position = defalutPos;
                    img.color = new Color(255, 255, 255, 255);
                    range.SetActive(false);
                    viewportFlag = false;
                    touchFlag = false;
                }
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (isTurn)
        {
            transform.position = defalutPos;
            img.color = new Color(255, 255, 255, 255);
            range.SetActive(false);
            viewportFlag = false;
            touchFlag = false;


            curPos = eventData.position;
            viewportPos = miniCam.ScreenToViewportPoint(curPos);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
                range.GetComponent<Bomb_Spell_Range>().Attack();
        }
    }
}
