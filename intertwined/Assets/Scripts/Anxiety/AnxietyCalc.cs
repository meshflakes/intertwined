using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyCalc : MonoBehaviour
{
    // The distancce between p1 and p2
    private float distance;
    // The maximum disatance apart between p1 and p2 before they get more anxious
    private static double MAX_DISTANCE = 6.0;
    //Want the anxiety calculations to occur every 250 update calls
    private int frames = 0;
    private static int UPDATE_TIME = 3;
    private static int PANIC_UPDATE_TIME = 1;
    private int updateInterval = UPDATE_TIME;
    private int nextUpdate = 0;

    //Indicates whether the players are too far apart and making them more anxious
    private bool more_anxious = false;
    
    //The amount of anxiety in total
    public int anxiety = 0;
    public static int MAX_ANXIETY = 100;
    //The lower bound to how much petting can reduce anxiety by
    public static int LOWER_BOUND = 60;
    
    public Transform p1;
    public Transform p2;
    
    //This is for updating the anxiety bar
    public AnxietyBar AnxietyBar;
    private Text tempAnxietyText;
    
    //Music player
    public MusicPlayer MusicPlayer;
    //Anxiety levels (for music and visuals)
    private static int ANXIETY_LEVEL_ONE = 20;
    private static int ANXIETY_LEVEL_TWO = 40;
    private static int ANXIETY_LEVEL_THREE = 60;
    private static int ANXIETY_LEVEL_FOUR = 80;
    private static int ANXIETY_LEVEL_FIVE = 100;

    private static double LIMIT_DIST = 2.0;
    
    // Start is called before the first frame update
    void Start()
    {
        distance  = Vector3.Distance(p1.position, p2.position);
        
        tempAnxietyText =GameObject.Find("TempText").GetComponent<Text>();
        AnxietyBar.SetMaxAnxiety(MAX_ANXIETY);

    }

    // Update is called once per frame
    void Update()
    {
        distance  = Vector3.Distance(p1.position, p2.position);
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + updateInterval;
            UpdateAnxiety();
        }

    }

    //Every UPDATE_FRAMES amount of frames, this function will be called to update anxiety stat
    void UpdateAnxiety()
    {
        //Increase anxiety by a point
        if(anxiety+1 <=MAX_ANXIETY) anxiety++;
        if (distance >= MAX_DISTANCE)
        {
            updateInterval = PANIC_UPDATE_TIME;
            more_anxious = true;
        }
        else
        {
            updateInterval = UPDATE_TIME;
            more_anxious = false;
        }
        
        UpdateMusic();
        
        
        if (anxiety == MAX_ANXIETY)
        {
            //TODO: Trigger panic mode
        }
        //Displaying anxiety information
        AnxietyBar.SetAnxiety(anxiety);
        tempAnxietyText.text = "Anx:" + anxiety + "  More anxious:" + more_anxious;
    }

    private void UpdateMusic()
    {
        //Updates the music based on anxiety
        if (anxiety == ANXIETY_LEVEL_ONE)
        {
            MusicPlayer.playLevelOne();

        }
        else if (anxiety == ANXIETY_LEVEL_TWO)
        {
            MusicPlayer.playLevelTwo();
        }
        else if (anxiety == ANXIETY_LEVEL_THREE)
        {
            MusicPlayer.playLevelThree();
        }
        else if (anxiety == ANXIETY_LEVEL_FOUR)
        {
            MusicPlayer.playLevelFour();
        }
        else if (anxiety == ANXIETY_LEVEL_FIVE)
        {
            MusicPlayer.playLevelFive();
        }
    }
    
    //Lower anxiety when pet
    public void LowerAnxiety()
    {
        if (anxiety >= LOWER_BOUND && distance < LIMIT_DIST)
        {
            AnxietyBar.SetAnxiety(LOWER_BOUND);
            tempAnxietyText.text = "Anx:" + anxiety + "  More anxious:" + more_anxious;
            anxiety = LOWER_BOUND;
            UpdateMusic();
            
        }
        
    }

    public double GetDistance()
    {
        Debug.Log(distance);
        return distance;
    }
}
