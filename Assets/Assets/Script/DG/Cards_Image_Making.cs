using UnityEngine;

public class Cards_Image_Making : MonoBehaviour
{
    public GameObject imagePrefab; // 생성할 이미지 프리팹
    public GameObject CardsFrame;
    public Transform parentTransform; // 부모 Transform
    public int n; // 생성할 이미지 개수

    void Start()
    {
        SpawnImages();
    }

    void SpawnImages()
    {
        // 현재 객체의 가로 길이를 가져옵니다.
        float width = CardsFrame.GetComponent<Renderer>().bounds.size.x;
        Debug.Log(width); //0 ?

        // 가로축 1/4 위치 계산
        float spawnX = imagePrefab.transform.position.x;
        float spawnY = imagePrefab.transform.position.y;


        for (int i = 1; i < n + 1; i++)
        {
            // 이미지 생성
            GameObject image = Instantiate(imagePrefab, new Vector3(spawnX, spawnY, imagePrefab.transform.position.z),
                Quaternion.identity, parentTransform);

            if (i % 4 == 0)
            {
                spawnX -= width;
                spawnY -= 100f;
            }
            else
            {
                spawnX += (width / 4);
            }
        }
    }
}