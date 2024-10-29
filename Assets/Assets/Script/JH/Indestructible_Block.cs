using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indestructible_Block : Brick
{
    protected override void Start()
    {
        Bricks.Add(this);
    }
}
