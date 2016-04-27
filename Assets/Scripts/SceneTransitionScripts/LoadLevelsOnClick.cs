using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelsOnClick : MonoBehaviour
{
public static LoadLevelsOnClick instance;
  
private void Start()
    {
    instance = this;
    }
public void LoadPlayScene ( )
    {
    SceneManager.LoadScene ( "PlayScene", LoadSceneMode.Single );
    }
 
public void LoadMenuScene ( )
    {
    SceneManager.LoadScene ( "Menu", LoadSceneMode.Single );
    }
public void ExitGame()
    {
    Application.Quit ( );
    }
}
