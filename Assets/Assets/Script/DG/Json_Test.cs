using System.Collections.Generic;
using UnityEngine;
using static Player_Data;

public class Json_Test : MonoBehaviour
{
    public static void initData() // 테스트로 사용할 정보로 데이터 초기화
    {
        GameData gameData = new GameData()
        {
            playerData = new PlayerData("Player_Test", 30000),
            cardDataList = new CardDataList
            {
                Cards = new List<CardData>
                {
                    new CardData
                    {
                        CardNumber = 0001,CardName = "일반 공", CardLevel = 1, CardDamage = 10 , CardIsHave = true
                    },
                    new CardData
                    {
                        CardNumber = 0002, CardName = "일반 공+", CardLevel = 1,  CardDamage = 15, CardIsHave = false
                    },
                    new CardData
                    {
                        CardNumber = 0003, CardName = "닌자", CardLevel = 1,  CardDamage = 30, CardIsHave = true
                    },
                    new CardData
                    {
                        CardNumber = 0004, CardName = "화염 마법사", CardLevel = 1,  CardDamage = 30, CardIsHave = true
                    },
                    new CardData
                    {
                        CardNumber = 0005, CardName = "궁수", CardLevel = 1,  CardDamage = 30, CardIsHave = true
                    },
                    new CardData
                    {
                        CardNumber = 0006, CardName = "모자람", CardLevel = 1,  CardDamage = 30, CardIsHave = true
                    },
                    new CardData
                    {
                        CardNumber = 0007, CardName = "번개", CardLevel = 2,  CardDamage = 30, CardIsHave = false
                    },
                    new CardData
                    {
                        CardNumber = 0008, CardName = "시계", CardLevel = 2,  CardDamage = 30, CardIsHave = false
                    },
                    new CardData
                    {
                        CardNumber = 0009, CardName = "먹보", CardLevel = 3,  CardDamage = 30, CardIsHave = false
                    },
                    new CardData
                    {
                        CardNumber = 0010, CardName = "블츠", CardLevel = 4,  CardDamage = 30, CardIsHave = false
                    },
                    new CardData
                    {
                        CardNumber = 0011, CardName = "시간정지", CardLevel = 4,  CardDamage = 30, CardIsHave = false
                    },
                    new CardData
                    {
                        CardNumber = 0012, CardName = "발사지정", CardLevel = 5,  CardDamage = 30, CardIsHave = false
                    },
                }
            }
        };
        SaveSystem.SavePlayerData(gameData, "save_1101");
        Debug.Log("데이터 초기화"); //데이터를 정확히 초기화 하였는지 확인하기 위한 로그 출력
    }

    public static void loadData()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
        if (gameData == null) // 저장 파일이 없을 경우 초기화된 파일을 저장
        {
            initData();
            loadData();
        }
        else // 데이터를 정확히 읽었는지 확인하기 위한 로그 출력
        {
            Debug.Log("데이터 읽음");
            // Debug.Log("Name : " + gameData.playerData.Name + ", Money : " + gameData.playerData.Money);
            // foreach (var card in gameData.cardDataList.Cards)
            // {
            //     Debug.Log("CardName : " + card.CardName + ", CardLeveL : " + card.CardLevel);
            // }
        }
    }
}