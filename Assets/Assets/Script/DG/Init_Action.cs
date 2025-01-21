using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Init_Action : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        Json_Test.initData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
