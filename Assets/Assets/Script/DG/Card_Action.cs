using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Action : MonoBehaviour, IPointerClickHandler
{
    public GameObject Card_Use_Frame;
    
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        
        string Card_Name = Data.pointerCurrentRaycast.gameObject.transform.parent.name;
        // 클릭한 UI 요소가 이 객체인지 확인
        if (Data.pointerCurrentRaycast.gameObject == gameObject)
        {
            if (Card_Use_Frame.activeSelf) // card info 창에서 use를 사용했을 경우 
            {
                Card_Use_Frame.SetActive(false);
                Read_GameData.instance.Save_Card_Into_Deck(int.Parse(Card_Name));
            }
            else
            {
                Cards_Image_Making.instance.Clicked_Sprite(Card_Name);
            }
        }
    }
}
