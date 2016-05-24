using System;
using Universe;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
    {

    public delegate void DayChanged ( );
    public event DayChanged OnDayChange;
    public static DateTime currentTime;
    public bool SavedDifficultyIsEasy;
    public bool SavedDifficultyIsNormal;
    public bool SavedDifficultyIsHard;
    public float TimeSpeed;
    public bool IsPaused;

    void Update ( )
        {
        DayIsChanged ( );
        }

    void Awake ( )
        {
        if ( SceneManager.GetActiveScene ( ).buildIndex == 0 )
            {
            SceneManager.LoadScene ( 1 );
            }
        IsPaused = true;
        }

    public void SetDifficultyEasy ( )
        {
        if ( SavedDifficultyIsEasy )
            TimeSpeed = 10f;
        StartCoroutine ( AddHours ( ) );
        }

    public void SetDifficultyNormal ( )
        {
        if ( SavedDifficultyIsNormal )
            TimeSpeed = 30f;
        StartCoroutine ( AddHours ( ) );
        }

    public void SetDifficultyHard ( )
        {
        if ( SavedDifficultyIsHard )
            TimeSpeed = 50f;
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
        if ( IsPaused == true )
            {
            IsPaused = false;
            if ( SavedDifficultyIsEasy == true )
                {
                SetDifficultyEasy ( );
                }
            else if ( SavedDifficultyIsNormal == true )
                {
                SetDifficultyNormal ( );
                }
            else if ( SavedDifficultyIsHard == true )
                {
                SetDifficultyHard ( );
                }
            }

        else
            {
            Pause ( );
            }
        }

    public void Pause ( )
        {
        if ( IsPaused == false )
            {
            IsPaused = true;
            TimeSpeed = 0f;
            StartCoroutine ( AddHours ( ) );
            }

        else
            {
            UnPause ( );
            return;
            }
        }

    private DayOfWeek tmpDay = currentTime.DayOfWeek;
    private object tempInfo;
    private object buttonText;

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
