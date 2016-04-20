using UnityEngine;
using System.Collections;

public class SortTextToParent : MonoBehaviour {

    void Start ( )
        {
        this.GetComponent<Renderer>().sortingLayerID = this.transform.parent.GetComponent<Renderer>().sortingLayerID;
        this.GetComponent<Renderer> ( ).sortingOrder = this.transform.parent.GetComponent<Renderer> ( ).sortingOrder;

        }
    }
