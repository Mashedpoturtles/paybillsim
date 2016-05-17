using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {

    [SerializeField]
    private GameObject easy;
    [SerializeField]
    private GameObject normal;
    [SerializeField]
    private GameObject hard;
    [SerializeField]
    private GameObject continueGame;
    [SerializeField]
    private GameObject quit;
    [SerializeField]
    private GameObject back;
    [SerializeField]
    private Text textDisplay;

    /// <summary>
    /// Cache the textDisplay component.
    /// </summary>
    private void Start ( )
        {
        if ( textDisplay == null )
            {
            textDisplay = GameObject.FindWithTag ( "DifficultyText" ).GetComponent<Text> ( );
            return;
            }
        }
    /// <summary>
    /// Raycast buttonhover to display text.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter ( PointerEventData eventData )
        {
        if ( easy != null )
            {
            if ( gameObject == easy )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 10";
                return;
                }
            return;
            }
        if ( normal != null )
            {
            if ( gameObject == normal )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 30";
                return;
                }
            return;
            }
        if ( hard != null )
            {
            if ( gameObject == hard )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 50";
                return;
                }
            return;
            }

        if ( continueGame != null )
            {
            if ( gameObject == continueGame )
                {
                textDisplay.text = "Ga verder waar je als laatst bent gebleven.";
                return;
                }
            return;
            }
        if ( back != null )
            {
            if ( gameObject == back )
                {
                textDisplay.text = "Keer terug naar het start scherm.";
                }
            }
        if ( quit != null )
            {
            if ( gameObject == quit )
                {
                textDisplay.text = "Sluit het spel af en keer terug naar desktop.";
                return;
                }
            return;
            }
        }

    public void OnPointerExit ( PointerEventData eventData )
        {
        textDisplay.text = "";
        }

    /// <summary>
    /// Check for clicks and switch difficulty.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown ( PointerEventData eventData )
        {
        if ( gameObject == easy )
            {
            GameManager.Instance.IsEasy = true;
            GameManager.Instance.IsNormal = false;
            GameManager.Instance.IsHard = false;
            return;
            }
        else if ( gameObject == normal )
            {
            GameManager.Instance.IsNormal = true;
            GameManager.Instance.IsEasy = false;
            GameManager.Instance.IsHard = false;
            return;
            }
        else if ( gameObject == hard )
            {
            GameManager.Instance.IsHard = true;
            GameManager.Instance.IsEasy = false;
            GameManager.Instance.IsNormal = false;
            return;
            }
        }
    }
