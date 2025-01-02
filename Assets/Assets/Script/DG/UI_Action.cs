using UnityEngine;
using UnityEngine.EventSystems;
public class UI_Action : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject Start_UI;
    [SerializeField]
    private GameObject[] End_UI;

    public void OnPointerClick(PointerEventData Data)   // 영역 안에서 터치 및 때기 포함
    {
        if (Start_UI == null) 
        {
            
        }
        else if (Start_UI!=null||Start_UI.activeSelf == false)
        {
            Start_UI.SetActive(true);
        }
        
        for (int i = 0; i < End_UI.Length; i++)
        {
            End_UI[i].SetActive(false);
        }

        if (Cards_Image_Making.instance != null)
        {
            Cards_Image_Making.instance.Clicked_Sprite(""); // 카드의 info창을 끄는 함수
        }
    }
}