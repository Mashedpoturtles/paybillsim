using UnityEngine;
using System;
using System.Collections;

public class TimeNow : MonoBehaviour {
 
    public static int currentTime;


    public static void Now()
    {
        int currentTime = (int)TimeManager.instance.Hour;
    }
}
