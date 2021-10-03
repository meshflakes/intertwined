using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour
{
    public GameObject anxietyIndicator;
    public GameObject GameOverText;
    
    private Text AnxietyIndicatorText;

    public GameObject anxietyCount;
    private Text AnxietyCountText;

    private int anxiety = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        GameOverText.SetActive(false);
        anxietyIndicator = GameObject.Find("AnxietyIndicator");
        AnxietyIndicatorText = anxietyIndicator.GetComponent<Text>();

        anxietyCount = GameObject.Find("AnxietyCount");
        AnxietyCountText = anxietyCount.GetComponent<Text>();

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

    public void IncrementAnxiety()
    {
        anxiety++;
        AnxietyCountText.text = "Anxiety: " + anxiety + "/25";
        if (anxiety >= 25)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        GameOverText.SetActive(true);
        Time.timeScale = 0;
    }
}
