using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private Queue<int> Deck_Queue = new Queue<int>(); //선입선출 방식의 덱을 구현하기위한 큐
    public GameObject[] Card;
    public GameObject[] Decks = new GameObject[8];
    public GameObject[] cardList;
    public GameObject Menu_Panel;
    public GameObject Map;
    public GameObject Menu;
    public GameObject Select_Map;
    public GameObject Map_1;
    public GameObject[] Maps_1;
    public GameObject Map_2;
    public GameObject[] Maps_2;
    public GameObject Map_2_ver2;
    public GameObject[] Maps_2_ver2;
    public GameObject Map_3;
    public GameObject[] Maps_3;
    public GameObject Default_Map;
    public Slider GaugeBar;
    public TMP_Text tMP_Text;
    public static UI_Manager manager;
    float gauge = 100;
    float _gauge;
    public int UsedCard;
    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(gameObject);

        Add_Queue();

        _gauge = gauge;
    }
    private void Start()
    {
        Add_Deck();
        foreach (var card in Card)
        {
            GameObject obj = Instantiate(Decks[Deck_Queue.Dequeue()], card.transform.position, Quaternion.identity);
            obj.transform.SetParent(card.transform);
        }
        GaugeBar.interactable = false;
        tMP_Text.text = $"{gauge}";
    }

    public bool Gauge_Check(int n)
    {
        if (_gauge - n < 0)
            return false;
        else
            return true;
    }

    public bool Is_Gauge_Zero()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Card[i].transform.GetChild(0).GetComponent<Deck>().cost <= _gauge)
                return false;
        }
        return true;
    }

    public void Gauge(int n)
    {
        _gauge -= n;

        GaugeBar.value = _gauge / gauge;
        tMP_Text.text = $"{_gauge}";
    }
    public void Clock_Ball_Gauge(int n)
    {
        StartCoroutine(clock_Ball_Gauge(n));
    }
    public IEnumerator clock_Ball_Gauge(int n)
    {
        yield return new WaitUntil(() => GameManager.manager._state == State.Play);
        _gauge += n;
        if (_gauge > gauge)
            _gauge = gauge;
        GaugeBar.value = _gauge / gauge;
        tMP_Text.text = $"{_gauge}";
    }

    public void Gauge_Reset()
    {
        _gauge = gauge;
        GaugeBar.value = 1;
        tMP_Text.text = $"{gauge}";
    }

    void Add_Deck()
    {
        Debug.Log(Decks.Length);
        for (int i = 0; i < 8; i++)
        {
            string cardName = GameManager.manager.cardName[i];
            switch (cardName)
            {
                case "Normal" :
                    Decks[i] = cardList[0];
                    break;
                case "Normal +" :
                    Decks[i] = cardList[1];
                    break;
                case "Ninja" :
                    Decks[i] = cardList[2];
                    break;
                case "Flame Magician" :
                    Decks[i] = cardList[3];
                    break;
                case "Archer" :
                    Decks[i] = cardList[4];
                    break;
                case "Big Head" :
                    Decks[i] = cardList[5];
                    break;
                case "Clock" :
                    Decks[i] = cardList[6];
                    break;
            }
        }
    }
    void Add_Queue()
    {
        int[] Deck_Array = new int[Decks.Length];
        for (int i = 0; i < Decks.Length; i++)
        {
            Deck_Array[i] = i;
        }

        Deck_Array = ShuffleArray(Deck_Array);

        for (int i = 0; i < Decks.Length; i++) // 큐에 셔플된 배열 값 저장
        {
            Deck_Queue.Enqueue(Deck_Array[i]);
        }
    }
    private T[] ShuffleArray<T>(T[] array) //배열 섞어주는 함수
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }
    public void DrawCard(int Num = -1) // 카드 선택시 대기열에 있는 카드 드로우, 사용한 카드는 대기열 맨뒤로
    {
        if (Num == -1)
        {
            return;
        }

        int UsedDeck = Num;
        Deck_Queue.Enqueue(UsedDeck);

        Card[4].transform.GetChild(0).transform.position = Card[UsedCard].transform.position;
        Card[4].transform.GetChild(0).transform.GetComponent<Deck>().defalutPos = Card[UsedCard].transform.position;
        Card[4].transform.GetChild(0).SetParent(Card[UsedCard].transform);


        GameObject obj = Instantiate(Decks[Deck_Queue.Dequeue()], Card[4].transform.position, Quaternion.identity);
        obj.transform.SetParent(Card[4].transform);
    }
    public void Menu_Open()
    {
        Menu_Panel.SetActive(true);
        GameManager.manager.Change_State(State.Menu);
    }

    public void Menu_Continue()
    {
        Menu_Panel.SetActive(false);
        GameManager.manager.Change_State(State.Play);
    }
}