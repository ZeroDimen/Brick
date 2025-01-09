using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using static Player_Data;
public class Start_Button : MonoBehaviour, IPointerClickHandler
{
    private GameData gameData;

    public void OnPointerClick(PointerEventData Data)   // 영역 안에서 터치 및 때기 포함
    {
        gameData = SaveSystem.LoadPlayerData("save_1101");
        gameData.playerData.Map = SelectMap.instance.map_number;
        SaveSystem.SavePlayerData(gameData, "save_1101");
        Debug.Log("Start_Button : 저장완료");
        SceneManager.LoadScene("Version1"); // 게임(종현)씬 불러오기
    }
}