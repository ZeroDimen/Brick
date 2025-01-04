using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha_Card : MonoBehaviour
{
    public float x;
    public float y;
    public int n;
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
    rect.anchoredPosition = new Vector2(Mathf.Lerp(rect.anchoredPosition.x, x, 0.1f),
        Mathf.Lerp(rect.anchoredPosition.y, y, 0.1f));
    rect.localScale = new Vector3(Mathf.Lerp(rect.localScale.x, 1f, 0.1f), Mathf.Lerp(rect.localScale.y, 1f, 0.1f), 0);
    }
}
