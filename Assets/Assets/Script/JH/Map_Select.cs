using UnityEngine;

public class Map_Select : MonoBehaviour
{
    public int n;

    public void Cahnge_Map()
    {
        UI_Manager.manager.Map_Change(n);
    }
}
