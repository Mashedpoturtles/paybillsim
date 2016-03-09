using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public Text timeToTextUI;
    public static TimeManager instance;
    public static DateTime currentTime;
    //TODO create better way of setting more dynamic duedates
    public static DateTime duedateelect;
    public delegate void DayChanged();
    public static event DayChanged OnDayChange;
 
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
        timeToTextUI.text = timeToTextUI.text = currentTime.DayOfWeek.ToString() + currentTime.ToString(" MMMM , yyyy ") + "Current Time: " +currentTime.ToString("HH: tt") + ".";
    }

    void DayIsChanged()
    {
        if(currentTime.Hour >= 23)
        {
            OnDayChange();
        }
    }
  
	[SerializeField]
    float speedUp = 1.0f;

	private IEnumerator AddHours()
    {
        while (true)
        {
            currentTime = currentTime.AddHours(1);
            yield return new WaitForSeconds(1.0f / speedUp);
        }
    }
}
