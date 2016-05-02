using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
    {
    private Text timeToTextUI;
    [SerializeField]
    private Text timeSpeedDisplay;

    void OnEnable ( )
        {
        GameManager.currentTime = new DateTime ( 2016, 1, 1 );
        timeToTextUI = GameObject.FindWithTag ( "Time" ).GetComponent<Text> ( );
        }

    void Update ( )
        {
        Timer ( );
        }

    public void Timer ( )
        {
        timeToTextUI.text = timeToTextUI.text = GameManager.currentTime.Day + GameManager.currentTime.ToString ( " MMMM , yyyy " );
        timeSpeedDisplay.text = string.Format ( "Tijd snelheid x {0} ", GameManager.Instance.TimeSpeed.ToString ( ) );
        }
    }
