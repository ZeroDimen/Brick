using UnityEngine;
using UnityEngine.EventSystems;
using static Player_Data;
public class Button_Action : MonoBehaviour , IPointerDownHandler
{
    public int Button_Num;
    public SpriteRenderer Ball;

    private GameData gameData;
    private float BallDamage;
    

    private void Awake()
    {
        Json_Test.loadData();
    }
    
    public void OnPointerDown(PointerEventData eventData) // 터치 시 
    {
        gameData = SaveSystem.LoadPlayerData("save_1101");
        BallDamage = gameData.ballDataList.Balls[Button_Num].BallDamage;
        Brick.instance.ChangeDmg(BallDamage);
   
        if (Button_Num == 0)
        {
            Ball.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (Button_Num == 1)
        {
            Ball.color = new Color(0.2f, 0.5f, 0.15f, 1f);
        }
        else if (Button_Num == 2)
        {
            Ball.color = new Color(0.85f, 0.35f, 0.15f, 1f);
        }

        Debug.Log("BallDamage : " + BallDamage);
    }
    
}