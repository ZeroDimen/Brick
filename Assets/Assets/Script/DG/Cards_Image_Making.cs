using UnityEngine;
using UnityEngine.UI;
using static Player_Data;
using System.Collections.Generic; // List 사용을 위한 네임스페이스 추가
using TMPro;

public class Cards_Image_Making : MonoBehaviour
{
    public static Cards_Image_Making instance; //함수 인스턴스화

    public GameObject cardPrefab; // 생성할 이미지 프리팹
    public Transform parentTransform; // 부모 Transform
    private GameData gameData;

    [SerializeField] private Sprite[] cards_Sprite_List; // 생성할 이미지 개수
    public GameObject cards_Sprite;
    public TMP_Text cards_Cost;
    public TMP_Text cards_Level;
    [SerializeField] private float increase_X;

    [SerializeField] private float increase_Y;


    private List<GameObject> spawnedImages = new List<GameObject>(); // 생성된 이미지 리스트

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnImages(GameData gameData)
    {
        RemoveAllImages();
        
        
        ScrollView_ContentResizer.instance.ResizeContent(gameData.cardDataList.Cards.Count);

        float spawnX = cardPrefab.GetComponent<RectTransform>().anchoredPosition.x;
        float spawnY = cardPrefab.GetComponent<RectTransform>().anchoredPosition.y;


        for (int i = 0; i < gameData.cardDataList.Cards.Count; i++)
        {

            Image imageSprite = cards_Sprite.GetComponent<Image>();
            if (!gameData.cardDataList.Cards[i].CardIsHave)
            {
                imageSprite.color = Color.HSVToRGB(0f, 0f, 50 / 255f);
                imageSprite.color = new Color(154/255f, 154/255f, 154/255f, 255f / 255f);
            }
            else
            {
                imageSprite.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            }
            // if (cards_Sprite_List[i] != null)
            if (i < 7)
            {
                // UI Sprite 변경
                imageSprite.sprite = cards_Sprite_List[i];
            }
            else
            {
                imageSprite.sprite = cards_Sprite_List[7];
            }

            cards_Level.text = gameData.cardDataList.Cards[i].CardLevel.ToString();
            cards_Cost.text = gameData.cardDataList.Cards[i].CardCost.ToString();

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
            GameObject image = Instantiate(cardPrefab, spawnPos, Quaternion.identity, parentTransform);
            image.SetActive(true);
            // 좌표 변경
            RectTransform imageRect = image.GetComponent<RectTransform>();
            imageRect.anchoredPosition = spawnPos;
            spawnedImages.Add(image); // 생성된 이미지 리스트에 추가
        }
    }

    public void RemoveAllImages()
    {
        foreach (GameObject image in spawnedImages)
        {
            Destroy(image); // 게임 오브젝트 제거
        }

        spawnedImages.Clear(); // 리스트 초기화
    }
}