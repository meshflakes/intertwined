using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour
{
    public GameObject anxietyIndicator;
    
    private Text AnxietyIndicatorText;
    
 
    // Start is called before the first frame update
    void Start()
    {
        anxietyIndicator = GameObject.Find("AnxietyIndicator");
        AnxietyIndicatorText = anxietyIndicator.GetComponent<Text>();
  
    }
    


    public void UpdatePlayerStatus(bool tooFar)
    {
        if (tooFar)
        {
            AnxietyIndicatorText.text = "Status: Anxious";
        }
        else
        {
            AnxietyIndicatorText.text = "Status: Calm";
        }
    }
}
