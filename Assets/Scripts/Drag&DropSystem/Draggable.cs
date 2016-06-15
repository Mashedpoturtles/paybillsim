using Assets.BillSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
    public RectTransform parentToReturnTo = null;
    public RectTransform placeholderParent = null;
    [SerializeField]
    private Transform storage;
    private GameObject closeUpZone;
    public static Draggable instance;
    public bool dragOnSurfaces = true;
    GameObject placeholder = null;
    GameObject MyParent;

    private void Start ( )
        {
        instance = this;
        MyParent = transform.parent.parent.gameObject;
        closeUpZone = GameObject.FindWithTag ( "CloseUpZone" );
        storage = GameObject.FindWithTag ( "Storage" ).GetComponent<RectTransform> ( );
        }
    /// <summary>
    /// Destroys the parent of the draggable object. Only call on objects that are temporary instantiated holders!
    /// </summary>
    public void DestroyParent ( )
        {
        if ( MyParent != null )
            {
            transform.SetParent ( MyParent.transform.parent, true );
            }
        Destroy ( MyParent );
        }

    public void OnPointerClick ( PointerEventData eventData )
        {
        if ( transform.parent == storage.transform )
            {
            if ( closeUpZone.transform.childCount == 0 )
                {
                transform.SetParent ( closeUpZone.transform as RectTransform );
                this.transform.localPosition = Vector3.zero;
                this.transform.localScale = new Vector3 ( 1, 1, 1 );
                this.transform.localRotation = new Quaternion ( 0, 0, 0, 0 );
                }
            }
        else if ( transform.parent == closeUpZone.transform )
            {
            transform.SetParent ( storage.transform as RectTransform );
            this.transform.localPosition = Vector3.zero;
            this.transform.localScale = new Vector3 ( 1, 1, 1 );
            this.transform.localRotation = new Quaternion ( 0, 0, 0, 0 );
            }

        if ( transform.parent == closeUpZone.transform || transform.parent == storage.transform )
            if ( eventData.pointerPress )
                {
                List<Bill> tempBillList = new List<Bill> ( BillManager.Bills );
                foreach ( var bill in tempBillList )
                    {
                    Debug.Log ( "Bill cost unsplit" + bill.Cost );
                    if ( bill.Object == this.gameObject )
                        {
                        if ( InstalmentSystem.instance.PayWithInstalments == true )
                            {
                            Debug.Log ( "Splitting bill: " + bill.Cost + " to parts: " + InstalmentSystem.instance.InstalmentsToPayIn );
                            BillManager.instance.SplitBillsInTerms ( bill, InstalmentSystem.instance.InstalmentsToPayIn );
                            Debug.Log ( "bill costs after split: " + bill.Cost );
                            InstalmentSystem.instance.DisableAfterInstalment ( );
                            }
                        }
                    }
                foreach ( var bill in BillManager.Bills )
                    Debug.Log ( "Bill cost unsplit" + bill.Cost );
                }
        }

    /// <summary>
    /// Implement the IbeginDraghandler interface.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag ( PointerEventData eventData )
        {
        if ( GameManager.Instance.IsPaused == false )
            {
            GameManager.Instance.Pause ( );
            }


        var canvas = FindInParents<Canvas> ( gameObject );
        if ( canvas == null )
            return;

        placeholder = new GameObject ( );
        placeholder.transform.SetParent ( this.transform.parent );

        LayoutElement le = placeholder.AddComponent<LayoutElement> ( );
        le.preferredWidth = this.GetComponent<LayoutElement> ( ).preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement> ( ).preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex ( this.transform.GetSiblingIndex ( ) );

        parentToReturnTo = transform.parent as RectTransform;
        placeholderParent = parentToReturnTo;

        this.transform.SetParent ( this.transform.parent.parent );
        GetComponent<CanvasGroup> ( ).blocksRaycasts = false;

        if ( dragOnSurfaces )
            placeholderParent = transform as RectTransform;

        SetDraggedPosition ( eventData );
        }

    /// <summary>
    /// Implement the Idrag interface
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag ( PointerEventData eventData )
        {
        this.transform.position = eventData.position;

        if ( placeholder.transform.parent != placeholderParent )
            placeholder.transform.SetParent ( placeholderParent );

        int newSiblingIndex = placeholderParent.childCount;
        for ( int i = 0 ; i < placeholderParent.childCount ; i++ )
            {
            if ( this.transform.position.x < placeholderParent.GetChild ( i ).position.x )
                {
                newSiblingIndex = i;
                if ( placeholder.transform.GetSiblingIndex ( ) < newSiblingIndex )
                    newSiblingIndex--;

                break;
                }
            }
        placeholder.transform.SetSiblingIndex ( newSiblingIndex );
        if ( gameObject != null )
            {
            SetDraggedPosition ( eventData );
            }
        }

    /// <summary>
    /// Implement the IEndDragHandler interface
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag ( PointerEventData eventData )
        {
        SetNewParent ( );

        this.transform.localPosition = Vector3.zero;
        this.transform.localScale = new Vector3 ( 1, 1, 1 );
        this.transform.localRotation = new Quaternion ( 0, 0, 0, 0 );

        Text buttonText = GameObject.FindWithTag ( "pausebutton" ).GetComponentInChildren<Text> ( );
        if ( buttonText.text != "Start!" )
            {
            GameManager.Instance.UnPause ( );
            }
        }

    /// <summary>
    /// Set the dragged position to that of the the mouse position
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition ( PointerEventData eventData )
        {
        if ( dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null )
            placeholderParent = eventData.pointerEnter.transform as RectTransform;

        Vector3 globalMousePos;

        if ( RectTransformUtility.ScreenPointToWorldPointInRectangle ( placeholderParent, eventData.position, eventData.pressEventCamera, out globalMousePos ) )
            {
            transform.position = globalMousePos;
            transform.rotation = placeholderParent.rotation;
            }
        }

    /// <summary>
    /// Overload for manually setting the parent back to the DropZone.cs transform if the object is not allowed to parent to the PayZone.cs transform.
    /// </summary>
    /// <param name="t"></param>
    public void SetNewParent ( Transform t )
        {
        parentToReturnTo = t as RectTransform;

        transform.rotation = parentToReturnTo.rotation;
        transform.position = parentToReturnTo.position;
        }

    public void SetNewParent ( )
        {
        this.transform.SetParent ( parentToReturnTo );
        this.transform.SetSiblingIndex ( placeholder.transform.GetSiblingIndex ( ) );
        GetComponent<CanvasGroup> ( ).blocksRaycasts = true;

        Destroy ( placeholder );
        }

    /// <summary>
    /// Not a clue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    static public T FindInParents<T> ( GameObject go ) where T : Component
        {
        if ( go == null ) return null;
        var comp = go.GetComponent<T> ( );

        if ( comp != null )
            return comp;

        Transform t = go.transform.parent;
        while ( t != null && comp == null )
            {
            comp = t.gameObject.GetComponent<T> ( );
            t = t.parent;
            }
        return comp;
        }
    }
