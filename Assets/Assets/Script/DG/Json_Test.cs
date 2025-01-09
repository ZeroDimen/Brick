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
            playerData = new PlayerData("Player_Test", 30000 , 0 , 0),
            cardDataList = new CardDataList()
            {
                Cards = new List<CardData>
                {
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData()
                    {
                        CardName = "None", CardCost = 0, CardLevel = 0, CardDamage = 0,
                        CardCount = 0
                    },
                    new CardData
                    {
                        CardName = "Normal", CardCost = 1, CardLevel = 1, CardDamage = 10,
                        CardCount = 1000
                    },
                    new CardData
                    {
                        CardName = "Normal +", CardCost = 7, CardLevel = 1, CardDamage = 15,
                        CardCount = 0
                    },
                    new CardData
                    {
                        CardName = "Ninja", CardCost = 6, CardLevel = 2, CardDamage = 30,
                        CardCount = 1
                    },
                    new CardData
                    {
                        CardName = "Flame Magician", CardCost = 6, CardLevel = 3, CardDamage = 30,
                        CardCount = 1
                    },
                    new CardData
                    {
                        CardName = "Archer", CardCost = 5, CardLevel = 4, CardDamage = 30,
                        CardCount = 1
                    },
                    new CardData
                    {
                        CardName = "Big Head", CardCost = 5, CardLevel = 5, CardDamage = 30,
                        CardCount = 1
                    },
                    new CardData
                    {
                        CardName = "번개", CardCost = 4, CardLevel = 1, CardDamage = 30,
                        CardCount = 0
                    },
                    new CardData
                    {
                        CardName = "Clock", CardCost = 4, CardLevel = 1, CardDamage = 30,
                        CardCount = 0
                    },
                    new CardData
                    {
                        CardName = "먹보", CardCost = 3, CardLevel = 1, CardDamage = 30,
                        CardCount = 0
                    },
                    new CardData
                    {
                        CardName = "블츠", CardCost = 3, CardLevel = 1, CardDamage = 30,
                        CardCount = 0
                    },
                    new CardData
                    {
                        CardName = "시간정지", CardCost = 2, CardLevel = 1, CardDamage = 30,
                        CardCount = 0
                    },
                    new CardData
                    {
                        CardName = "발사지정", CardCost = 2, CardLevel = 1, CardDamage = 30,
                        CardCount = 0
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