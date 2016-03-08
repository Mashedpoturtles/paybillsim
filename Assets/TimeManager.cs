using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public Text timeToTextUI;
    public static TimeManager instance;
    public static DateTime currentTime;
    public static DateTime duedateelect;
    //Timer variables
    public bool _speedUpTimeTimes2 = false;
    public bool _speedUpTimeTimes4 = false;
    public bool _speedUpTimeTimes8 = false;

    void Start()
    {
        currentTime = new DateTime(2016, 1, 1);
        StartCoroutine(AddHours());
        instance = this;
        duedateelect = new DateTime(2016, 1, 1);
    }

    void Update()
    {
        Timer();
    }

    //Hours are seconds and time passes in 24 hour cycles.
    public void Timer()
    { 
        currentTime = currentTime.AddHours(1 * Time.deltaTime);
        timeToTextUI.text = timeToTextUI.text = currentTime.DayOfWeek.ToString() + currentTime.ToString(" MMMM , yyyy ") + "Current Time: " +currentTime.ToString("HH: tt") + ".";
    }

    IEnumerator AddHours()
    {
        while (true)
        {
            currentTime = currentTime.AddHours(1 * Time.deltaTime);
            if(_speedUpTimeTimes2 == true)
            {
                currentTime = currentTime.AddHours(2);
            }
            if(_speedUpTimeTimes4 == true)
            {
                currentTime = currentTime.AddHours(4);
            }
            if(_speedUpTimeTimes8 == true)
            {
                currentTime = currentTime.AddHours(8);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
