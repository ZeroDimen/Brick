using UnityEngine;
using UnityEngine.UI;
using static Player_Data;
using System.Collections.Generic; // List 사용을 위한 네임스페이스 추가
using TMPro;
using System.Linq;
public class Cards_Image_Making : MonoBehaviour
{
    public static Cards_Image_Making instance; //함수 인스턴스화

    public GameObject deckPrefab; // 생성할 덱 이미지 프리팹
    public Transform parentTransform_deckPrefab; // 부모 Transform
    
    public TMP_Text deck_Cost;
    public TMP_Text deck_Level;
    public TMP_Text deck_Count;
    public TMP_Text deck_Upgrade_text;
    
    public GameObject cards_Sprite_Deck;

    
    public GameObject cardPrefab; // 생성할 카드 이미지 프리팹
    public Transform parentTransform_cardPrefab; // 부모 Transform
    
    public TMP_Text cards_Cost;
    public TMP_Text cards_Level;
    public TMP_Text cards_Count;
    public TMP_Text cards_Upgrade_text;
    
    public GameObject cards_Sprite_Card;
    

    [SerializeField] private float increase_X;
    [SerializeField] private float increase_Y;
    [SerializeField] private Sprite[] cards_Sprite_List; // 생성할 이미지 개수

    private GameData gameData;
    private GameObject[] card_Clone;
    private GameObject[] deck_Clone;
    
    private List<GameObject> spawnedDeck = new List<GameObject>(); // 생성된 덱 이미지 리스트
    private List<GameObject> spawnedCards = new List<GameObject>(); // 생성된 카드 이미지 리스트

    public GameObject Card_Use_Frame;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnCards(GameData gameData)
    {
        RemoveAllImages(spawnedCards);
        
        ScrollView_ContentResizer.instance.ResizeContent(gameData.cardDataList.Cards.Count);

        float spawnX = cardPrefab.GetComponent<RectTransform>().anchoredPosition.x;
        float spawnY = cardPrefab.GetComponent<RectTransform>().anchoredPosition.y;

        int NeedCard;

        card_Clone = new GameObject[gameData.cardDataList.Cards.Count]; 
        
        for (int i = 0; i < gameData.cardDataList.Cards.Count; i++)
        {
            Image imageSprite = cards_Sprite_Card.GetComponent<Image>();
            if (gameData.cardDataList.Cards[i].CardCount == 0 && gameData.cardDataList.Cards[i].CardLevel == 1)
            {
                imageSprite.color = Color.HSVToRGB(0f, 0f, 50 / 255f);
                imageSprite.color = new Color(154 / 255f, 154 / 255f, 154 / 255f, 255f / 255f);
            }
            else
            {
                imageSprite.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            }

            for (int j = 1; j < cards_Sprite_List.Length; j++)
            {
                if (gameData.cardDataList.Cards[i].CardName == cards_Sprite_List[j].name)
                {
                    imageSprite.sprite = cards_Sprite_List[j];
                    break;
                }
                else
                {
                    imageSprite.sprite = cards_Sprite_List[0];
                }
            }
            
            cards_Level.text = gameData.cardDataList.Cards[i].CardLevel.ToString();
            cards_Cost.text = gameData.cardDataList.Cards[i].CardCost.ToString();
            
            NeedCard = Card_Upgrade_count(gameData.cardDataList.Cards[i].CardLevel);
            // cards_Count.text = gameData.cardDataList.Cards[i].CardCount.ToString();
            cards_Count.text = gameData.cardDataList.Cards[i].CardCount + " /  " + NeedCard;
            if (gameData.cardDataList.Cards[i].CardCount >= NeedCard)
            {
                cards_Upgrade_text.text = "Upgrade";
            }
            else
            {
                cards_Upgrade_text.text = "Info";
            }

            // UI 생성 개수에 따른 좌표 이동
            if (i == 0)
            {
            }
            else if (i % 4 == 0)
            {
                spawnX -= increase_X * 3;
                spawnY -= increase_Y;
            }
            else
            {
                spawnX += increase_X;
            }

            Vector2 spawnPos = new Vector2(spawnX, spawnY);


            // Image UI 생성
            card_Clone[i] = Instantiate(cardPrefab, spawnPos, Quaternion.identity, parentTransform_cardPrefab);
            card_Clone[i].name = gameData.cardDataList.Cards[i].CardName;
            
            card_Clone[i].SetActive(true);
            // 좌표 변경
            RectTransform imageRect = card_Clone[i].GetComponent<RectTransform>();
            imageRect.anchoredPosition = spawnPos;
            spawnedCards.Add(card_Clone[i]); // 생성된 이미지 리스트에 추가
        }
        
        //완성된 Image 프리펩들을 Info 창을 통한 의도적 가리기를 위해 Hierarchy 순서 변경
        for (int i = 0; i < gameData.cardDataList.Cards.Count; i++)
        {
            card_Clone[i].transform.SetSiblingIndex(gameData.cardDataList.Cards.Count - i);
        }
        
    }



    public void SpawnDeck(GameData gameData)
    {
        RemoveAllImages(spawnedDeck);
            
        float spawnX = deckPrefab.GetComponent<RectTransform>().anchoredPosition.x;
        float spawnY = deckPrefab.GetComponent<RectTransform>().anchoredPosition.y;
        
        int NeedCard;
        
        deck_Clone = new GameObject[gameData.cardDataList.Cards.Count];
        
        for (int i = 0; i < gameData.cardDataList.Cards.Count; i++)
        {
            Image imageSprite = cards_Sprite_Deck.GetComponent<Image>();
            imageSprite.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            
            for (int j = 1; j < cards_Sprite_List.Length; j++)
            {
                if (gameData.cardDataList.Cards[i].CardName == cards_Sprite_List[j].name)
                {
                    imageSprite.sprite = cards_Sprite_List[j];
                    break;
                }
                else
                {
                    imageSprite.sprite = cards_Sprite_List[0];
                }
            }
            
            deck_Level.text = gameData.cardDataList.Cards[i].CardLevel.ToString();
            deck_Cost.text = gameData.cardDataList.Cards[i].CardCost.ToString();
            
            NeedCard = Card_Upgrade_count(gameData.cardDataList.Cards[i].CardLevel);
            deck_Count.text = gameData.cardDataList.Cards[i].CardCount + " /  " + NeedCard;
            if (gameData.cardDataList.Cards[i].CardCount >= NeedCard)
            {
                deck_Upgrade_text.text = "Upgrade";
            }
            else
            {
                deck_Upgrade_text.text = "Info";
            }

            // UI 생성 개수에 따른 좌표 이동
            if (i == 0)
            {
            }
            else if (i % 4 == 0)
            {
                spawnX -= increase_X * 3;
                spawnY -= increase_Y;
            }
            else
            {
                spawnX += increase_X;
            }

            Vector2 spawnPos = new Vector2(spawnX, spawnY);


            // Image UI 생성
            deck_Clone[i] = Instantiate(deckPrefab, spawnPos, Quaternion.identity, parentTransform_deckPrefab);
            
            // deck_Clone[i].name = gameData.cardDataList.Cards[i].CardName;
            deck_Clone[i].name = i.ToString();
            
            deck_Clone[i].SetActive(true);
            // 좌표 변경
            RectTransform imageRect = deck_Clone[i].GetComponent<RectTransform>();
            imageRect.anchoredPosition = spawnPos;
            spawnedDeck.Add(deck_Clone[i]); // 생성된 이미지 리스트에 추가
        }
        
        //완성된 Image 프리펩들을 Info 창을 통한 의도적 가리기를 위해 Hierarchy 순서 변경
        for (int i = 0; i < gameData.cardDataList.Cards.Count; i++)
        {
            deck_Clone[i].transform.SetSiblingIndex(gameData.cardDataList.Cards.Count - i);
        }
    }
    public void RemoveAllImages(List<GameObject> spawned)
    {
        foreach (GameObject image in spawned)
        {
            Destroy(image); // 게임 오브젝트 제거
        }

        spawned.Clear(); // 리스트 초기화
    }
    

    public void Clicked_Sprite(string Card_Name) // 카드의 info창을 관리하는 함수
    {
        Transform card_Info;
        CardData cardData;
        
        // use 중 위쪽터치로 info 창이 꺼지는것 방지 및 정보가 없는 deck은 info 창 작동 방지
        if (card_Clone == null || Card_Use_Frame.activeSelf  || Card_Name == "") 
        {
            return;
        }
        cardData = Read_GameData.instance.Read_Card_Info(Card_Name);
        if (cardData.CardName == "None")
        {
            return;
        }
       
        GameObject[] combinedArray = card_Clone.Concat(deck_Clone).ToArray();

        
        for (int i = 0; i < combinedArray.Length; i++)
        {
            card_Info = combinedArray[i].transform.Find("Card_Info");
            
            if (card_Info.gameObject.activeSelf && combinedArray[i].name == Card_Name)
            {
                card_Info.gameObject.SetActive(false);
                return;
            }
            card_Info.gameObject.SetActive(false);
            
            // 카드 이름이 공백일 경우 info창을 키지않음
            if (combinedArray[i].name == Card_Name && Card_Name != "")
            {
                card_Info.gameObject.SetActive(true);
            }
        }
    }

    public int Card_Upgrade_count(int Now_Lv = -1) // 클래시로얄 일반 카드 기준 레벨업에 필요한 카드량를 반환해주는 함수
    {
        int Card_Upgrade_count = 0;

        switch (Now_Lv)
        {
            default:
                return 0;
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 4;
            case 4:
                return 10;
            case 5:
                return 20;
            case 6:
                return 50;
            case 7:
                return 100;
            case 8:
                return 200;
            case 9:
                return 400;
            case 10:
                return 800;
        }
    }
    public int Card_Upgrade_Money(int Now_Lv = -1) // 클래시로얄 일반 카드 기준 레벨업에 필요한 비용를 반환해주는 함수
    {
        int Card_Upgrade_count = 0;

        switch (Now_Lv)
        {
            default:
                return -1;
            case 1:
                return 0;
            case 2:
                return 5;
            case 3:
                return 20;
            case 4:
                return 50;
            case 5:
                return 150;
            case 6:
                return 400;
            case 7:
                return 1000;
            case 8:
                return 2000;
            case 9:
                return 4000;
            case 10:
                return 8000;
        }
    }
}