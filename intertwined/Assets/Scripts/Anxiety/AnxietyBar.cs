using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxAnxiety(float max)
    {
        slider.maxValue = max;
    }
    public void SetAnxiety(float anxietyAmount)
    {
        slider.value = anxietyAmount;
    }
}
