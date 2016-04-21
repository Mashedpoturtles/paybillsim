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

    [SerializeField]
    private float speedUp = 1.0f;
    private Text timeToTextUI;
    private DayOfWeek tmpDay = currentTime.DayOfWeek;
    void Start ( )
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
            yield return new WaitForSeconds ( 1.0f / speedUp );
            }
        }
    }
