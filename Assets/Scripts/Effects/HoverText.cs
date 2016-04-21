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
            target.text = "Bewaar je rekeningen hier om later te betalen.";
            }
        else if ( gameObject == payField )
            {
            target.text = "Sleep je rekening hier heen om de rekening te betalen.";
            }
        }

    public void OnPointerExit ( PointerEventData eventData )
        {
        target.text = "";
        }
    }
