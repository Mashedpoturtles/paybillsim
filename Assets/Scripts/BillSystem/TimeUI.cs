using System;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Threading;

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
        // Creates a CultureInfo for German in the Netherlands
        CultureInfo ci = new CultureInfo ( "nl-NL" );
        // Sets the CurrentCulture property to dutch
        Thread.CurrentThread.CurrentCulture = new CultureInfo ( "nl-NL" );

        timeToTextUI.text = GameManager.currentTime.Day + GameManager.currentTime.ToString ( " MMMM , yyyy ", ci );
        timeSpeedDisplay.text = string.Format ( "Tijd snelheid x {0} ", GameManager.Instance.TimeSpeed.ToString ( ) );
        }
    }
