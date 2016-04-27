using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

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
    private Text textDisplay;

    private void Start()
        {
        if(textDisplay == null)
            {
            textDisplay = GameObject.FindWithTag ( "DifficultyText" ).GetComponent<Text> ( );
            }
        }

    public void Easy()
        {
        TimeSpeed = 100f;
        }

    public void Normal()
        {
        TimeSpeed = 200f;
        }

    public void Hard()
        {
        TimeSpeed = 500f;
        }

    public void OnPointerEnter ( PointerEventData eventData )
        {
            if ( gameObject == easy )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 100";
                return;
                }
            else if ( gameObject == normal )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 200";
                return;
                }
            else if ( gameObject == hard )
                {
                textDisplay.text = "Moeilijkheidsgraad : Tijd x 500";
                return;
                }
            else if ( gameObject == continueGame )
                {
                textDisplay.text = "Ga verder waar je bent gebleven.";
                return;
                }
            else if ( gameObject == quit )
                {
                textDisplay.text = "Sluit het spel af en keer terug naar desktop.";
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
