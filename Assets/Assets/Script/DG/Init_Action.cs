using UnityEngine.EventSystems;
using UnityEngine;

public class Init_Action : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        Json_Test.initData();
        Json_Test.loadData();
        SortCards.instance.Sorting("Cost");
        SortCards.instance.refresh_Deck();
    }
}
