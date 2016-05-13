using UnityEngine;
using System.Collections.Generic;

public class ActivityOnLoad : MonoBehaviour
    {
    [SerializeField]
    public GameObject PlayButton;
    public GameObject ContinueButton;
    public static List<GameObject> ObjectsToDisableForPlayScene;
    public static List<GameObject> ObjectsToDisableForStartMenu;

    private void OnLevelWasLoaded ( int level )
        {
        foreach ( var obj in ObjectsToDisableForPlayScene )
            {
            obj.SetActive ( level == 1 );
            }
        foreach ( var obj in ObjectsToDisableForStartMenu )
            {
            obj.SetActive ( level == 2 );
            }
        }

    private void Start ( )
        {
        ObjectsToDisableForPlayScene = new List<GameObject> ( );
        ObjectsToDisableForStartMenu = new List<GameObject> ( );
        ObjectsToDisableForPlayScene.Add ( PlayButton );
        ObjectsToDisableForPlayScene.Add ( ContinueButton );
        foreach ( var obj in ObjectsToDisableForPlayScene )
            {
            obj.SetActive ( true );
            }
        foreach ( var obj in ObjectsToDisableForStartMenu )
            {
            obj.SetActive ( false );
            }
        }
    }
