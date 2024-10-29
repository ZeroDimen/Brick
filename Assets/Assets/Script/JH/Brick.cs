using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
    public static List<Brick> Bricks = new List<Brick>();
    protected TMP_Text tMP_Text;
    protected float hp;
    protected float curHp;
    protected Scrollbar scrollbar;
    private static float ball_Dmg = 10;

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
        scrollbar = transform.GetChild(1).GetChild(0).GetComponent<Scrollbar>();

        tMP_Text.rectTransform.position = transform.position;
        scrollbar.transform.position = Camera.main.WorldToScreenPoint(transform.position - Vector3.up * 0.45f);

        tMP_Text.text = $"{hp}";

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }

    public virtual void Hit()
    {
        curHp -= ball_Dmg;
        if (curHp <= 0)
            Destroy(gameObject);
        else
        {
            tMP_Text.text = $"{curHp}";
            scrollbar.size = curHp / hp;
        }
    }

    public void ChangeDmg(float _Dmg)
    {
        ball_Dmg = _Dmg;
    }
}
