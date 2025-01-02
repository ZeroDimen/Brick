using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using static Player_Data;

public class Card_Info_Info_Action : MonoBehaviour, IPointerClickHandler
{
    private CardData card;
    
    public GameObject Card_Info_Frame;
    public TMP_Text cards_Name;
    public TMP_Text cards_Level;
    public TMP_Text cards_Damage;
    public TMP_Text cards_Cost;
    
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        string Card_Name = Data.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.name;
        Debug.Log(Card_Name);
        Card_Info_Frame.SetActive(true);
        card = Read_GameData.instance.Read_Card_Info(Card_Name);
        
        cards_Name.text = card.CardName;
        cards_Level.text = card.CardLevel.ToString();
        cards_Damage.text = card.CardDamage.ToString();
        cards_Cost.text = card.CardCost.ToString();
    }
}
