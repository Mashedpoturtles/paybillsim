using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

    public RectTransform parentToReturnTo = null;
    public RectTransform placeholderParent = null;
    public static Draggable instance;
    public bool dragOnSurfaces = true;
    GameObject placeholder = null;
    GameObject MyParent;
    private void Start ( )
        {
        instance = this;
        MyParent = transform.parent.parent.gameObject;
        }

    public void DestroyParent ( )
        {
        Destroy ( MyParent );
        }

    public void OnBeginDrag ( PointerEventData eventData )
        {
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
        else
            placeholderParent = canvas.transform as RectTransform;

        SetDraggedPosition ( eventData );

        }

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
            SetDraggedPosition ( eventData );
        }
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
    public void SetNewParent ( Transform t )
        {
        parentToReturnTo = t as RectTransform;
        transform.rotation= parentToReturnTo.rotation;
        transform.position = parentToReturnTo.position;
        DestroyParent ( );
        }

public void SetNewParent ( )
        {
        this.transform.SetParent ( parentToReturnTo);
        this.transform.SetSiblingIndex ( placeholder.transform.GetSiblingIndex ( ) );
        GetComponent<CanvasGroup> ( ).blocksRaycasts = true;

        Destroy ( placeholder );
        DestroyParent ( );
        }
    public void OnEndDrag ( PointerEventData eventData )
        {
        SetNewParent ( );
        DestroyParent ( );
        }

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
