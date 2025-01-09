using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using System;
using static Player_Data;
using UnityEngine.UI;

public class Card_Info_Info_Action : MonoBehaviour, IPointerClickHandler
{
    private CardData card;
    private GameData gameData;
    
    public GameObject Card_Info_Frame;
    public GameObject Card_Info_Upgrade;
    
    public TMP_Text cards_Name;
    public TMP_Text cards_Level;
    public TMP_Text cards_Damage;
    public TMP_Text cards_Cost;
    
    public TMP_Text Is_Card_Upgrade;
    public TMP_Text cards_Upgrade_Money;
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        // 현제 카드의 이름을 가져오는
        string Card_Name = Data.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.name;
       
        Card_Info_Frame.SetActive(true);
        card = Read_GameData.instance.Read_Card_Info(Card_Name); // 현제 카드의 이름을 통한 정보 가져오는 함수
        
        cards_Name.text = card.CardName;
        cards_Level.text = card.CardLevel.ToString();
        cards_Damage.text = card.CardDamage + " + " + (int)Math.Round(card.CardDamage / 10.0); // 정수형 반올림 계산
        cards_Cost.text = card.CardCost.ToString();
        
        int Upgrade_Money = Cards_Image_Making.instance.Card_Upgrade_Money(card.CardLevel);
        cards_Upgrade_Money.text = Upgrade_Money.ToString();

        Image imageSprite = Card_Info_Upgrade.GetComponent<Image>();
        gameData = SaveSystem.LoadPlayerData("save_1101"); // 플레이어의 제화를 보기위함
        
        if (Is_Card_Upgrade.text == "Upgrade" && gameData.playerData.Money >= Upgrade_Money)
        {
            imageSprite.color = Color.green;
        }
        else
        {
            imageSprite.color = Color.gray;
        }
    }
}
