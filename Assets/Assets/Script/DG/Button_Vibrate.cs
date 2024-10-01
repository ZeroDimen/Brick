using UnityEngine;
using TMPro;
using RDG;

public class Button_Vibrate : MonoBehaviour
{
    public TMP_Text ButtonStatus;
    private bool V_Toggle = true;
    
    public void OnClick1()
    {
        if (V_Toggle)
        {
            Vibration.Vibrate((long)3000);
            ButtonStatus.text = "Vibration.Vibrate(3000)";
        }
    }
    public void OnClick2()
    {
        if (V_Toggle)
        {
            Vibration.Vibrate((long)1100);
            ButtonStatus.text = "Vibration.Vibrate(1100)";
        }
    }
    
    public void OnClick3()
    {
        if (V_Toggle)
        {
            Vibration.Vibrate((long)1050);
            ButtonStatus.text = "Vibration.Vibrate(1050)";
        }
    }
    public void OnClick4()
    {
        if (V_Toggle)
        {
            Vibration.Vibrate((long)1005);
            ButtonStatus.text = "Vibration.Vibrate(1005)";
        }
    }

    public void Toggle_Vibrate_Volume()
    {
        V_Toggle = V_Toggle == true ? false : true;
        Debug.Log(V_Toggle);
    }
}
