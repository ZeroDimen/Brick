using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Player_Data;

public class SwipeUI : MonoBehaviour
{
	public static SwipeUI instance; //함수 인스턴스화
	
	[SerializeField] private Scrollbar scrollBar; // Scrollbar의 위치를 바탕으로 현재 페이지 검사
	[SerializeField] private float swipeTime = 0.2f; // 페이지가 Swipe 되는 시간
	[SerializeField] private float swipeDistance = 50.0f; // 페이지가 Swipe되기 위해 움직여야 하는 최소 거리

	private float[] scrollPageValues; // 각 페이지의 위치 값 [0.0 - 1.0]

	private float valueDistance = 0; // 각 페이지 사이의 거리
	
	private int maxPage = 0; // 최대 페이지
	private float startTouchX; // 터치 시작 위치 (X)
	private float startTouchY; // 터치 시작 위치 (Y)
	private float endTouchX; // 터치 종료 위치 (X)
	private bool isSwipeMode = false; // 현재 Swipe가 되고 있는지 체크

	public int currentPage = 0; // 현재 페이지

	private GameData gameData;
	
	
	public GameObject mapPrefab; // 생성할 멥 이미지 프리팹
	public Transform parentTransform_mapPrefab; // 부모 Transform
	[SerializeField] private Sprite[] map_Sprite_List; // 생성할 멥의 이미지 (갯수)
    
	[SerializeField] private float increase_X;
	
	private GameObject[] map_Clone;

	public GameObject choice_Object;
	
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		
		// 최대 페이지의 수
		maxPage = map_Sprite_List.Length;
		
		gameData = SaveSystem.LoadPlayerData("save_1101");
		currentPage = gameData.playerData.Map;
		
		// 스크롤 되는 페이지의 각 value 값을 저장하는 배열 메모리 할당
		scrollPageValues = new float[maxPage];

		// 스크롤 되는 페이지 사이의 거리
		valueDistance = 1f / (scrollPageValues.Length - 1f);

		// 스크롤 되는 페이지의 각 value 위치 설정 [0 <= value <= 1]
		for (int i = 0; i < scrollPageValues.Length; ++i)
		{
			scrollPageValues[i] = valueDistance * i;
		}

		
	}

	private void Start()
	{
		SpawnMaps();
		// 최초 시작할 때 0번 페이지를 볼 수 있도록 설정
		SetScrollBarValue(currentPage);
	}
	
	public void SpawnMaps()
	{
		float spawnX = mapPrefab.GetComponent<RectTransform>().anchoredPosition.x;
		float spawnY = mapPrefab.GetComponent<RectTransform>().anchoredPosition.y;

		map_Clone = new GameObject[map_Sprite_List.Length];
        
		
		for (int i = 0; i < map_Sprite_List.Length; i++)
		{
			Image imageSprite = mapPrefab.GetComponent<Image>();
			if (gameData.playerData.MaxMap >= i) // 클리어한 맵번호 이상을 보여줄때 흑백처리
			{
				imageSprite.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
			}
			else
			{
				imageSprite.color = new Color(56 / 255f, 56 / 255f, 56 / 255f, 255f / 255f);
			}
			imageSprite.sprite = map_Sprite_List[i];
			
			
			// UI 생성 개수에 따른 좌표 이동
			spawnX += increase_X;

			Vector2 spawnPos = new Vector2(spawnX, spawnY);

            
			// Image UI 생성
			map_Clone[i] = Instantiate(mapPrefab, parentTransform_mapPrefab);
            
			map_Clone[i].name = i.ToString();
            
			map_Clone[i].SetActive(true);
			// 좌표 변경
			RectTransform imageRect = map_Clone[i].GetComponent<RectTransform>();
			imageRect.anchoredPosition = spawnPos;
		}
	}
	
	public void SetScrollBarValue(int index)
	{
		currentPage = index;
		scrollBar.value = scrollPageValues[index];
	}

	private void Update()
	{
		UpdateInput();
		if (gameData.playerData.MaxMap >= currentPage) // 클리어한 맵번호 이상을 보여줄때 선택 버튼 비활성화
		{
			choice_Object.SetActive(true);
		}
		else
		{
			choice_Object.SetActive(false);
		}
	}

	private void UpdateInput()
	{
		// 현재 Swipe를 진행중이면 터치 불가
		if (isSwipeMode == true) return;

#if UNITY_EDITOR
		// 마우스 왼쪽 버튼을 눌렀을 때 1회
		if (Input.GetMouseButtonDown(0))
		{
			// 터치 시작 지점 (Swipe 방향 구분)
			startTouchX = Input.mousePosition.x;
			startTouchY = Input.mousePosition.y;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			// 터치 종료 지점 (Swipe 방향 구분)
			endTouchX = Input.mousePosition.x;
			UpdateSwipe();
		}
#endif

#if UNITY_ANDROID
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				// 터치 시작 지점 (Swipe 방향 구분)
				startTouchX = touch.position.x;
				startTouchY = touch.position.y;
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				// 터치 종료 지점 (Swipe 방향 구분)
				endTouchX = touch.position.x;

				UpdateSwipe();
			}
		}
#endif
	}

	private void UpdateSwipe()
	{
		if (startTouchY < Screen.height * 11 / 28.0 || startTouchY > Screen.height * 22 / 28.0)
		{
			StartCoroutine(OnSwipeOneStep(currentPage));
			return;
		}
		
		// 너무 작은 거리를 움직였을 때는 Swipe X
		if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
		{
			// 원래 페이지로 Swipe해서 돌아간다
			StartCoroutine(OnSwipeOneStep(currentPage));
			return;
		}

		// Swipe 방향
		bool isLeft = startTouchX < endTouchX ? true : false;

		// 이동 방향이 왼쪽일 때
		if (isLeft == true)
		{
			// 현재 페이지가 왼쪽 끝이면 종료
			if (currentPage == 0) return;

			// 왼쪽으로 이동을 위해 현재 페이지를 1 감소
			currentPage--;
		}
		// 이동 방향이 오른쪽일 떄
		else
		{
			// 현재 페이지가 오른쪽 끝이면 종료
			if (currentPage == maxPage - 1) return;

			// 오른쪽으로 이동을 위해 현재 페이지를 1 증가
			currentPage++;
		}

		// currentIndex번째 페이지로 Swipe해서 이동
		StartCoroutine(OnSwipeOneStep(currentPage));
	}

	/// <summary>
	/// 페이지를 한 장 옆으로 넘기는 Swipe 효과 재생
	/// </summary>
	private IEnumerator OnSwipeOneStep(int index)
	{
		float start = scrollBar.value;
		float current = 0;
		float percent = 0;

		isSwipeMode = true;

		while (percent < 1)
		{
			current += Time.deltaTime;
			percent = current / swipeTime;

			scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);

			yield return null;
		}

		isSwipeMode = false;
	}
	
	public void ChangePageButton(int page)
	{
		// 현재 Swipe를 진행중이면 터치 불가
		if (isSwipeMode == true) return;
		
		if (currentPage + page > maxPage - 1) currentPage = maxPage - 1;
		else if (currentPage + page < 0) currentPage = 0;
		else currentPage += page;
		// currentIndex번째 페이지로 Swipe해서 이동
		StartCoroutine(OnSwipeOneStep(currentPage));

	}

}
