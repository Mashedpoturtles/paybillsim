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

    /// <summary>
    /// Implement the IbeginDraghandler interface.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag ( PointerEventData eventData )
        {
        GameManager.Instance.Paused ( );
        var canvas = FindInParents<Canvas> ( gameObject );
        if ( canvas == null )
            return;
        //When you drag the draggable object from its parent to a new parent:
        //First a new empty game object child gets 
        //Made of the potential parent you are currently dragging over.
        //which destroys once you stop dragging or drop the object and then parents the draggable object to the actual
        //Dropzone. 
        placeholder = new GameObject ( );
        //As we begin dragging, the draggable objects current parent will get its placeholder parented to the same parent.
        placeholder.transform.SetParent ( this.transform.parent );

        //Adds a layout element to the placeholder to keep the layout element behaviour in place when 
        //The the parent is a placeholder empty game object.
        LayoutElement le = placeholder.AddComponent<LayoutElement> ( );
        //The settings for the layout element.
        le.preferredWidth = this.GetComponent<LayoutElement> ( ).preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement> ( ).preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        //Sets the sibling index of the placeholder to that of the current draggable object.
        placeholder.transform.SetSiblingIndex ( this.transform.GetSiblingIndex ( ) );
        //Sets the parent to return to whatever the starting parent of this draggable object was from the moment we
        //Begun to drag as a RectTransform.
        parentToReturnTo = transform.parent as RectTransform;
        //Sets the placeholder parent as the parent to return to in case of releasing our drag on a non-dropable area.
        placeholderParent = parentToReturnTo;
        //Sets this draggable objects parent to the parent of the placeholder to get the actual droppable zone as parent.
        this.transform.SetParent ( this.transform.parent.parent );
        //Gets the objects canvas group and sets raycast block to all children to false.
        GetComponent<CanvasGroup> ( ).blocksRaycasts = false;

        //Checks if the object is dragging over a surface
        if ( dragOnSurfaces )
            //If so then the placeholder parent will take on the transform behavior of the surface were dragging over.
            placeholderParent = transform as RectTransform;
        else
            //Otherwise the draggable object will be aligning to the surface of the canvas.
            placeholderParent = canvas.transform as RectTransform;
        //Set the dragged position to that of the the mouse position
        SetDraggedPosition ( eventData );
        }

    /// <summary>
    /// Implement the Idrag interface
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag ( PointerEventData eventData )
        {
        //Sets the draggable object's position to that of the mouse position.
        this.transform.position = eventData.position;

        //When the placeholders's parent is not set to the placeholder parent RecTransform
        //We set it to that.
        if ( placeholder.transform.parent != placeholderParent )
            placeholder.transform.SetParent ( placeholderParent );

        //We set an integer with the name newSiblingIndex and give it the value of childcount in the placeholder parent.
        int newSiblingIndex = placeholderParent.childCount;
        //Increase counting i when i is less than childcount inside the placeholder parent.
        for ( int i = 0 ; i < placeholderParent.childCount ; i++ )
            {
            //If the draggable objects x position is less than the placeholderparents children position
            if ( this.transform.position.x < placeholderParent.GetChild ( i ).position.x )
                {
                //Then we update the sibling index to the current value of local int i.
                newSiblingIndex = i;
                //If the current sibling index is less than the updated sibling index we subtract from the
                //NewSiblingIndex.
                if ( placeholder.transform.GetSiblingIndex ( ) < newSiblingIndex )
                    newSiblingIndex--;

                break;
                }
            }
        //Set the placeholder's sibling index to the new sibling index.
        placeholder.transform.SetSiblingIndex ( newSiblingIndex );
        //so long as this draggable game object exists we set the dragged position.
        if ( gameObject != null )
            {
            SetDraggedPosition ( eventData );
            }
        }

    /// <summary>
    /// Set the dragged position to that of the the mouse position
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition ( PointerEventData eventData )
        {
        //If were dragging over a surface and we are raycasting onto a RectTransform...
        if ( dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null )
            //Then the placeholderparent RectTransform will be whatever we're hovering above currently.
            placeholderParent = eventData.pointerEnter.transform as RectTransform;
        //Local vector 3 variable to keep track of the mouse position.
        Vector3 globalMousePos;
        //Hover functionality in the world space. 
        //Gets the screenpoint of our monitor relative to the 3D world to find the mouse position in the 3D world.
        if ( RectTransformUtility.ScreenPointToWorldPointInRectangle ( placeholderParent, eventData.position, eventData.pressEventCamera, out globalMousePos ) )
            {
            //Set the draggable object's position to the position of the mouse in world space.
            transform.position = globalMousePos;
            //And set the draggable object's rotation to that of the placeholder parent.
            transform.rotation = placeholderParent.rotation;
            }
        }

    /// <summary>
    /// Overload for manually setting the parent back to the DropZone.cs transform if the object is not allowed to parent to the PayZone.cs transform.
    /// </summary>
    /// <param name="t"></param>
    public void SetNewParent ( Transform t )
        {
        //Force the parent to return to the given Transform
        parentToReturnTo = t as RectTransform;
        //Set the draggable object's rotation and position to that of the parent to return to.
        transform.rotation = parentToReturnTo.rotation;
        transform.position = parentToReturnTo.position;
        }

    /// <summary>
    /// Set the parent of this draggable object to its parent to return to.
    /// </summary>
    public void SetNewParent ( )
        {
        //Set the parent of this draggable object to its parent to return to.
        this.transform.SetParent ( parentToReturnTo );
        //Sets this draggable objects sibling index to that of the placeholder
        this.transform.SetSiblingIndex ( placeholder.transform.GetSiblingIndex ( ) );
        GetComponent<CanvasGroup> ( ).blocksRaycasts = true;

        //Once the draggable gameobject has been given its new parent we can destroy the placeholder that is an empty game object.
        Destroy ( placeholder );
        }

    /// <summary>
    /// Implement the IEndDragHandler interface
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag ( PointerEventData eventData )
        {
        SetNewParent ( );
        // Reset the localposition , scale and rotation of the draggable object to prevent it from changing while
        //Moving parents.
        this.transform.localPosition = Vector3.zero;
        this.transform.localScale = new Vector3 ( 1, 1, 1 );
        this.transform.localRotation = new Quaternion ( 0, 0, 0, 0 );
        GameManager.Instance.UnPause ( );
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
