using TMPro;
using UnityEngine;
using static Player_Data;


public class Stamina_Test : MonoBehaviour
{
    public TMP_Text Stamina;

    public void Info_Stamina()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
        Stamina.text = gameData.playerData.Stamina.ToString();
    }
    
    public void Use_Stamina()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
        gameData.playerData.Stamina -= 1;
        
        SaveSystem.SavePlayerData(gameData, "save_1101");
        Stamina.text = gameData.playerData.Stamina.ToString();
    }

    public void Recover_Stamina()
    {
        GameData gameData = SaveSystem.LoadPlayerData("save_1101");
        
        if (gameData.playerData.Stamina < 5)
        {
            //gameData.timeData.Time_All_Second
        }
    }
}
