using System;
using System.Collections.Generic;
using UnityEngine;
using static Player_Data;

public class Json_Test : MonoBehaviour
{
    public static void initData() // 테스트로 사용할 정보로 데이터 초기화
    {
        DateTime NTP_Time = NTP_Test.GetNetworkTime();
        GameData gameData = new GameData()
        {
            playerData = new PlayerData("HI", 30000, 5),
            ballDataList = new BallDataList
            {
                Balls = new List<BallData>
                {
                    new BallData("Ball 1",1,1,10,11,9),
                    new BallData("Ball 2",1,2,7,11,9),
                    new BallData("Ball 3",1,3,7,11,9),
                    new BallData("Ball 4",1,4,7,11,9),
                    new BallData("Ball 5",1,5,7,11,9),
                    new BallData("Ball 6",1,6,7,11,9),
                    new BallData("Ball 7",1,7,7,11,9),
                    new BallData("Ball 8",1,8,7,11,9)
                }
            },
            timeData = new TimeData(NTP_Time.ToString("yyyy-MM-dd HH:mm:ss"), 0)
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
            Debug.Log("Name : " + gameData.playerData.Name + ", Money : " + gameData.playerData.Money + ", Stamina : " + gameData.playerData.Stamina);
            foreach (var ball in gameData.ballDataList.Balls)
            {
                Debug.Log("BallName : " + ball.BallName + ", BallLeveL : " + ball.BallLevel + ", BallCost : " +
                          ball.BallCost);
            }
            Debug.Log(gameData.timeData.User_Time);
        }
    }
}