using UnityEngine;
using System.Collections;

public class BillManager : MonoBehaviour {

    public static BillManager instance;
 
 
    void Start()
    {
        instance = this;
    }

}
