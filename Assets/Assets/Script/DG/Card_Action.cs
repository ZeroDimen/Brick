using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Action : MonoBehaviour, IPointerClickHandler
{
    public GameObject Card_Info;

    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        // 클릭한 UI 요소가 이 객체인지 확인
        if (Data.pointerCurrentRaycast.gameObject == gameObject)
        {
            if (!Card_Info.activeSelf)
            {
                Card_Info.SetActive(true); // 클릭 시 활성화
                Debug.Log("활성화");
            }
            else
            {
                Card_Info.SetActive(false);
            }
        }
    }
}
