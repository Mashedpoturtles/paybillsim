using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Envelope : MonoBehaviour{
    [SerializeField]
    private Animator anim;

   
    private void OnMouseDown ( )
        {
        anim.SetBool ( "Hovering", true);
        }
    private void OnMouseExit ( )
        {
        anim.SetBool ( "Hovering", false );
        }
 
    private void Start ()
        {
        anim = GetComponentInChildren<Animator> ( );
        }
}
