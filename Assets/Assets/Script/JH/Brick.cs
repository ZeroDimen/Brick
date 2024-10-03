using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
    public TMP_Text tMP_Text;
    float hp;
    float curHp;
    Scrollbar scrollbar;
    private float ball_Dmg = 1;
    
    public static Brick instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tMP_Text = transform.GetChild(0).GetComponent<TMP_Text>();
        scrollbar = transform.GetChild(1).GetChild(0).GetComponent<Scrollbar>();
        tMP_Text.rectTransform.position = transform.position;
        scrollbar.transform.position = Camera.main.WorldToScreenPoint(transform.position - Vector3.up * 0.45f);
        curHp = hp = 30;
        tMP_Text.text = $"{hp}";
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
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
    }

    public void ChangeDmg(float _Dmg)
    {
        ball_Dmg = _Dmg;
    }
}
