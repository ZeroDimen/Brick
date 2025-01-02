using UnityEngine;


public class ScrollView_ContentResizer : MonoBehaviour
{
    public static ScrollView_ContentResizer instance; //함수 인스턴스화

    public RectTransform contentRectTransform; // 스크롤 뷰의 콘텐츠 RectTransform
    private float currentWidth; // 현재 사용중인 Content의 가로
    private float currentHeight; // 현재 사용중인 Content의 세로
    [SerializeField] private float increase_Y;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentWidth = contentRectTransform.sizeDelta.x;
        currentHeight = contentRectTransform.sizeDelta.y;
    }

    public void ResizeContent(int Num)
    {
        float nowWidth;
        float nowHeight;

        nowWidth = contentRectTransform.sizeDelta.x;
        if (Num <= 8)
        {
            nowHeight = currentHeight;
        }
        else
        {
            nowHeight = currentHeight + increase_Y * (Num / 4) - increase_Y * 2;
        }


        if (contentRectTransform != null)
        {
            contentRectTransform.sizeDelta = new Vector2(nowWidth, nowHeight);
        }
    }
}