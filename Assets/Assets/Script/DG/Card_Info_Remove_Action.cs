using UnityEngine.EventSystems;
using UnityEngine;
using static Player_Data;
public class Card_Info_Remove_Action : MonoBehaviour, IPointerClickHandler
{
    private CardData card;
    private GameData gameData;
    
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        string Card_Name = Data.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.name;
        Read_GameData.instance.Save_Card_Into_Deck(int.Parse(Card_Name), true);
        Debug.Log(Card_Name);
    }
}