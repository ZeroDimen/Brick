using UnityEngine;
using UnityEngine.EventSystems;
public class Map_Button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject Start_UI;
    [SerializeField]
    private GameObject[] End_UI;

    [SerializeField] private GameObject Stage_set;

    public void OnPointerClick(PointerEventData Data)   // 영역 안에서 터치 및 때기 포함
    {
        Stage_set.GetComponent<SelectMap>().changeMap(SwipeUI.instance.currentPage);
        // SelectMap.instance.changeMap(SwipeUI.instance.currentPage); // 아래의 방법은 오브젝트 비활성화로 인해 오류 발생
        
        if (Start_UI.activeSelf == false)
        {
            Start_UI.SetActive(true);
            for (int i = 0; i < End_UI.Length; i++)
            {
                End_UI[i].SetActive(false);
            }
        }
    }
    
}