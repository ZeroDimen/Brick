using UnityEngine;
using UnityEngine.UI;

public class Sound_Test : MonoBehaviour
{
    public AudioSource BGM_Source;
    public Slider BGM_Slider;
    private bool isBGMPlaying = true;   // 사운드 ON/OFF 값을 저장하기 위한 함수
    private float BGM_Volume;   // 현제 볼륨을 저장하기 위한 변수

    public void Set_BGM_Volume(float volume) // 슬라이드바를 사용한 조정
    {
        BGM_Source.volume = volume;
    }

    public void Toggle_BGM_Volume() // 토클버튼을 사용한 조정
    {
        isBGMPlaying = isBGMPlaying == true ? false : true;
        BGM_Slider.interactable = isBGMPlaying; // isBGMPlaying 값에 따라 슬라이더 활성화 여부
        if (!isBGMPlaying)
        {
            BGM_Volume = BGM_Source.volume; 
            BGM_Source.volume = 0;
        }
        else
        {
            BGM_Source.volume = BGM_Volume;
        }
    }
}
