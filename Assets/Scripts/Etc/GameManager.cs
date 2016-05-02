using UnityEngine;
using System;
using System.Collections;

public class GameManager : Manager<GameManager>
    {
    public delegate void DayChanged ( );
    public event DayChanged OnDayChange;

    public bool IsEasy;
    public static DateTime currentTime;
    public bool IsNormal;

    public bool IsHard;

    public float TimeSpeed;

    private void Update ( )
        {
        DayIsChanged ( );
        }

    public void Paused ( )
        {
        TimeSpeed = 0f;
        StartCoroutine ( AddHours ( ) );
        }

    public void Easy ( )
        {
        if ( IsEasy )
            TimeSpeed = 100f;
        StartCoroutine ( AddHours ( ) );
        }

    public void Normal ( )
        {
        if ( IsNormal )
            TimeSpeed = 200f;
        StartCoroutine ( AddHours ( ) );
        }

    public void Hard ( )
        {
        if ( IsHard )
            TimeSpeed = 500f;
        StartCoroutine ( AddHours ( ) );
        }
    private IEnumerator AddHours ( )
        {
        while ( true )
            {
            currentTime = currentTime.AddHours ( 1 );
            yield return new WaitForSeconds ( 1.0f / TimeSpeed );
            }
        }

    public void UnPause ( )
        {
        if ( IsEasy == true )
            {
            Easy ( );
            }
        else if ( IsNormal == true )
            {
            Normal ( );
            }
        else if ( IsHard == true )
            {
            Hard ( );
            }
        }

    private DayOfWeek tmpDay = currentTime.DayOfWeek;
    public void DayIsChanged ( )
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
    }
