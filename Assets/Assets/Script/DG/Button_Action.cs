using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Action : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public int ButtoneNum;
    public TMP_Text ButtonStatus;
    public TMP_Text CardStatus;

    public void OnPointerClick(PointerEventData Data)   // 영역 안에서 터치 및 때기 포함
    {   
        ButtonStatus.text = "OnPointerClick";
        CardStatus.text = Random_Test.instance.CardNum[ButtoneNum].text;
        Random_Test.instance.DrawCard(ButtoneNum);
    }

    public void OnPointerDown(PointerEventData Data) // 영역 안에서 터치만 
    {
        ButtonStatus.text = "OnPointerDown";
    }

    public void OnPointerUp(PointerEventData Data)  // 영역 안에서 터치, 때는건 영역 밖도 가능
    {
        ButtonStatus.text = "OnPointerUp";
    }
}
