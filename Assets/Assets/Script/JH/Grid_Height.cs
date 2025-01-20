using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Height : MonoBehaviour
{
    public int height;

    private void Start()
    {
        GameManager.manager.Open_Grid(height);
    }
}
