using UnityEngine;

public class Map_Select : MonoBehaviour
{
    public int stage;
    public int n;

    public void Cahnge_Map()
    {
        UI_Manager.manager.Map_Change(stage, n);
    }
}
