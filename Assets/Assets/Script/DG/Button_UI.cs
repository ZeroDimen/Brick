using UnityEngine;

public class Button_UI : MonoBehaviour
{
    [SerializeField] private GameObject[] UI_Buttons;
    
    public void OnClick1()
    {
        if (UI_Buttons[0].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[0].SetActive(true);
    }
    public void OnClick2()
    {
        if (UI_Buttons[1].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[1].SetActive(true);
    }
    public void OnClick3()
    {
        if (UI_Buttons[2].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[2].SetActive(true);
    }
    public void OnClick4()
    {
        Json_Test.loadData();
        if (UI_Buttons[3].activeSelf)
        {
            return;
        }
        for (int i = 0; i < UI_Buttons.Length ; i++)
        {
            UI_Buttons[i].SetActive(false);
        }
        UI_Buttons[3].SetActive(true);
    }
}
