using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Moving_block : Brick
{
    float speed = 0.05f;
    private void Update()
    {
        transform.position += new Vector3(speed, 0);
        if (Mathf.Abs(transform.position.x) > 2)
            speed *= -1;

        tMP_Text.rectTransform.position = transform.position;
        scrollbar.transform.position = Camera.main.WorldToScreenPoint(transform.position - Vector3.up * 0.45f);
    }
    protected override void Start()
    {
        hp = curHp = 30;
        base.Start();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ball"))
            Hit();
    }
}
