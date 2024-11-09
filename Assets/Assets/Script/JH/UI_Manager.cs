using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject Menu;

    public void Menu_Open()
    {
        Menu.SetActive(true);
        GameManager.manager.Change_State(State.Menu);
    }

    public void Menu_Continue()
    {
        Menu.SetActive(false);
        GameManager.manager.Change_State(State.Play);

    }
}
