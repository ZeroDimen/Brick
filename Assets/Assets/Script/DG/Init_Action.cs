using UnityEngine.EventSystems;
using UnityEngine;

public class Init_Action : MonoBehaviour, IPointerClickHandler
{
    public GameObject Deck_Set;
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        Json_Test.initData();
        Json_Test.loadData();
        Read_GameData.instance.refresh_Gold();
        if (Deck_Set.activeSelf) //Deck_Set이 켜져있을 경우 출력까지
        {
            SortCards.instance.Sorting(); // 카드 다시 출력
        }
    }
}
