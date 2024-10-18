using System;
using UnityEngine;
using static Player_Data;
public class Button_UI : MonoBehaviour
{
    [SerializeField] private GameObject[] UI_Buttons;
    
    
    public void OnClick1()
    {
        if (UI_Buttons[0].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[0].SetActive(true);
    }
    public void OnClick2()
    {
        if (UI_Buttons[1].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[1].SetActive(true);
    }
    public void OnClick3()
    {
        if (UI_Buttons[2].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[2].SetActive(true);
    }
    public void OnClick4()
    {
        Json_Test.loadData();
        if (UI_Buttons[3].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[3].SetActive(true);
        Stamina_Test.instance.Info_Stamina();
    }

    public void OnClick5()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
        DateTime now = NTP_Test.GetNetworkTime();
        gameData.timeData.User_Time = now.ToString("yyyy-MM-dd HH:mm:ss");
        SaveSystem.SavePlayerData(gameData, "save_1101");
        // 모바일 기기에서 앱 종료
        Application.Quit();

        // 에디터에서는 플레이 모드를 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
}
