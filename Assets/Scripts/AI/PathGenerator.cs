using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class PathGenerator : MonoBehaviour
{
// Array's to create a new waypoint based path.
public Transform[] Route1;
public Transform[] Route2;
public StateConditions stateConditions;
// Caching
private Seeker seeker;
private Rigidbody2D rb2d;
Animator animator;

// The calculated path.
public Path path;

// The AI's speed per second.
public float speed = 300f;

public ForceMode2D fmode;

// Waypoint we are currently moving towards.
public int currentWaypoint = 0;
// The distance between the transform position this script is on and the target waypoint transform position.
public float dist;
       
    
IEnumerator loadcomponents()
{
    seeker = GetComponent<Seeker>();
    rb2d = GetComponent<Rigidbody2D>();
    yield break;
}
    
void Start()
{
       
    Physics2D.IgnoreLayerCollision(11, 11, true);
    animator = GetComponent<Animator>();
    //  Initialize to current waypoint
    StartCoroutine(loadcomponents());

    stateConditions.InvokeRepeating("HungerRegen", 0, 1);
    stateConditions.InvokeRepeating("ThirstRegen", 0, 1);
    StartCoroutine(GeneratePath());
}

  
//  Check the current position of the player, and looks for the position of the next waypoint.
IEnumerator GeneratePath()
{
    seeker.StartPath(transform.position, Route1[currentWaypoint].position);
    yield break;
}
IEnumerator GeneratePath2()
{
    seeker.StartPath(transform.position, Route2[currentWaypoint].position);
    yield break;
}
   
//  Moving the routes needs to be in the FixedUpdate in order to function properly.
void FixedUpdate()
{       
    if (stateConditions._hungry == true)
    {
        stateConditions._thirsty = false;           
        StartCoroutine(MoveRoute1());
        //TODO create a function to pay money when "Ordering food"           
    }       
    if(stateConditions._thirsty == true)
    {
        stateConditions._hungry = false;
        StartCoroutine(MoveRoute2());
        //TODO create a function to pay money when "Ordering whatever else"    
    }

    animator.SetFloat("speed", rb2d.velocity.magnitude);
}         

    //  Below this point is where new routes are added currently.
    //  TODO: neat up route codes to be more compact.
//  Check Distances and move towards waypoint
public IEnumerator MoveRoute1()
{
    if (currentWaypoint > Route1.Length - 1)
        yield break;      
    dist = Vector3.Distance(transform.position, Route1[currentWaypoint].position);
    Vector3 dir = (Route1[currentWaypoint].position - transform.position).normalized;
    dir *= speed * Time.fixedDeltaTime;
    rb2d.AddForce(dir, fmode);

        // If the integer "currentWaypoint" is greater than the length of array minus 1, 
    // We are on a valid integer and therefore can move towards the next generated path.
    if (dist <= 3f)
    {          
        currentWaypoint++;
        if (currentWaypoint < Route1.Length - 1)
            StartCoroutine(GeneratePath());
        if (currentWaypoint == Route1.Length)
        {
            currentWaypoint = 0;
            stateConditions._hungry = false;
        }
    }
}
public IEnumerator MoveRoute2()
{
    if (currentWaypoint > Route2.Length - 1)
        yield break;
      
    dist = Vector3.Distance(transform.position, Route2[currentWaypoint].position);
    Vector3 dir = (Route2[currentWaypoint].position - transform.position).normalized;
    dir *= speed * Time.fixedDeltaTime;
    rb2d.AddForce(dir, fmode);

        // If the integer "currentWaypoint" is greater than the length of array minus 1, 
    // We are on a valid integer and therefore can move towards the next generated path.
    if (dist <= 3f)
    {           
        currentWaypoint++;
        if (currentWaypoint <= Route2.Length - 1)
            StartCoroutine(GeneratePath2());
        if (currentWaypoint == Route2.Length)
        {
            currentWaypoint = 0;
            stateConditions._thirsty = false;
        }
    }
}
}