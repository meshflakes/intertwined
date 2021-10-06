using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxAnxiety(int max)
    {
        slider.maxValue = max;
    }
    public void SetAnxiety(int anxietyAmount)
    {
        slider.value = anxietyAmount;
    }
}
