using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public static GachaManager manager;
    Rito.WeightedRandomPicker<string> wrPicker = new Rito.WeightedRandomPicker<string>();
    public GameObject panel;
    public GameObject card;
    public List<string> picks = new List<string>();
    public List<GameObject> picks_card = new List<GameObject>();    // 카드 지우기용
    public Sprite[] sprites;

    void Start()
    {
        if (manager == null) manager = this;
        else Destroy(gameObject);
        
        wrPicker.Add(
            ("Normal", 1000),
            ("Normal +", 800),
            ("Ninja", 100),
            ("Flame Magician", 300),
            ("Archer", 100),
            ("Big Head", 200),
            ("Clock", 200)
        );
    }

    public void Gacha()
    {
        RandomPicks();
        panel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject obj = Instantiate(card, transform.position, Quaternion.identity);
                obj.GetComponent<Gacha_Card>().x = -300 + 300 * j;
                obj.GetComponent<Gacha_Card>().y = 550 - 350 * i;
                obj.GetComponent<Gacha_Card>().n = i * 3 + j;
                obj.transform.SetParent(panel.transform);
                obj.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0);
                picks_card.Add(obj);
                Get_Img(obj, picks[i * 3 + j]);
            }
        }
    }
    public void Get_Img(GameObject obj, string name)
    {
        Sprite sprite = obj.GetComponent<Image>().sprite;

        if (name == "Normal")
            sprite = sprites[0];
        else if (name == "Normal +")
            sprite = sprites[1];
        else if (name == "Ninja")
            sprite = sprites[2];
        else if (name == "Flame Magician")
            sprite = sprites[3];
        else if (name == "Archer")
            sprite = sprites[4];
        else if (name == "Big Head")
            sprite = sprites[5];
        else if (name == "Clock")
            sprite = sprites[6];

        obj.GetComponent<Image>().sprite = sprite;
    }

    void RandomPicks()
    {
        for (int i = 0; i < 9; i++)
        {
            string pick = wrPicker.GetRandomPick();
            picks.Add(pick);
        }
    }

    public void Close_Panel()
    {
        foreach (var cards in picks_card)
            Destroy(cards);
        
        picks_card.Clear();
        picks.Clear();
        panel.SetActive(false);
    }
}
