using System;
using TMPro;
using UnityEngine;
using static Player_Data;


public class Stamina_Test : MonoBehaviour
{
    public TMP_Text Stamina;    // 화면에 표시될 스테미너
    public TMP_Text Recover_Time;

    private int Recover_cycle = 10; // 스테미너가 회복 되는 시간
    private int Max_Stamina = 5; // 최대 스테미너

    private TimeSpan timespan;

    private DateTime userindate;
    private DateTime now;

    public static Stamina_Test instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        InvokeRepeating("Time_Check", 0f, 1f);
        InvokeRepeating("Recover_Stamina", 0f, 1f);
        InvokeRepeating("Info_Stamina", 0f, 1f);
    }

    public void Time_Check() // 현제 시간과 마지막으로 스테미너를 사용한 시간을 비교
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
        now = NTP_Test.GetNetworkTime();
        userindate = Convert.ToDateTime(gameData.timeData.User_Time);
        
        if (gameData.playerData.Stamina < Max_Stamina)
        {
            timespan = now - userindate;
        }
        else
        {
            timespan = TimeSpan.Zero;
        }
        
    }

    public void Info_Stamina()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");

        Stamina.text = gameData.playerData.Stamina.ToString();
        Debug.Log("gameData.timeData.Seconds : " + gameData.timeData.Seconds);
        if (gameData.playerData.Stamina < Max_Stamina)
        {
            Recover_Time.text = (Recover_cycle - gameData.timeData.Seconds  - (int)timespan.TotalSeconds % Recover_cycle).ToString();
        }
        else
        {
            Recover_Time.text = "0";
        }
    }

    public void Use_Stamina()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
        if (gameData.playerData.Stamina > 0)
        {
            gameData.playerData.Stamina -= 1;
            gameData.timeData.User_Time = now.ToString("yyyy-MM-dd HH:mm:ss");
            gameData.timeData.Seconds += (int)timespan.TotalSeconds;
            Debug.Log("Use_Stamina");
    
    
            SaveSystem.SavePlayerData(gameData, "save_1101");
        }
    }
   
    

    public void Recover_Stamina()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
       int Recover_Num = ((int)timespan.TotalSeconds + gameData.timeData.Seconds) / Recover_cycle;

        if (Recover_Num > 0)
        {
            gameData.playerData.Stamina += Recover_Num;
            if (gameData.playerData.Stamina > Max_Stamina)
            {
                gameData.playerData.Stamina = Max_Stamina;
            }

            gameData.timeData.User_Time = now.ToString("yyyy-MM-dd HH:mm:ss");
            gameData.timeData.Seconds = 0;
            SaveSystem.SavePlayerData(gameData, "save_1101");
        }
        Debug.Log("(int)timespan.TotalSeconds : " + (int)timespan.TotalSeconds);
    }
}