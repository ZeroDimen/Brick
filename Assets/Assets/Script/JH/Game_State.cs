using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    TMP_Text tMP_Text;
    private void Awake()
    {
        tMP_Text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        if (Ball.isTurn)
            tMP_Text.text = "State : Ball";
        else
            tMP_Text.text = "State : Map";
    }
}
