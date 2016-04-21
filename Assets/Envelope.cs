using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Envelope : MonoBehaviour{
    [SerializeField]
    private Animator anim;

   
    private void OnMouseEnter ( )
        {
        anim.SetBool ( "Hovering", true);
        Debug.Log ( "hovering true" );
        }
    private void OnMouseExit()
        {
        anim.SetBool ( "Hovering", false );
        Debug.Log ( "hovering false" );
        } 

    private void Start ()
        {
        anim = GetComponentInChildren<Animator> ( );
        }
}
