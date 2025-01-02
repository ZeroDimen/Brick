using System;
using System.Collections.Generic;
using UnityEngine;
using static Player_Data;

public class Json_Test : MonoBehaviour
{
    private void Awake()
    {
        throw new NotImplementedException();
    }

    public static void initData() // 테스트로 사용할 정보로 데이터 초기화
    {
        GameData gameData = new GameData()
        {
            playerData = new PlayerData("Player_Test", 30000 , 0),
            cardDataList = new CardDataList()
            {
                Cards = new List<CardData>
                {
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData()
                    {
                        CardName = "None"
                    },
                    new CardData
                    {
                        CardName = "Normal", CardCost = 1, CardLevel = 0, CardDamage = 10,
                        CardIsHave = true
                    },
                    new CardData
                    {
                        CardName = "Normal +", CardCost = 7, CardLevel = 1, CardDamage = 15,
                        CardIsHave = false
                    },
                    new CardData
                    {
                        CardName = "Ninja", CardCost = 6, CardLevel = 2, CardDamage = 30,
                        CardIsHave = true
                    },
                    new CardData
                    {
                        CardName = "Flame Magician", CardCost = 6, CardLevel = 3, CardDamage = 30,
                        CardIsHave = true
                    },
                    new CardData
                    {
                        CardName = "Archer", CardCost = 5, CardLevel = 4, CardDamage = 30,
                        CardIsHave = true
                    },
                    new CardData
                    {
                        CardName = "Big Head", CardCost = 5, CardLevel = 5, CardDamage = 30,
                        CardIsHave = true
                    },
                    new CardData
                    {
                        CardName = "번개", CardCost = 4, CardLevel = 6, CardDamage = 30,
                        CardIsHave = false
                    },
                    new CardData
                    {
                        CardName = "Clock", CardCost = 4, CardLevel = 7, CardDamage = 30,
                        CardIsHave = false
                    },
                    new CardData
                    {
                        CardName = "먹보", CardCost = 3, CardLevel = 8, CardDamage = 30,
                        CardIsHave = false
                    },
                    new CardData
                    {
                        CardName = "블츠", CardCost = 3, CardLevel = 9, CardDamage = 30,
                        CardIsHave = false
                    },
                    new CardData
                    {
                        CardName = "시간정지", CardCost = 2, CardLevel = 10, CardDamage = 30,
                        CardIsHave = false
                    },
                    new CardData
                    {
                        CardName = "발사지정", CardCost = 2, CardLevel = 11, CardDamage = 30,
                        CardIsHave = false
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
        }
    }
}