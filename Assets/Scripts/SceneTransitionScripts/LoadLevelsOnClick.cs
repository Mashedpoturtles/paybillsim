using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelsOnClick : MonoBehaviour
    {

    public void LoadPlayScene ( )
        {
        SceneManager.LoadScene ( "PlayScene", LoadSceneMode.Single );
        ButtonBrancher.lifeTimer = 6;
        }

    public void LoadMenuScene ( )
        {
        SceneManager.LoadScene ( "Menu", LoadSceneMode.Single );
        ButtonBrancher.lifeTimer = 6;
        }
    }
