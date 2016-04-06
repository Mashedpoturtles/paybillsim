using UnityEngine;
using System.Collections.Generic;

public class ActivityOnLoad : MonoBehaviour
    {
    [SerializeField]
    public GameObject PlayButton;
    public GameObject ContinueButton;
    public static List<GameObject> ObjectsToDisable;

    private void OnLevelWasLoaded ( int level )
        {
        foreach ( var obj in ObjectsToDisable )
            {
            obj.SetActive ( level == 1 );
            }
        }

    private void Start ( )
        {
        ObjectsToDisable = new List<GameObject> ( );
        ObjectsToDisable.Add ( PlayButton );
        ObjectsToDisable.Add ( ContinueButton );
        foreach ( var obj in ObjectsToDisable )
            {
            obj.SetActive ( true );
            }
        }
    }
