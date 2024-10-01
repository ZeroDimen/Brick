using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static Player_Data;

public class Button_Action2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int ButtoneNum;
    public TMP_Text BallName;
    public TMP_Text BallLevel;
    public TMP_Text BallCost;
    public TMP_Text BallDamage;
    public TMP_Text BallCriticalChance;
    public TMP_Text BallCriticalDamage;

    private GameData gameData;
    private BallData ballData;

    private bool isTouching = false;
    private float touchDuration = 0f;
    private float maxTouchDuration = 1f;

    private void Update()
    {
        if (isTouching)
        {
            touchDuration += Time.deltaTime;

            // N초가 경과하면  ReactToTouch 함수 호출 (N = maxTouchDuration)
            if (touchDuration >= maxTouchDuration)
            {
                ReactToTouch();
                ResetTouch();
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData) // 터치 시 저장된 데이터를 읽고 화면에 데이터 출력 및 터치 시간 초기화
    {
        gameData = SaveSystem.LoadPlayerData("save_1101");
        ballData = gameData.ballDataList.Balls[ButtoneNum];

        isTouching = true;
        touchDuration = 0f; // 타이머 초기화
        PrintInfo();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetTouch();
    }

    private void ResetTouch() // 화면에서 손을 때거나 N초가 경과했을 경우 터치 시간을 초기화
    {
        isTouching = false;
        touchDuration = 0f;
    }

    private void ReactToTouch() // 테스트를 위한 레벨업 함수
    {
        Debug.Log("터치가 " + maxTouchDuration + "초간 감지되었습니다!");
        ballData.BallLevel = ballData.BallLevel + 1;
        PrintInfo();
        SaveSystem.SavePlayerData(gameData, "save_1101");
    }

    private void PrintInfo() // 테스트를 위한 화면에 저장된 공의 정보를 출력
    {
        BallName.text = ballData.BallName;
        BallLevel.text = ballData.BallLevel.ToString();
        BallCost.text = ballData.BallCost.ToString();
        BallDamage.text = ballData.BallDamage.ToString();
        BallCriticalChance.text = ballData.BallCriticalChance.ToString();
        BallCriticalDamage.text = ballData.BallCriticalDamage.ToString();
    }
}