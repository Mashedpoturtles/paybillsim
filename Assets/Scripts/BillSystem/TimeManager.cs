using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
    {

    public static TimeManager instance;
    public static DateTime currentTime;
    public delegate void DayChanged ( );
    public static event DayChanged OnDayChange;

    private Text timeToTextUI;
    [SerializeField]
    private Text timeSpeedDisplay;


    private DayOfWeek tmpDay = currentTime.DayOfWeek;
    void OnEnable ( )
        {
        currentTime = new DateTime ( 2016, 1, 1 );
        StartCoroutine ( AddHours ( ) );
        instance = this;
        timeToTextUI = GameObject.FindWithTag ( "Time" ).GetComponent<Text> ( );
        }

    void Update ( )
        {
        Timer ( );
        DayIsChanged ( );
        }

    public void Timer ( )
        {
        timeToTextUI.text = timeToTextUI.text = currentTime.Day + currentTime.ToString ( " MMMM , yyyy " );
        timeSpeedDisplay.text = string.Format ( "Tijd snelheid x {0} ", Difficulty.TimeSpeed.ToString ( ) );
        }

    void DayIsChanged ( )
        {
        if ( OnDayChange != null )
            {
            if ( tmpDay != currentTime.DayOfWeek )
                {
                OnDayChange ( );
                tmpDay = currentTime.DayOfWeek;
                }
            }
        }

    private IEnumerator AddHours ( )
        {
        while ( true )
            {
            currentTime = currentTime.AddHours ( 1 );
            yield return new WaitForSeconds ( 1.0f / Difficulty.TimeSpeed );
            }
        }
    }
