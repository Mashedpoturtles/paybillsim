using UnityEngine;
using System.Collections;
using Pathfinding;

public class Pathing : MonoBehaviour
    {
    [SerializeField]
    Animator anim;
    private CharacterController characterController;
    [SerializeField]
    private Seeker seeker;
    [SerializeField]
    public Path path;
    public Transform target;
    public int currentWaypoint;
    public float speed = 15f;
    public float maxWaypointDistance = 2f;
    public float maxTargetDistance = 5f;
    private Transform existingTarget;

    void Start ( )
        {
        anim = GetComponent<Animator> ( );
        characterController = GetComponent<CharacterController> ( );
        seeker.GetComponent<Seeker> ( );
        }

    public void OnPathComplete ( Path p )
        {
        if ( !p.error )
            {
            path = p;
            currentWaypoint = 0;
            }
        else
            {
            Debug.Log ( p.error );
            }
        }

    void FixedUpdate ( )
        {
        if ( path == null )
            {
            return;
            }
        if ( currentWaypoint >= path.vectorPath.Count )
            {
            return;
            }
        //assign a speed to the direction were headed 
        Vector3 dir = ( path.vectorPath [ currentWaypoint ] - transform.position ).normalized * speed;
        //set the speed of the direction to the character controller
        characterController.SimpleMove ( dir );

        anim.SetFloat ( "Speed", characterController.velocity.magnitude );

        //if the distance between the character and next waypoint is less than the M.W.P.D. then keep moving through W.P.'s
        if ( Vector3.Distance ( transform.position, path.vectorPath [ currentWaypoint ] ) < maxWaypointDistance )
            {
            currentWaypoint++;
            }
        //if the distance between the character and the target is less than the M.T.D. then halt the speed to 0
        if ( Vector3.Distance ( transform.position, existingTarget.transform.position ) < maxTargetDistance )
            {
            speed = 0f;
            }
        //otherwise set the speed back to the default 40
        else
            {
            speed = 40f;
            }
        }
    /// <summary>
    /// This will move the npc to a random child transform inside the target.
    /// </summary>
    /// <param name="target"></param>
    public void MoveToRandom ( Transform target )
        {
        //initialize array
        Transform [ ] targetArray;
        //fill the target array with the children transforms from the target
        targetArray = target.GetComponentsInChildren<Transform> ( );
        //cache the new target to this target
        existingTarget = target;
        //if the existing target equals a new target, assign the existing target to a new random target from the target array
        while ( existingTarget == target )
            {
            existingTarget = targetArray [ Random.Range ( 0, targetArray.Length ) ];
            }
        //finally the target will be assigned the new existing target value and start moving to the target
        target = existingTarget;
        seeker.StartPath ( transform.position, target.position, OnPathComplete );
        foreach ( var Object in targetArray )
            {
            //Debug.Log ( this.gameObject.name + " is moving to: " + existingTarget );
            characterController.transform.LookAt ( existingTarget );
            }
        }
    }
