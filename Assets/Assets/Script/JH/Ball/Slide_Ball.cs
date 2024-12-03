using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide_Ball : Ball
{
    Slider slider;
    Vector2 pos;
    protected override void Start()
    {
        base.Start();
        pos = transform.position;
        slider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
    }

    protected override void Update()
    {
        base.Update();
        Destroy_Ball();
        if (GameManager.manager._state == State.Play)
        {
            pos.x = (slider.value - 0.5f) * 3.5f;
            transform.position = pos;
        }
    }
}
