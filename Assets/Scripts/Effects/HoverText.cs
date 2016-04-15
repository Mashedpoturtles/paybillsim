using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.BillSystem;
using System;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
    [SerializeField]
    private Text target;
    [SerializeField]
    private GameObject storageField;
    [SerializeField]
    private GameObject payField;
    [SerializeField]
    private GameObject spawnField;


    public void OnPointerEnter ( PointerEventData eventData )
        {
        if ( gameObject == storageField )
            {
            target.text = "Store your bills here to pay them later.";
            }
        else if ( gameObject == payField )
            {
            target.text = "Drop your card here to pay the bill.";
            }
        }

    public void OnPointerExit ( PointerEventData eventData )
        {
        target.text = "";
        }
    }
