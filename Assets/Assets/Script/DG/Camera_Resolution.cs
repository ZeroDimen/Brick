using UnityEngine;

public class Camera_Resolution : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        // (현제 디스플레이 가로 / 세로) /  (고정하고 싶은 가로 / 세로)
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)  9 / 16); 
        float scalewidth = 1f / scaleheight;
        
        
        if (scaleheight < 1)    //위 아래 공간이 남는 경우
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else                    //좌우 공간이 남는 경우
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }
}
