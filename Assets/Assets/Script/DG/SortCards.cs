using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using static Player_Data;

public class SortCards : MonoBehaviour, IPointerClickHandler
{
    public static SortCards instance; //함수 인스턴스화

    public Toggle IsHaveToggle;
    
    private GameData gameData_for_Card;
    private GameData gameData_for_Deck;
    public TMP_Text cards_Sort_By;

    public TMP_Text cards_Num_Have;
    public TMP_Text cards_Num_All;

    private int i;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Sorting("Cost");
        IsHaveToggle.onValueChanged.AddListener(OnButton);
    }
    
    public void OnPointerClick(PointerEventData Data)   // 영역 안에서 터치 및 때기 포함
    {
        i++;
        if (i % 2 == 0)
        {
            i = 0;
        }
            
        if (i == 0)
        {
            Sorting("Cost");
        }
        else if (i == 1)
        {
            Sorting("Level");
        }
        
    }

    private void OnButton(bool isHave)
    {
        isHave = IsHaveToggle.isOn;
        if (i == 0)
        {
            Sorting("Cost");
        }
        else if (i == 1)
        {
            Sorting("Level");
        }
    }
    
    
    public void Sorting(string By)
    {
        cards_Sort_By.text = By;
        gameData_for_Card = SaveSystem.LoadPlayerData("save_1101");
        gameData_for_Card.cardDataList.Cards = gameData_for_Card.cardDataList.Cards.GetRange(8, gameData_for_Card.cardDataList.Cards.Count - 8);
        
        cards_Num_Have.text = gameData_for_Card.cardDataList.Cards.FindAll(card => card.CardIsHave).Count.ToString();
        cards_Num_All.text = gameData_for_Card.cardDataList.Cards.Count.ToString();
        
        if (IsHaveToggle.isOn)
        {
            gameData_for_Card.cardDataList.Cards = gameData_for_Card.cardDataList.Cards.FindAll(card => card.CardIsHave);
        }
        if (By == "Cost")
        {
            gameData_for_Card.cardDataList.Cards.Sort((card1, card2) => card1.CardCost.CompareTo(card2.CardCost));
        }
        else if (By == "Level")
        {
            gameData_for_Card.cardDataList.Cards.Sort((card1, card2) => card1.CardLevel.CompareTo(card2.CardLevel));
        }
        Cards_Image_Making.instance.SpawnCards(gameData_for_Card);

        refresh_Deck();
        
    }

    public void refresh_Deck()
    {
        gameData_for_Deck = SaveSystem.LoadPlayerData("save_1101");
        gameData_for_Deck.cardDataList.Cards = gameData_for_Deck.cardDataList.Cards.GetRange(0,  8);
        Cards_Image_Making.instance.SpawnDeck(gameData_for_Deck);
    }
}
