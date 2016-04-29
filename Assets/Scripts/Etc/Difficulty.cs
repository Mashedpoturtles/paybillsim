using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {

    public static float TimeSpeed;
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

    private void Start ( )
        {
        if ( textDisplay == null )
            {
            textDisplay = GameObject.FindWithTag ( "DifficultyText" ).GetComponent<Text> ( );
            return;
            }
        }

    public void Easy ( )
        {
        TimeSpeed = 100f;
        }

    public void Normal ( )
        {
        TimeSpeed = 200f;
        }

    public void Hard ( )
        {
        TimeSpeed = 500f;
        }

    public void OnPointerEnter ( PointerEventData eventData )
        {
        if ( easy != null )
            {
            if ( gameObject == easy )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 100";
                return;
                }
            return;
            }
        if ( normal != null )
            {
            if ( gameObject == normal )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 200";
                return;
                }
            return;
            }
        if ( hard != null )
            {
            if ( gameObject == hard )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 500";
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

    public void OnPointerDown ( PointerEventData eventData )
        {
        if ( gameObject == easy )
            {
            Easy ( );
            return;
            }
        else if ( gameObject == normal )
            {
            Normal ( );
            return;
            }
        else if ( gameObject == hard )
            {
            Hard ( );
            return;
            }
        }
    }
