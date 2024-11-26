using UnityEngine;
using UnityEngine.EventSystems;
public class UI_Action : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject Start_UI;
    [SerializeField]
    private GameObject End_UI;

    public void OnPointerClick(PointerEventData Data)   // 영역 안에서 터치 및 때기 포함
    {
        if (Start_UI.activeSelf == false)
        {
            Start_UI.SetActive(true);
            End_UI.SetActive(false);
        }
    }
    
}