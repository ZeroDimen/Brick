using UnityEngine;
using UnityEngine.EventSystems;
public class SwipeUI_Button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int ChangePage = 0; // 변경할 페이지
    

    public void OnPointerClick(PointerEventData Data)   // 영역 안에서 터치 및 때기 포함
    {
        SwipeUI.instance.ChangePageButton(ChangePage);
    }
}
