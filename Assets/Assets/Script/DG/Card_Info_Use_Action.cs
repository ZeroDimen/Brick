using UnityEngine.EventSystems;
using UnityEngine;
using static Player_Data;
public class Card_Info_Use_Action : MonoBehaviour, IPointerClickHandler
{
    private CardData card;
    private GameData gameData;

    public GameObject Card_Use_Frame;
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        Card_Use_Frame.SetActive(true);
        
        string Card_Name = Data.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.name;
        card = Read_GameData.instance.Read_Card_Info(Card_Name);
    }
}