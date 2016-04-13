using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
    {
    private Button button;
    private Text buttonText;
    public Text tempInfo;
    public bool paused;

    void Start ( )
        {
        button = GetComponent<Button> ( );
        buttonText = button.GetComponentInChildren<Text> ( );
        tempInfo.text = "Press Start to begin!";
        paused = false;
        Time.timeScale = 0.0f;
        buttonText.text = "Start!";
        }
    //ToDo fix press start message!
    public void Paused ( )
        {
        paused = !paused;
        if ( paused )
            {
            tempInfo.text = "";
            Time.timeScale = 1.0f;
            buttonText.text = "Pause";
            }
        if ( !paused )
            {
            tempInfo.text = "Press Start to begin!";
            buttonText.text = "Start!";
            Time.timeScale = 0.0F;
            }
        }
    }
