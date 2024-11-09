using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
    public static List<Brick> Bricks = new List<Brick>();
    public TMP_Text tMP_Text;
    public float hp;
    public float curHp;
    public String block_name;
    //protected Scrollbar scrollbar;
    public static float ball_Dmg = 10;

    public static Brick instance;

    private void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        Bricks.Add(this);
        Hp_Setting();
    }

    private void Hp_Setting()
    {
        tMP_Text = transform.GetChild(0).GetComponent<TMP_Text>();
        //scrollbar = transform.GetChild(1).GetChild(0).GetComponent<Scrollbar>();

        tMP_Text.rectTransform.position = transform.position;
        //scrollbar.transform.position = Camera.main.WorldToScreenPoint(transform.position - Vector3.up * 0.45f);

        tMP_Text.text = $"{hp}";

        transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }

    public virtual void Hit(float dmg = 0)
    {
        if (dmg == 0)
            curHp -= ball_Dmg;
        else
            curHp -= dmg;

        if (curHp <= 0)
            Destroy(gameObject);
        else
        {
            tMP_Text.text = $"{curHp}";
            //scrollbar.size = curHp / hp;
        }
    }

    public void ChangeDmg(float _Dmg)
    {
        ball_Dmg = _Dmg;
    }
}
