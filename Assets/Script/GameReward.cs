using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameReward : MonoBehaviour
{
    public Button[] datePos;
    public int dayRamain = 1;
    public Image progessDay;
    
    private DateTime d1;
    private DateTime d2;
    private TimeSpan timeSpan;
    private float occurances;
    private bool isTimeCreate;
    
    // Start is called before the first frame update
    void Start()
    {
        timeSpan = new TimeSpan(24, 00, 00);
        
        d1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        d2 = d1.AddDays(1);
        
        
        if (occurances >= 1 && isTimeCreate)
        {
            dayRamain++;
            Debug.Log("One day Complete");
        }

        for (int i = 0; i < dayRamain; i++)
        {
            datePos[i].interactable = true;
        }
        
        Debug.Log("d1 = " + d1);
        Debug.Log("d2 = " + d2);
    }

    // Update is called once per frame
    void OnEnable()
    {
            d1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            occurances = ((d2 - d1).Ticks / (float)timeSpan.Ticks);
            progessDay.fillAmount = occurances;
            Debug.Log(occurances);
    }

    public void rewardButton()
    {
        isTimeCreate = true;
        Debug.Log(d1);
    }
}
