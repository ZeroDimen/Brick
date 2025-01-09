using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
public class Card_Upgrade_Action : MonoBehaviour, IPointerClickHandler
{
    public GameObject Card_Info_Frame;
    public GameObject Card_Info_Upgrade;
    public TMP_Text cards_Name;

    private Image imageSprite;

    private void Start()
    {
        imageSprite = Card_Info_Upgrade.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData Data) // 영역 안에서 터치 및 때기 포함
    {
        if (imageSprite.color != Color.gray)
        {
           Read_GameData.instance.Card_Lvl_Up(cards_Name.text);
           Card_Info_Frame.SetActive(false);
        }
        else
        {
            Debug.Log("비활성화");
        }
    }
}