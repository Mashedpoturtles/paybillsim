using UnityEngine;
using System;
using System.Collections;

public class LightCycle : MonoBehaviour
    {
    public Light sun;
    public float secondsInFullDay = 86400f;
    [Range ( 0, 1 )]
    public float currentTimeOfDay;
    public float timeMultiplier = 1f;
    float sunInitialIntensity;
    float intensityMultiplier = 1;
    public static LightCycle lightCycle;

    void lowerIntensityInPercentageMorning ( )
        {
        intensityMultiplier = Mathf.Clamp01 ( ( currentTimeOfDay - 0.25f ) * ( 1 / 0.02f ) );
        }
    void lowerIntensityInPercentageEvening ( )
        {
        intensityMultiplier = Mathf.Clamp01 ( 1 - ( ( currentTimeOfDay - 0.68f ) * ( 1 / 0.02f ) ) );
        }

    void Start ( )
        {
        sunInitialIntensity = sun.intensity;
        // 24th hour of the day = 1.      00:01 AM starts back at 0,00000.  
        if ( currentTimeOfDay >= 1 )
            {
            currentTimeOfDay = 0;
            }
        }

    public void Update ( )
        {
        currentTimeOfDay = ( float ) ( DateTime.Now.TimeOfDay.TotalSeconds / secondsInFullDay );
        UpdateSun ( );
        // Debug.Log(currentTimeOfDay);       
        }

    public void UpdateSun ( )
        {
        if ( currentTimeOfDay <= 0.33 || currentTimeOfDay >= 0.70f )
            {
            intensityMultiplier = 0;
            }
        else if ( currentTimeOfDay <= 0.35f )
            {
            lowerIntensityInPercentageMorning ( );
            }
        else if ( currentTimeOfDay >= 0.68f ) // Time in percentage x 86400 / 3600 = real time for sun to go down.  
            {
            lowerIntensityInPercentageEvening ( );
            }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
        }
    }
