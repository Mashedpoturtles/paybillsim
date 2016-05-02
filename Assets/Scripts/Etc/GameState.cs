using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
    {
    private Button button;
    private Text buttonText;
    public Text tempInfo;
    public bool paused;

    private void Update ( )
        {
        if ( GameManager.Instance.TimeSpeed <= 1f )
            {
            tempInfo.text = "Druk op Start om te beginnen!";
            }
        }

    private void Start ( )
        {
        GlobalAudio.instance.SoundPause ( );
        Application.runInBackground = true;
        button = GetComponent<Button> ( );
        buttonText = button.GetComponentInChildren<Text> ( );
        tempInfo.text = "Druk op Start om te beginnen!";
        paused = false;
        GameManager.Instance.Paused ( );
        buttonText.text = "Start!";
        }

    public void Paused ( )
        {
        paused = !paused;
        if ( paused )
            {
            GameManager.Instance.UnPause ( );
            buttonText.text = "Pauze";
            GlobalAudio.instance.SoundStart ( );
            }
        if ( !paused )
            {
            buttonText.text = "Start!";
            GameManager.Instance.Paused ( );
            GlobalAudio.instance.SoundPause ( );
            }
        }
    }
