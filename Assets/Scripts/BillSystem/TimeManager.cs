using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// Display the time to UI.
    /// </summary>
    private Text timeToTextUI;
    public static TimeManager instance;
    /// <summary>
    /// Custom datetime.
    /// </summary>
    public static DateTime currentTime;
    public static DateTime duedateelect;
    public delegate void DayChanged();
    /// <summary>
    /// This event checks of a day has changed from previous to the next.
    /// </summary>
    public static event DayChanged OnDayChange;

    void Start()
    {
        currentTime = new DateTime(2016, 1, 1);
        StartCoroutine(AddHours());
        instance = this;
        duedateelect = new DateTime(2016, 1, 1);
        timeToTextUI = GameObject.FindWithTag("Time").GetComponent<Text>();
    }

    void Update()
    {
        Timer();
        DayIsChanged();
    }

    /// <summary>
    ///  Hours are seconds and time passes in 24 hour cycles.
    /// </summary>
    public void Timer()
    {
        timeToTextUI.text = timeToTextUI.text = currentTime.DayOfWeek.ToString() + currentTime.ToString(" MMMM , yyyy ") + "Current Time: " + currentTime.ToString("HH: tt") + ".";
    }
    /// <summary>
    /// Store the current time temporarily to this string to compare if a day is changed in the OndayChange method.
    /// </summary>
    private string temporaryDay = "";
    /// <summary>
    /// Fires the OnDayChange event.
    /// </summary>
    void DayIsChanged()
    {
        if (OnDayChange != null) // Check that there are subscribers to the OnDayChange event.
        {
            if (temporaryDay == "")
            {
                temporaryDay = currentTime.Day.ToString();
            }
            if (temporaryDay != currentTime.Day.ToString())
            {
                OnDayChange();
                temporaryDay = currentTime.Day.ToString();
            }
        }
    }

    [SerializeField]
    float speedUp = 1.0f;
    /// <summary>
    /// Speeds up time in the inspector mainly for testing purposes.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AddHours()
    {
        while (true)
        {
            currentTime = currentTime.AddHours(1);
            yield return new WaitForSeconds(1.0f / speedUp);
        }
    }
}
