using System.Collections.Generic;
using UnityEngine;
using static Player_Data;

public class Json_Test : MonoBehaviour
{
    public static void initData() // 테스트로 사용할 정보로 데이터 초기화
    {
        GameData gameData = new GameData()
        {
            playerData = new PlayerData("HI", 30000),
            ballDataList = new BallDataList
            {
                Balls = new List<BallData>
                {
                    new BallData
                    {
                        BallName = "Ball 1", BallLevel = 1, BallCost = 1, BallDamage = 7, BallCriticalDamage = 11,
                        BallCriticalChance = 9
                    },
                    new BallData
                    {
                        BallName = "Ball 2", BallLevel = 0, BallCost = 2, BallDamage = 8, BallCriticalDamage = 10,
                        BallCriticalChance = 9
                    },
                    new BallData
                    {
                        BallName = "Ball 3", BallLevel = 0, BallCost = 3, BallDamage = 9, BallCriticalDamage = 10,
                        BallCriticalChance = 9
                    },
                    new BallData
                    {
                        BallName = "Ball 4", BallLevel = 0, BallCost = 4, BallDamage = 10, BallCriticalDamage = 10,
                        BallCriticalChance = 9
                    },
                    new BallData
                    {
                        BallName = "Ball 5", BallLevel = 0, BallCost = 5, BallDamage = 10, BallCriticalDamage = 10,
                        BallCriticalChance = 9
                    },
                    new BallData
                    {
                        BallName = "Ball 6", BallLevel = 0, BallCost = 6, BallDamage = 10, BallCriticalDamage = 10,
                        BallCriticalChance = 9
                    },
                    new BallData
                    {
                        BallName = "Ball 7", BallLevel = 0, BallCost = 7, BallDamage = 10, BallCriticalDamage = 10,
                        BallCriticalChance = 9
                    },
                    new BallData
                    {
                        BallName = "Ball 8", BallLevel = 0, BallCost = 8, BallDamage = 10, BallCriticalDamage = 10,
                        BallCriticalChance = 9
                    }
                }
            }
        };
        SaveSystem.SavePlayerData(gameData, "save_1101");
        Debug.Log("initData"); //데이터를 정확히 초기화 하였는지 확인하기 위한 로그 출력
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
            Debug.Log("Name : " + gameData.playerData.Name + ", Money : " + gameData.playerData.Money);
            foreach (var ball in gameData.ballDataList.Balls)
            {
                Debug.Log("BallName : " + ball.BallName + ", BallLeveL : " + ball.BallLevel + ", BallCost : " +
                          ball.BallCost);
            }
        }
    }
}