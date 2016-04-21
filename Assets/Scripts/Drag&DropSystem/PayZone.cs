﻿using Assets.BillSystem;
using UnityEngine;
using UnityEngine.EventSystems;


public class PayZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
    public BillManager manager;
    public Bill bill;
    private RectTransform storage;
    private void Start ( )
        {
        storage = GameObject.FindWithTag ( "Storage" ).GetComponent<RectTransform> ( );
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
                if ( bill.Object == data.pointerDrag && bill.Cost <= Money.instance.currentMoney )
                    {
                    manager.PayBill ( bill );
                    break;
                    }
                else if ( bill.Object == data.pointerDrag && bill.Cost > Money.instance.currentMoney )
                    {
                    BillManager.instance.InsufficientFunds ( bill );
                     
                    d.SetNewParent ( storage.transform as RectTransform );
                    GlobalAudio.instance.SoundAttention ( );
                                  
                    }
                }
            }
        }
    }