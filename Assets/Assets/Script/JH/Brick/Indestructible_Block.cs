using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indestructible_Block : Brick
{
    protected override void Start()
    {
        block_name = "Indestructible";
        Bricks.Add(this);
    }
}
