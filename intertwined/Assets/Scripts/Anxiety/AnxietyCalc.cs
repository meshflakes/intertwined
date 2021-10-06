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
    private static int UPDATE_FRAMES = 140;
    private static int PANIC_UPDATE_FRAMES = 70;
    private int frameUpdate = UPDATE_FRAMES;

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
        if (frames >= frameUpdate)
        {
            UpdateAnxiety();
            frames = 0;
        }
        frames++;

    }

    //Every UPDATE_FRAMES amount of frames, this function will be called to update anxiety stat
    void UpdateAnxiety()
    {
        //Increase anxiety by a point
        if(anxiety+1 <=MAX_ANXIETY) anxiety++;
        if (distance >= MAX_DISTANCE)
        {
            frameUpdate = PANIC_UPDATE_FRAMES;
            more_anxious = true;
        }
        else
        {
            frameUpdate = UPDATE_FRAMES;
            more_anxious = false;
        }
        
        Debug.Log(anxiety);
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
    void LowerAnxiety()
    {
        if (anxiety >= LOWER_BOUND)
        {
            anxiety = LOWER_BOUND;
            UpdateMusic();
            AnxietyBar.SetAnxiety(anxiety);
            //Want the frames before the anxiety update to be two cycles
            frames = -frameUpdate;
        }
        
    }
}
