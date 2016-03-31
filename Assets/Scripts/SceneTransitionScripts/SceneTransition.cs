using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
    {
    public void LoadPlayScene ( )
        {

        SceneFadeTransition.instance.LoadScene ( "PlayScene" );

        }

    public void LoadMenuScene ( )
        {

        SceneFadeTransition.instance.LoadScene ( "Menu" );

        }
    }
