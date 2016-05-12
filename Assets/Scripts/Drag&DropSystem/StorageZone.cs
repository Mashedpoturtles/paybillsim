using System;
using Assets.BillSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

    private Animator anim;

    void Awake ( )
        {
        anim = GetComponent<Animator> ( );
        }

    public void OnPointerEnter ( PointerEventData eventData )
        {
        if ( eventData.pointerDrag == null )
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable> ( );
        if ( d != null )
            {
            d.placeholderParent = transform as RectTransform;
            }
        }

    public void OnPointerExit ( PointerEventData eventData )
        {
        if ( eventData.pointerDrag == null )
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable> ( );
        if ( d != null && d.placeholderParent == transform )
            {
            d.placeholderParent = d.parentToReturnTo;
            }
        }

    public void OnDrop ( PointerEventData data )
        {
        Draggable d = data.pointerDrag.GetComponent<Draggable> ( );
        if ( d != null )
            {
            d.parentToReturnTo = transform as RectTransform;
            foreach ( Bill bill in BillManager.Bills )
                {
                if ( bill.Object != null )
                    {
                    d.DestroyParent ( );
                    break;
                    }
                }
            }
        }


    public void OnPointerClick ( PointerEventData eventData )
        {
        if ( !eventData.dragging )
            {
            var isClicked = anim.GetBool ( "IsClicked" );
            anim.SetBool ( "IsClicked", true );
            if ( isClicked == true )
                {
                anim.SetBool ( "IsClicked", false );
                }
            }
        }
    }
