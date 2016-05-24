using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
    {
    [SerializeField]
    private Button button;
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Text tempInfo;

    private void Start ( )
        {
        GlobalAudio.instance.SoundPause ( );
        Application.runInBackground = true;
        tempInfo.text = "Druk op Start om te beginnen!";
        buttonText.text = "Start!";
        }
    /// <summary>
    /// Pause or Unpause the game.
    /// </summary>
    public void PauseState ( )
        {
        GameManager.Instance.Pause ( );
        CheckPauseState ( );
        }

    public void CheckPauseState ( )
        {
        if ( GameManager.Instance.IsPaused == true )
            {
            buttonText.text = "Start!";
            GlobalAudio.instance.SoundPause ( );
            tempInfo.text = " ";
            }
        if ( GameManager.Instance.IsPaused == false )
            {
            buttonText.text = "Pauze";
            GlobalAudio.instance.SoundStart ( );
            }
        }
    }
