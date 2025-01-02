using UnityEngine;
using UnityEngine.UI;
using static Player_Data;

public class SelectMap : MonoBehaviour
{
    
    public static SelectMap instance; //함수 인스턴스화
    public GameObject mapPrefab; // 생성할 멥 이미지 프리팹
    public Transform parentTransform_mapPrefab; // 부모 Transform
    [SerializeField] private Sprite[] map_Sprite_List; // 생성할 멥의 이미지 (갯수)
    private GameObject map_Clone;
    private GameData gameData;

    public int map_number;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        gameData = SaveSystem.LoadPlayerData("save_1101");
        map_number = gameData.playerData.Map;
        SpawnMaps(); // 처음 보여줄 Map
    }

    public void SpawnMaps() // map_number에 해당하는 맵의 이미지를 출력하는 함수
    {
        Destroy(map_Clone);
        
        for (int i = 0; i < map_Sprite_List.Length; i++)
        {
            Image imageSprite = mapPrefab.GetComponent<Image>();
            imageSprite.sprite = map_Sprite_List[i];

            if (map_number == i) // 원하는 맵 하나만 생성
            {
                map_Clone = Instantiate(mapPrefab, parentTransform_mapPrefab);
                map_Clone.name = i.ToString();
                map_Clone.SetActive(true);
            }
            
        }
    }
    
    public void changeMap(int currentMap) // 맵을 바꾸었을때 이미지 출력을 다시하는 함수
    {
        map_number = currentMap;
        SpawnMaps();
    }
}
