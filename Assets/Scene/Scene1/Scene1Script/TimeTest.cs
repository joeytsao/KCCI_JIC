﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeTest : MonoBehaviour
{
    public SkeletonInformation Tskeletoninfomation;

    public GameObject StartPic, TimeupPic, WordCompletePic;

    public bool intheGame;
    public GameObject NoBodyDetect;
    public float ReLoadGame;

    public float f_GameTime, AllWordTime;
    public GUIStyle StartCount, TimeCounter, EndGuiSC;

    public int SWdown, SWup, SHdown, SHup, WordW, WordH;
    
    public float TSWdown, TSWup, TSHdown, TSHup, TWordW, TWordH;

    public int CorrectSWdown, CorrectSWup, CorrectSHdown, CorrectSHup, CorrectWordW, CorrectWordH;


    public GameObject G_WordCreat;

    public bool AllWord = false;

    public AudioSource CountDownAudio, StartAudio, TimeUpAudio, BGAudio;
    int AudioCount = 0;
    
    //  private List<int> saveList = new List<int>();

    // Use this for initialization
    void Start()
    {
        f_GameTime = 96;
        intheGame = false;
    }

    void OnGUI()
    {
        if (AllWord == true)
        {
            if (AllWordTime > 3)
            {
                GUI.Label(new Rect(Screen.width * TSWup / TSWdown, Screen.height * TSHup / TSHdown, TWordW, TWordH), Mathf.Floor(f_GameTime).ToString("00"), TimeCounter);
                TimeCounter.fontSize = (int)(Screen.height * TWordH / TWordW);
            }
            else
            {
                GUI.Label(new Rect(Screen.width * TSWup / TSWdown, Screen.height * TSHup / TSHdown, TWordW, TWordH), Mathf.Floor(f_GameTime).ToString("00"), TimeCounter);
                TimeCounter.fontSize = (int)(Screen.height * TWordH / TWordW);
            }
        }
        else
        {
            if (f_GameTime <= -3)
            {
                GUI.Label(new Rect(Screen.width * TSWup / TSWdown, Screen.height * TSHup / TSHdown, TWordW, TWordH), "00", TimeCounter);
                TimeCounter.fontSize = (int)(Screen.height * TWordH / TWordW);

                TimeupPic.SetActive(false);
                WordCompletePic.SetActive(true);

                GUI.Label(new Rect(Screen.width * CorrectSWup / CorrectSWdown,
                    Screen.height * CorrectSHup / CorrectSHdown, CorrectWordW, CorrectWordH),  G_WordCreat.GetComponent<WordTest>().CorrectCount.ToString() , EndGuiSC);
                EndGuiSC.fontSize = Screen.height * CorrectWordH / CorrectWordW;

              
            }
            else if (f_GameTime <= 0)
            {
                GUI.Label(new Rect(Screen.width * TSWup / TSWdown, Screen.height * TSHup / TSHdown, TWordW, TWordH), "00", TimeCounter);
                TimeupPic.SetActive(true);
                if (AudioCount == 5)
                {
                    TimeUpAudio.Play();
                    AudioCount++;
                }
            }
            else if (f_GameTime <= 90)
            {
                StartPic.SetActive(false);
                GUI.Label(new Rect(Screen.width * TSWup / TSWdown, Screen.height * TSHup / TSHdown, TWordW, TWordH), Mathf.Floor(f_GameTime).ToString("00"), TimeCounter);
                TimeCounter.fontSize = (int)(Screen.height * TWordH / TWordW);
                if (AudioCount == 4)
                {
                    BGAudio.Play();
                    AudioCount++;
                }
                G_WordCreat.SetActive(true);
            }
            else if (f_GameTime <= 91)
            {
                StartPic.SetActive(true);
            
                if ( AudioCount == 3)
                {
                    StartAudio.Play();
                    AudioCount++;
                }
            }
            else if (f_GameTime <= 94)
            {
                GUI.Label(new Rect(Screen.width * SWup / SWdown, Screen.height * SHup / SHdown, WordW, WordH), Mathf.Floor((f_GameTime - 90)).ToString("0"), StartCount);
                StartCount.fontSize = Screen.height * WordH / WordW;
                if (f_GameTime < 94 && AudioCount == 0)
                {
                    CountDownAudio.Play();
                    AudioCount++;
                }
                else if (f_GameTime < 93 && AudioCount == 1)
                {
                    CountDownAudio.Play();
                    AudioCount++;
                }
                else if (f_GameTime < 92 && AudioCount == 2)
                {
                    CountDownAudio.Play();
                    AudioCount++;
                }
                
            }
        }



    }


    // Update is called once per frame
    void Update()
    {
   //     if (intheGame == false)
    //    {
   //     }
   //     else
   //     {
        if (KinectDetectOutput.SkeletonIsEnable == true)
        {
        
            NoBodyDetect.SetActive(false);
            intheGame = true;
            ReLoadGame = 0;
            if (AllWord != true)
            {

                f_GameTime -= Time.deltaTime;
            }
            else
            {

                AllWordTime += Time.deltaTime;
            }
            if (f_GameTime <= -13)
                Application.LoadLevel("Scene1");
        }
        else if (KinectDetectOutput.SkeletonIsEnable == false && intheGame == true)
        {
            NoBodyDetect.SetActive(true);
            ReLoadGame += Time.deltaTime;
            if (ReLoadGame >= 10)
                Application.LoadLevel("Scene1");
        }
        else if (KinectDetectOutput.SkeletonIsEnable == false)
            NoBodyDetect.SetActive(true);
        }
  //  }
}
