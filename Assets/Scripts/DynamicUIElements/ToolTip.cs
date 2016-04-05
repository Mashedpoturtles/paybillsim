using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
    {
    /// <summary>
    /// Animtion settings to alter in the inspector.
    /// </summary>
    [System.Serializable] //serializes class for inspector
    public class AnimationSettings
        {
        public enum OpenStyle
            { WidthToHeight, HeightToWidth, HeightAndWidth };
        public OpenStyle openstyle;
        public float widthSmooth = 4.6f, heightSmooth = 4.6f;
        public float textSmooth = 0.1f;

        [HideInInspector]
        public bool widthOpen = false, heightOpen = false;

        /// <summary>
        /// Initialize the start values.
        /// </summary>
        public void Initialize ( )
            {
            widthOpen = false;
            heightOpen = false;
            }
        }
    /// <summary>
    /// UI settings to alter in the inspector.
    /// </summary>
    [System.Serializable]
    public class UISettings
        {
        public Image textBox;
        public Text text;
        public Vector2 initialBoxSize = new Vector2 ( 0.25f, 0.25f );
        public Vector2 openedBoxSize = new Vector2 ( 400, 200 );
        public float snapToSizeDistance = 0.25f;
        public float lifeSpan = 10;

        [HideInInspector]
        public bool opening = false;
        [HideInInspector]
        public Color textColor;
        [HideInInspector]
        public Color textBoxColor;
        [HideInInspector]
        public RectTransform textBoxRect;
        [HideInInspector]
        public Vector2 currentSize;
        /// <summary>
        /// Initialize the start values.
        /// </summary>
        public void Initialize ( )
            {
            textBoxRect = textBox.GetComponent<RectTransform> ( );
            textBoxRect.sizeDelta = initialBoxSize;
            currentSize = textBoxRect.sizeDelta;
            opening = false;

            textColor = text.color;
            textColor.a = 0;
            text.color = textColor;
            textBoxColor = textBox.color;
            textBoxColor.a = 1;
            textBox.color = textBoxColor;
            }
        }

    //-- public references to classes so they can be seen and modified from the inspector
    public AnimationSettings animsettings = new AnimationSettings ( );
    public UISettings uiSettings = new UISettings ( );

    float lifeTimer = 0;

    private void Start ( )
        {
        animsettings.Initialize ( );
        uiSettings.Initialize ( );
        }

    /// <summary>
    /// This is called when the button is clicked
    /// </summary>
    public void StartOnClick ( )
        {
        uiSettings.opening = true;
        }

    private void Update ( )
        {
        if ( uiSettings.opening )
            {
            OpenToolTip ( );

            if ( animsettings.widthOpen && animsettings.heightOpen )
                {
                lifeTimer += Time.deltaTime;
                if ( lifeTimer > uiSettings.lifeSpan )
                    {
                    FadeToolTipOut ( );
                    }
                else
                    {
                    FadeTextIn ( );
                    }
                }
            }
        }

    private void OpenToolTip ( )
        {
        switch ( animsettings.openstyle )
            {
            case AnimationSettings.OpenStyle.HeightToWidth:
                OpenHeightToWidth ( );
                break;
            case AnimationSettings.OpenStyle.WidthToHeight:
                OpenWidthToHeight ( );
                break;
            case AnimationSettings.OpenStyle.HeightAndWidth:
                OpenHeightAndWidth ( );
                break;
            default:
                Debug.LogError ( "No animation is selected for the selected open style." );
                break;
            }
        uiSettings.textBoxRect.sizeDelta = uiSettings.currentSize;
        }

    private void OpenWidthToHeight ( )
        {
        if ( !animsettings.widthOpen )
            {
            OpenWidth ( );
            }
        else
            {
            OpenHeight ( );
            }
        }

    private void OpenHeightToWidth ( )
        {
        if ( !animsettings.heightOpen )
            {
            OpenHeight ( );
            }
        else
            {
            OpenWidth ( );
            }
        }

    private void OpenHeightAndWidth ( )
        {
        if ( animsettings.heightOpen && animsettings.widthOpen )
            {
            OpenHeightAndWidth ( );
            }
        }

    private void OpenWidth ( )
        {
        uiSettings.currentSize.x = Mathf.Lerp ( uiSettings.currentSize.x, uiSettings.openedBoxSize.x, animsettings.widthSmooth * Time.deltaTime );
        //-- Snap to target width and set opened to true when width is close enoughto target width.
        if ( Mathf.Abs ( uiSettings.currentSize.x - uiSettings.openedBoxSize.x ) < uiSettings.snapToSizeDistance )
            {
            uiSettings.currentSize.x = uiSettings.openedBoxSize.x;
            animsettings.widthOpen = true;
            }
        }

    private void OpenHeight ( )
        {
        uiSettings.currentSize.y = Mathf.Lerp ( uiSettings.currentSize.y, uiSettings.openedBoxSize.y, animsettings.heightSmooth * Time.deltaTime );
        //-- Snap to target height and set opened to true when height is close enough to target height.
        if ( Mathf.Abs ( uiSettings.currentSize.y - uiSettings.openedBoxSize.y ) < uiSettings.snapToSizeDistance )
            {
            uiSettings.currentSize.y = uiSettings.openedBoxSize.y;
            animsettings.heightOpen = true;
            }
        }

    private void FadeTextIn ( )
        {
        uiSettings.textColor.a = Mathf.Lerp ( uiSettings.textColor.a, 1, animsettings.textSmooth * Time.deltaTime );
        uiSettings.text.color = uiSettings.textColor;
        }
    /// <summary>
    /// Finish animation and re-initialize 
    /// </summary>
    private void FadeToolTipOut ( )
        {
        uiSettings.textColor.a = Mathf.Lerp ( uiSettings.textColor.a, 0, animsettings.textSmooth * Time.deltaTime );
        uiSettings.text.color = uiSettings.textColor;
        uiSettings.textBoxColor.a = Mathf.Lerp ( uiSettings.textBoxColor.a, 0, animsettings.textSmooth * Time.deltaTime );
        uiSettings.textBox.color = uiSettings.textBoxColor;

        if ( uiSettings.textBoxColor.a < 0.01f )
            {
            uiSettings.opening = false;
            animsettings.Initialize ( );
            uiSettings.Initialize ( );
            lifeTimer = 0;
            }
        }
    }
