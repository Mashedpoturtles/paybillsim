using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public Text timeToTextUI;
    public static TimeManager instance;
    public TimeSpan current = new TimeSpan();
    //Timer variables
    private float hour;
    private int day;
    private int month;
    private int year;
    private int currentTime;
    public bool _speedUpTimeTimes2 = false;
    public bool _speedUpTimeTimes4 = false;
    public bool _speedUpTimeTimes8 = false;
  
    public  float Hour
    {
        get
        {
            return hour;
        }
        set
        {
            hour = value;
        }
    }

    public int Month
    {
        get
        {
            return month;
        }
        set
        {
            month = value;
        }
    }
    public int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
        }
    }
    public  int Year
    {
        get
        {
            return year;
        }
        set
        {
            year = value;
        }
    }
    
    //Hours are seconds and time passes in 24 hour cycles.
    public void Timer()
    {
        
        hour += Time.deltaTime;
        timeToTextUI.text = timeToTextUI.text = string.Format(" Hours : {0}  Days : {1}    Month : {2}    Year: {3}", hour.ToString("0"), day, month, year);
        //current.Add((new TimeSpan(year, month, day, hour)));
        Debug.Log(current);
        if (hour >= 24)
        {
            day += 1;
            hour = 0;  
        }
       if(day >= 30)
        {
            month += 1;
            day = 0;
        }
       if(month >= 12)
        {
            year += 1;
            month = 0;
        }
        if (_speedUpTimeTimes2 == true)
        {
            hour += Time.deltaTime * 2;
        }
        if(_speedUpTimeTimes4 == true)
        {
            hour += Time.deltaTime * 4;
        }
        if (_speedUpTimeTimes8 == true)
        {
            hour += Time.deltaTime * 8;
        }
    }
	void Start ()
    {
        instance = this; 
        year = 2016;
        month = 1;
        day = 1;
        hour = 0;
	}

	void Update ()
    {
        Timer();
	}
}
