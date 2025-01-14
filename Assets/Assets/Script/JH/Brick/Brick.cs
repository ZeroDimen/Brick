using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public static List<Brick> Bricks = new List<Brick>();
    public TMP_Text tMP_Text;
    public float hp;
    public float curHp;
    public String block_name;
    //protected Scrollbar scrollbar;
    public static float ball_Dmg = 10;
    protected int fire_ball_hit_count;
    public static Brick instance;

    private void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        Bricks.Add(this);
        Hp_Setting();
        StartCoroutine(Fire_Ball_Hit());
    }

    private void Hp_Setting()
    {
        tMP_Text = transform.GetChild(0).GetComponent<TMP_Text>();
        //scrollbar = transform.GetChild(1).GetChild(0).GetComponent<Scrollbar>();

        tMP_Text.rectTransform.position = transform.position;
        //scrollbar.transform.position = Camera.main.WorldToScreenPoint(transform.position - Vector3.up * 0.45f);

        tMP_Text.text = $"{curHp}";

        transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }

    public virtual void Hit(float dmg = -1)
    {
        if (dmg == -1)
            curHp -= ball_Dmg;
        else if (dmg == 0)
            return;
        else
            curHp -= dmg;

        if (curHp <= 0)
        {
            Bricks.Remove(this);
            if(Bricks.Count == 0)
                GameManager.manager.GameClearPanel();
            Destroy(gameObject);
        }
        else
        {
            tMP_Text.text = $"{curHp}";
            //scrollbar.size = curHp / hp;
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (block_name == "Indestructible")
            return;

        if (other.collider.CompareTag("ball"))
        {
            if (block_name == "Diamond")
                Hit(1);
            else
                Hit();
        }
        else if (other.collider.CompareTag("Ninja"))
            Hit(1);
        else if (other.collider.CompareTag("Fire"))
        {
            Hit();
            fire_ball_hit_count++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Archer") && block_name != "Indestructible")
        {
            if (block_name == "Diamond")
                Hit(1);
            else
                Hit();
        }
    }

    protected IEnumerator Fire_Ball_Hit()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.manager._state == State.Shoot);
            yield return new WaitUntil(() => GameManager.manager._state == State.Play);
            Hit(fire_ball_hit_count);
            fire_ball_hit_count = 0;
        }
    }

    public void ChangeDmg(float _Dmg)
    {
        ball_Dmg = _Dmg;
    }
}
