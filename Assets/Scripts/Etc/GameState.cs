using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameState : MonoBehaviour
    {
    private Button button;
    private Text buttonText;
    public Text tempInfo;
    public bool paused;
    [SerializeField]
    private GameObject timer;

    private void Update ( )
        {
        if ( Time.timeScale < 1.0f )
            {
            tempInfo.text = "Druk op Start om te beginnen!";
            }

        }

    private void Start ( )
        {
        timer.SetActive ( false );
        GlobalAudio.instance.SoundPause ( );
        Application.runInBackground = true;
        button = GetComponent<Button> ( );
        buttonText = button.GetComponentInChildren<Text> ( );
        tempInfo.text = "Druk op Start om te beginnen!";
        paused = false;
        Time.timeScale = 0.0f;
        buttonText.text = "Start!";
        }

    public void Paused ( )
        {
        paused = !paused;
        if ( paused )
            {
            if ( timer.activeSelf == false )
                timer.SetActive ( true );
            tempInfo.text = "";
            Time.timeScale = 1.0f;
            buttonText.text = "Pauze";
            GlobalAudio.instance.SoundStart ( );
            }
        if ( !paused )
            {
            buttonText.text = "Start!";
            Time.timeScale = 0.0f;
            GlobalAudio.instance.SoundPause ( );
            }
        }
    }
