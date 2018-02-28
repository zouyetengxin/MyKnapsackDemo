using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPValue : MonoBehaviour
{
    private Text text1;
    private Slider HPSlider;

    public  float HP = 100;
    private float CurrentHP = 100;
    
    public void SetTextValue(float damage)
    {
        HPSlider = GameObject.Find("HPBG").GetComponent<Slider>();
        text1 = gameObject.GetComponent<Text>();
        CurrentHP -= damage;
        HPSlider.value = CurrentHP / HP;
        text1.text = CurrentHP + "/" + HP;
    }
}
