using TMPro;
using UnityEngine;
using static Player_Data;
public class Read_GameData : MonoBehaviour
{
    private GameData gameData;
    public TMP_Text Gold_Text;
    
    private void Awake()
    {
        Json_Test.loadData();
    }

    private void Start()
    {
        gameData = SaveSystem.LoadPlayerData("save_1101");
        Gold_Text.text = gameData.playerData.Money.ToString();
    }
}