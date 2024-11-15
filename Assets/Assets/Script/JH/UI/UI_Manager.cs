using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    private Queue<int> Deck_Queue = new Queue<int>(); //선입선출 방식의 덱을 구현하기위한 큐
    public GameObject[] Card;
    public GameObject[] Deck;
    public GameObject Menu_Panel;
    public GameObject[] Map;
    public GameObject Menu_Button;
    public GameObject Map_Button;
    public static UI_Manager manager;
    public int UsedCard;
    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(gameObject);

        int[] Deck_Array = new int[5];
        for (int i = 0; i < 5; i++)
        {
            Deck_Array[i] = i;
        }

        Deck_Array = ShuffleArray(Deck_Array);

        for (int i = 0; i < 5; i++) // 큐에 셔플된 배열 값 저장
        {
            Deck_Queue.Enqueue(Deck_Array[i]);
        }
    }
    private void Start()
    {
        foreach (var card in Card)
        {
            GameObject obj = Instantiate(Deck[Deck_Queue.Dequeue()], card.transform.position, Quaternion.identity);
            obj.transform.SetParent(card.transform);
        }
    }
    void UsedCard_Chack()
    {

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
        Card[4].transform.GetChild(0).SetParent(Card[UsedCard].transform);

        GameObject obj = Instantiate(Deck[Deck_Queue.Dequeue()], Card[4].transform.position, Quaternion.identity);
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

    public void Menu_Map_Change_Button()
    {
        if (Menu_Button.activeSelf == true)
        {
            Menu_Button.SetActive(false);
            Map_Button.SetActive(true);
        }
        else
        {
            Map_Button.SetActive(false);
            Menu_Button.SetActive(true);
        }
    }

    public void Map_Change(int n)
    {
        foreach (var map in Map)
        {
            map.SetActive(false);
        }

        switch (n)
        {
            case 0:
                Map[0].SetActive(true);
                break;
            case 1:
                Map[1].SetActive(true);
                break;
            case 2:
                Map[2].SetActive(true);
                break;
            case 3:
                Map[3].SetActive(true);
                break;
            case 4:
                Map[4].SetActive(true);
                break;
            case 5:
                Map[5].SetActive(true);
                break;
            case 6:
                Map[6].SetActive(true);
                break;
            case 7:
                Map[7].SetActive(true);
                break;
            case 8:
                Map[8].SetActive(true);
                break;
            case 9:
                Map[9].SetActive(true);
                break;
            case 10:
                Map[10].SetActive(true);
                break;
            case 11:
                Map[11].SetActive(true);
                break;
            case 12:
                Map[12].SetActive(true);
                break;
        }
        return;
    }
}
