using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject Menu_Panel;
    public GameObject[] Map;
    public GameObject Menu_Button;
    public GameObject Map_Button;
    public static UI_Manager manager;
    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(gameObject);
    }
    public void Menu_Open()
    {
        Menu_Panel.SetActive(true);
        GameManager.manager.Change_State(State.Menu);
    }

    public void Menu_Continue()
    {
        Menu_Panel.SetActive(false);
        GameManager.manager.Change_State(State.Play);
    }

    public void Menu_Map_Change_Button()
    {
        if (Menu_Button.activeSelf == true)
        {
            Menu_Button.SetActive(false);
            Map_Button.SetActive(true);
        }
        else
        {
            Map_Button.SetActive(false);
            Menu_Button.SetActive(true);
        }
    }

    public void Map_Change(int n)
    {
        foreach (var map in Map)
        {
            map.SetActive(false);
        }

        switch (n)
        {
            case 0:
                Map[0].SetActive(true);
                break;
            case 1:
                Map[1].SetActive(true);
                break;
            case 2:
                Map[2].SetActive(true);
                break;
            case 3:
                Map[3].SetActive(true);
                break;
            case 4:
                Map[4].SetActive(true);
                break;
            case 5:
                Map[5].SetActive(true);
                break;
            case 6:
                Map[6].SetActive(true);
                break;
            case 7:
                Map[7].SetActive(true);
                break;
            case 8:
                Map[8].SetActive(true);
                break;
            case 9:
                Map[9].SetActive(true);
                break;
            case 10:
                Map[10].SetActive(true);
                break;
            case 11:
                Map[11].SetActive(true);
                break;
            case 12:
                Map[12].SetActive(true);
                break;
        }
        return;
    }
}
