using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
[SerializeField]
private Transform zoomTarget;
[SerializeField]
private Transform rotateTargetLeft;
[SerializeField]
private Transform rotateTargetRight;
[SerializeField]
private float speed;
private Vector3 newDir;
private Vector3 targetDir;
private Quaternion leftRot;
   
private void Update ( )
    {
    speed = 200;
    var step = speed * Time.deltaTime;
    if ( Input.GetKey ( KeyCode.Q ) )
        {
        Camera.main.transform.position = Vector3.MoveTowards ( transform.position, zoomTarget.transform.position, step );

        targetDir = rotateTargetLeft.transform.position - transform.position;
        newDir = Vector3.RotateTowards ( transform.forward, targetDir, step, 0.0f );
        transform.rotation = Quaternion.LookRotation ( newDir );
        return;
        }

    if ( Input.GetKey ( KeyCode.E ) )
        {
        Camera.main.transform.position = Vector3.MoveTowards ( transform.position, zoomTarget.transform.position, step );

        targetDir = rotateTargetRight.transform.position - transform.position;
        newDir = Vector3.RotateTowards ( transform.forward, targetDir, step, 0.0f );
        transform.rotation = Quaternion.LookRotation ( newDir );
        return;
        }    
    else
        {
        Camera.main.transform.position = Vector3.MoveTowards ( transform.position, new Vector3 ( 0, 0, 0 ), step );
        transform.rotation = Quaternion.identity;         
        return;
        }
    }
}
