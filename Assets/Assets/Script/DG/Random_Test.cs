using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Random_Test : MonoBehaviour
{
    public static Random_Test instance; //함수 인스턴스화
    private Queue<int> Deck_Queue = new Queue<int>(); //선입선출 방식의 덱을 구현하기위한 큐 
    [SerializeField] public TMP_Text[] CardNum; // Text로 화면에 보여지는 카드 순서를 보여주기 위한 배열
    private void Awake()
    {
        
        Random_Test.instance = this;
        
        int[] Deck_Array = new int[8];
        for (int i = 0; i < 8; i++)
        {
            Deck_Array[i] = i;
        }
        
        Deck_Array = ShuffleArray(Deck_Array);

        for (int i = 0; i < 8; i++) // 큐에 셔플된 배열 값 저장
        {
            Deck_Queue.Enqueue(Deck_Array[i]);
        }

    }

    private void Start()
    {
        for (int i = 0; i < CardNum.Length; i++)
        {
            CardNum[i].text = Deck_Queue.Dequeue().ToString();
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
        
        int UsedCard;
        int.TryParse(CardNum[Num].text, out UsedCard);
        Deck_Queue.Enqueue(UsedCard);
        
        
        CardNum[Num].text = CardNum[4].text;
        CardNum[4].text = Deck_Queue.Dequeue().ToString();
    }
   
}
