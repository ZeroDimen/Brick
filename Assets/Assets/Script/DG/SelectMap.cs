using System;
using UnityEngine;

public class SelectMap : MonoBehaviour
{
    public static SelectMap instance; //함수 인스턴스화
    [SerializeField] private GameObject[] Maps;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        foreach (GameObject map in Maps)
        {
            map.SetActive(false);
        }
        Maps[0].SetActive(true);
    }

    public void changeMap(int currentMap)
    {
        foreach (GameObject map in Maps)
        {
            map.SetActive(false);
        }
        Maps[currentMap].SetActive(true);
    }
}
