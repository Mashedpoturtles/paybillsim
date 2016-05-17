using UnityEngine;
using System.Collections.Generic;

public class ButtonBrancher : MonoBehaviour
    {
    public class ButtonScaler
        {
        enum ScaleMode { MatchWidthHeight, IndependentWidthHeight }
        ScaleMode mode;
        Vector2 referenceButtonSize;

        [HideInInspector]
        public Vector2 referenceScreensize;
        public Vector2 newButtonSize;

        public void Initialize ( Vector2 refButtonSize, Vector2 refScreensize, int scaleMode )
            {
            mode = ( ScaleMode ) scaleMode;
            referenceButtonSize = refButtonSize;
            referenceScreensize = refScreensize;
            SetNewButtonSize ( );
            }

        void SetNewButtonSize ( )
            {
            if ( mode == ScaleMode.IndependentWidthHeight )
                {
                newButtonSize.x = ( referenceButtonSize.x * Screen.width ) / referenceScreensize.x;
                newButtonSize.y = ( referenceButtonSize.y * Screen.height ) / referenceScreensize.y;
                }
            else if ( mode == ScaleMode.MatchWidthHeight )
                {
                newButtonSize.x = ( referenceButtonSize.x * Screen.width ) / referenceScreensize.x;
                newButtonSize.y = newButtonSize.x;
                }
            }
        }

    [System.Serializable]
    public class RevealSettings
        {
        public enum RevealOption { linear };
        public RevealOption option;
        public float translateSmooth = 5f;
        public float fadeSmooth = 0.01f;
        public bool revealOnStart = false;

        [HideInInspector]
        public bool opening = false;
        [HideInInspector]
        public bool spawned = false;
        }

    [System.Serializable]
    public class LinearSpawner
        {
        public enum RevealStyle { SlideToPosition, FadeInAtPosition };
        public RevealStyle revealStyle;
        public Vector2 direction = new Vector2 ( 0, 1 );
        public float baseButtonSpacing = 5f;
        public int buttonNumOffset = 0;

        [HideInInspector]
        public float buttonSpacing = 5f;

        public void FitSpacingToSceenSize ( Vector2 refScreenSize )
            {
            float refScreenFloat = ( refScreenSize.x + refScreenSize.y ) / 2;
            float screenFloat = ( Screen.width + Screen.height ) / 2;
            buttonSpacing = ( baseButtonSpacing * screenFloat ) / refScreenFloat;
            }
        }

    #region  variables
    public GameObject [ ] buttonRefs;
    [HideInInspector]
    public List<GameObject> buttons;

    public enum ScaleMode { MatchWidthHeight, IndependentWidthHeight };
    public ScaleMode mode;
    public Vector2 referenceButtonSize;
    public Vector2 referenceScreenSize;

    ButtonScaler buttonScaler = new ButtonScaler ( );
    public RevealSettings revealSettings = new RevealSettings ( );
    public LinearSpawner linSpawner = new LinearSpawner ( );

    float lastScreenWidth = 0;
    float lastScreenHeight = 0;
    #endregion
    private void Start ( )
        {
        buttons = new List<GameObject> ( );
        buttonScaler = new ButtonScaler ( );
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        buttonScaler.Initialize ( referenceButtonSize, referenceScreenSize, ( int ) mode );
        linSpawner.FitSpacingToSceenSize ( buttonScaler.referenceScreensize );

        if ( revealSettings.revealOnStart )
            {
            SpawnButtons ( );
            }
        else
            {
            revealSettings.opening = false;
            }
        }

    private void Update ( )
        {
        if ( Screen.width != lastScreenWidth || Screen.height != lastScreenHeight )
            {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            buttonScaler.Initialize ( referenceButtonSize, referenceScreenSize, ( int ) mode );
            linSpawner.FitSpacingToSceenSize ( buttonScaler.referenceScreensize );
            SpawnButtons ( );
            }

        if ( revealSettings.opening )
            {
            if ( !revealSettings.spawned )
                {
                SpawnButtons ( );
                }
            switch ( revealSettings.option )
                {

                case RevealSettings.RevealOption.linear:
                    switch ( linSpawner.revealStyle )
                        {
                        case LinearSpawner.RevealStyle.SlideToPosition:
                            RevealLinearlyNormal ( );
                            break;
                        case LinearSpawner.RevealStyle.FadeInAtPosition:
                            RevealLinearlyFade ( );
                            break;
                        }
                    break;
                }
            }
        }
    /// <summary>
    /// --if revealOnStart == false, this method will be called but the button click event. --
    /// </summary>
    public void SpawnButtons ( )
        {
        if ( revealSettings.opening == false )
            {
            revealSettings.opening = true;

            for ( int i = buttons.Count - 1 ; i >= 0 ; i-- )

                Destroy ( buttons [ i ] );
            buttons.Clear ( );

            ClearCommonButtonBranchers ( );

            for ( int i = 0 ; i < buttonRefs.Length ; i++ )
                {
                GameObject _button = Instantiate ( buttonRefs [ i ] );
                _button.transform.SetParent ( transform );
                _button.transform.position = transform.position;
                buttons.Add ( _button );
                }
            revealSettings.spawned = true;
            }
        else
            {
            DespawnButtons ( );
            }
        }

    private void RevealLinearlyNormal ( )
        {
        for ( int i = 0 ; i < buttons.Count ; i++ )
            {
            //-- Target to move towards.
            Vector3 targetPos;
            RectTransform buttonRect = buttons [ i ].GetComponent<RectTransform> ( );
            //-- Sets size.
            buttonRect.sizeDelta = new Vector2
                ( buttonScaler.newButtonSize.x, buttonScaler.newButtonSize.y );
            //-- Sets the position towards the target.
            targetPos.x = linSpawner.direction.x * ( ( i + linSpawner.buttonNumOffset ) * ( buttonRect.sizeDelta.x + linSpawner.buttonSpacing ) ) + transform.position.x;

            targetPos.y = linSpawner.direction.y * ( ( i + linSpawner.buttonNumOffset ) * ( buttonRect.sizeDelta.y + linSpawner.buttonSpacing ) ) + transform.position.y;

            targetPos.z = 0;

            buttonRect.position = Vector3.Lerp ( buttonRect.position, targetPos, revealSettings.translateSmooth * Time.fixedDeltaTime );
            }
        }
    private void RevealLinearlyFade ( )
        {
        for ( int i = 0 ; i < buttons.Count ; i++ )
            {
            //-- Target to move towards.
            Vector3 targetPos;
            RectTransform buttonRect = buttons [ i ].GetComponent<RectTransform> ( );
            //-- Sets size.
            buttonRect.sizeDelta = new Vector2
                ( buttonScaler.newButtonSize.x, buttonScaler.newButtonSize.y );
            //-- Sets the position towards the target.
            targetPos.x = linSpawner.direction.x * ( ( i + linSpawner.buttonNumOffset ) * ( buttonRect.sizeDelta.x + linSpawner.buttonSpacing ) ) + transform.position.x;

            targetPos.y = linSpawner.direction.y * ( ( i + linSpawner.buttonNumOffset ) * ( buttonRect.sizeDelta.y + linSpawner.buttonSpacing ) ) + transform.position.y;

            targetPos.z = 0;
            ButtonFader previousButtonFader;
            if ( i > 0 )
                {
                previousButtonFader = buttons [ i ].GetComponent<ButtonFader> ( );
                }
            else
                {
                previousButtonFader = null;
                ButtonFader buttonFader = buttons [ i ].GetComponent<ButtonFader> ( );


                if ( previousButtonFader.faded )
                    {
                    buttons [ i ].transform.position = targetPos;
                    if ( buttonFader )
                        {
                        buttonFader.Fade ( revealSettings.fadeSmooth );
                        }
                    else
                        {
                        Debug.LogError ( "You need to have a buttonFader script attached." );
                        }
                    }
                else
                    {
                    buttons [ i ].transform.position = targetPos;
                    if ( buttonFader )
                        {
                        buttonFader.Fade ( revealSettings.fadeSmooth );
                        }
                    else
                        {
                        Debug.LogError ( "You need to have a buttonfader script attached." );
                        }
                    }
                }
            }
        }

    /// <summary>
    /// Clears all branchers tagged as ButtonBrancher.
    /// </summary>
    private void ClearCommonButtonBranchers ( )
        {
        GameObject [ ] branchers = GameObject.FindGameObjectsWithTag ( "ButtonBrancher" );
        foreach ( GameObject brancher in branchers )
            {
            if ( brancher.transform.parent == transform.parent )
                {
                ButtonBrancher buttonBrancher = brancher.GetComponent<ButtonBrancher> ( );
                for ( int i = buttonBrancher.buttons.Count - 1 ; i >= 0 ; i-- )
                    {
                    Destroy ( buttonBrancher.buttons [ i ] );
                    buttonBrancher.buttons.Clear ( );
                    }
                }
            }
        }

    private void OnLevelWasLoaded ( int level )
        {
        if ( level == 1 )
            {
            DespawnButtons ( );
            }
        else if ( level == 2 )
            {
            DespawnButtons ( );
            }
        else if ( level == 3 )
            {
            DespawnButtons ( );
            }
        }

    private void DespawnButtons ( )
        {
        revealSettings.opening = false;
        revealSettings.spawned = false;
        for ( int i = buttons.Count - 1 ; i >= 0 ; i-- )

            Destroy ( buttons [ i ] );
        buttons.Clear ( );

        ClearCommonButtonBranchers ( );
        }
    }
