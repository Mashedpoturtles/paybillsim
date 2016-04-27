using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI : MonoBehaviour
{
public GameObject Player;
public static float speed = 20;
public GameObject Waypoint1;
public GameObject Waypoint2;
public GameObject Waypoint3;
public GameObject Waypoint4;
public GameObject Waypoint5;
  
     

public  static float step = speed * Time.deltaTime; 
// change behavior slightly to move to various locations

public IEnumerator EnterBuilding()
{
    Queue<GameObject> waypoints = new Queue<GameObject>();
    waypoints.Enqueue(Waypoint5);
    waypoints.Enqueue(Waypoint1);
    waypoints.Enqueue(Waypoint2);
    waypoints.Enqueue(Waypoint3);
    waypoints.Enqueue(Waypoint4);
    waypoints.Enqueue(Waypoint1);
    
    while (waypoints.Count > 0)
    {
        GameObject nextWaypoint = waypoints.Dequeue();

        while (Vector2.Distance(Player.transform.position, nextWaypoint.transform.position) > 0.1f)
        {
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, nextWaypoint.transform.position, step);
            yield return null;
        }
    }       
}
    
public IEnumerator Working()
{
    Queue<GameObject> waypoints = new Queue<GameObject>();
    waypoints.Enqueue(Waypoint1);
    waypoints.Enqueue(Waypoint2);
    waypoints.Enqueue(Waypoint3);
    while (waypoints.Count > 0)
    {
        GameObject nextWaypoint = waypoints.Dequeue();

        while (Vector2.Distance(Player.transform.position, nextWaypoint.transform.position) > 0.1f)
        {
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, nextWaypoint.transform.position, step);
            yield return null;
        }
    }
}

public IEnumerator LeaveBuilding()
{
    Queue<GameObject> waypoints = new Queue<GameObject>();

    waypoints.Enqueue(Waypoint2);
    waypoints.Enqueue(Waypoint3);
    waypoints.Enqueue(Waypoint4);
    waypoints.Enqueue(Waypoint5);

    while (waypoints.Count > 0)
    {
        GameObject nextWaypoint = waypoints.Dequeue();

        while (Vector2.Distance(Player.transform.position, nextWaypoint.transform.position) > 0.1f)
        {
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, nextWaypoint.transform.position, step);
            yield return null;
        }
    }
}
 
// decide when and how to fire movement towards locations.

public void CheckSchedule()
{
    TimeSpan StartMorning = new TimeSpan(08, 0, 0); //8 o'clock
    TimeSpan EndMorning = new TimeSpan(09, 0, 0); //16:30 o'clock
    TimeSpan Midday = new TimeSpan(09, 0, 30); //8 o'clock
    TimeSpan EndMidday = new TimeSpan(16, 50, 0); //16:30 o'clock
    TimeSpan StartEvening = new TimeSpan(16, 50, 0); //8 o'clock
    TimeSpan EndEvening = new TimeSpan(17, 30, 0); //16:30 o'clock
    TimeSpan now = DateTime.Now.TimeOfDay;

    if ((now > StartMorning) && (now < EndMorning))
    {
        Debug.Log("going to work!");
        StartCoroutine("EnterBuilding");
    }
    if ((now > Midday) && (now < EndMidday))
    {
        Debug.Log("Everyone is busy working!");
        StartCoroutine("Working");                   
    }

    if ((now > StartEvening) && (now < EndEvening))
    {
        Debug.Log("going home!");
        StartCoroutine("LeaveBuilding");
    }      

}
public void Start()
{
    //check the schedule every minute based on current time by repeating the schedule script and fire appropriate courotines
    InvokeRepeating("CheckSchedule", 0, 60);
}     
}