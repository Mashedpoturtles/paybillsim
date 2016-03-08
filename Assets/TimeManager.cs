using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public Text timeToTextUI;
    public static TimeManager instance;
    public DateTime currentTime;   
    //Timer variables
    private float hour;
    private DateTime day;
    private DateTime month;
    private DateTime year;  
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

    public DateTime Month
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
    public DateTime Day
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
    public  DateTime Year
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
        DateTime time = new DateTime(year, month, day, (int)hour, 0, 0);
        Debug.Log(time);
        hour += Time.deltaTime;
        timeToTextUI.text = timeToTextUI.text = string.Format(" Hours : {0}  Days : {1}    Month : {2}    Year: {3}", hour.ToString("0"), day, month, year);
      
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
    /*
    IEnumerator AddSecond()
    {
        while (true)
        {
            someDateTime = someDateTime.Add(TimeSpan.FromSeconds(1));
            yield return new WaitForSeconds(1);
        }
    }
    */
}
