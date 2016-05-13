using System;
using Assets.BillSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {

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
    }
