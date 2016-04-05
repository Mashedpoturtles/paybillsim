using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonFader : MonoBehaviour
    {
    Image buttonImage;
    Text text;
    Color buttonColor;
    Color textColor;
    public bool faded = false;
    bool startFade = false;
    float smooth = 0;
    bool initialized = false;

    private void Start ( )
        {
        Initialize ( );
        }
    private void Initialize ( )
        {
        startFade = false;
        faded = false;
        buttonImage = GetComponent<Image> ( );
        buttonColor = buttonImage.color;
        if ( GetComponentInChildren<Text> ( ) )
            {
            text = GetComponentInChildren<Text> ( );
            textColor = text.color;
            }
        initialized = true;
        }
    private void Update ( )
        {
        if ( startFade )
            {
            Fade ( smooth );
            if ( buttonColor.a > 0.9 )
                faded = true;
            }
        }
    public void Fade ( float rate )
        {
        if ( !initialized )
            {
            Initialize ( );
            }
        smooth = rate;
        startFade = true;

        buttonColor.a += rate;
        buttonImage.color = buttonColor;

        if ( text )
            {
            textColor.a += rate;
            text.color = textColor;
            }
        }
    }
