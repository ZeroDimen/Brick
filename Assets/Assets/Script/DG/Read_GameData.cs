using TMPro;
using UnityEngine;
using System;
using static Player_Data;

public class Read_GameData : MonoBehaviour
{
    public static Read_GameData instance; //함수 인스턴스화
    
    private CardData card;
    private GameData gameData;
    public TMP_Text Gold_Text;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        Json_Test.loadData();
    }

    private void Start()
    {
        refresh_Gold();
    }

    public void refresh_Gold()
    {
        gameData = SaveSystem.LoadPlayerData("save_1101");
        Gold_Text.text = gameData.playerData.Money.ToString();
    }
    public CardData Read_Card_Info(string cardName = null)
    {
        Debug.Log(cardName);
        
        gameData = SaveSystem.LoadPlayerData("save_1101");
        
        for (int i = 0; i < gameData.cardDataList.Cards.Count; i++) // cardName의 이름을 가진 카드 찾는 반복문
        {
            if (cardName == gameData.cardDataList.Cards[i].CardName)
            {
                card = gameData.cardDataList.Cards[i];
                return card;
            }
        }
        // cardName의 이름을 가진 카드가 없다면 숫자로 변환하여 list에 있는 카드 반환
        card = gameData.cardDataList.Cards[int.Parse(cardName)];
        return card;
    }

    public void Save_Card_Into_Deck(int deckNumber, bool delete = false) // 카드를 덱으로 넣는 함수
    {
        gameData = SaveSystem.LoadPlayerData("save_1101");
        

        if (delete) // 추후 수정 필요
        {
            gameData.cardDataList.Cards[deckNumber].CardName = "None";
            gameData.cardDataList.Cards[deckNumber].CardLevel = 0;
            gameData.cardDataList.Cards[deckNumber].CardCost = 0;
            gameData.cardDataList.Cards[deckNumber].CardDamage = 0;
            gameData.cardDataList.Cards[deckNumber].CardCount = 0;
        }
        else if (card.CardCount == 0) // 카드 미소지시 덱으로 못넣게 함
        {
            return;
        }
        else
        {
            gameData.cardDataList.Cards[deckNumber] = card;
        }
        SaveSystem.SavePlayerData(gameData, "save_1101");
        SortCards.instance.Sorting(); // 카드 다시 출력
        Cards_Image_Making.instance.Clicked_Sprite(""); // 카드의 info창을 끄는 함수
    }

    public void Save_Gacha_Result(string CardName = null)
    {
        if (CardName == null)
        {
            Debug.Log("Read_GameData.Cs 오류");
            return;
        }
        gameData = SaveSystem.LoadPlayerData("save_1101"); // 반복해서 호출하는게 별로일거같음
        for (int i = 8; i < gameData.cardDataList.Cards.Count; i++)
        {
            if (gameData.cardDataList.Cards[i].CardName == CardName)
            {
                gameData.cardDataList.Cards[i].CardCount ++;
            }
        }
        SaveSystem.SavePlayerData(gameData, "save_1101");
    }

    public void Card_Lvl_Up(string CardName = null)
    {
        if (CardName == null)
        {
            Debug.Log("Read_GameData.Cs 오류");
            return;
        }

        int currunt_lv = 0;
        gameData = SaveSystem.LoadPlayerData("save_1101");
        
        for (int i = 0; i < gameData.cardDataList.Cards.Count; i++) // cardName의 이름을 가진 카드 찾는 반복문
        {
            if (CardName == gameData.cardDataList.Cards[i].CardName)
            {
                // card = gameData.cardDataList.Cards[i];
                currunt_lv = gameData.cardDataList.Cards[i].CardLevel;
                gameData.cardDataList.Cards[i].CardCount -= Cards_Image_Making.instance.Card_Upgrade_count(currunt_lv);
                gameData.cardDataList.Cards[i].CardDamage += (int)Math.Round(card.CardDamage / 10.0); // 정수형 반올림 계산
                gameData.cardDataList.Cards[i].CardLevel++;
            }
        }
        
        gameData.playerData.Money -= Cards_Image_Making.instance.Card_Upgrade_Money(currunt_lv);
        
        SaveSystem.SavePlayerData(gameData, "save_1101");
        refresh_Gold();
        SortCards.instance.Sorting(); // 카드 다시 출력
    }
    
}