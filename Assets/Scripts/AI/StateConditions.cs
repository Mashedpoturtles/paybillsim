using UnityEngine;
using System.Collections;

public class StateConditions : MonoBehaviour
    {
    public int random_waitTimeHunger;
    public int random_waitTimeThirst;
    public bool _hungry;
    public bool _thirsty;
    public float currentHunger;
    public static float maxHunger = 20;
    public float triggerEating = 10;
    public float currentThirst;
    public float triggerDrinking = 30;
    public static float maxThirst = 40;
    [SerializeField]
    private Pathing path;

    void Start ( )
        {
        InvokeRepeating ( "HungerRegen", 0, 1 );
        InvokeRepeating ( "ThirstRegen", 0, 1 );
        }

    public void HungerRegen ( )
        {
        if ( currentHunger < maxHunger )
            {
            currentHunger++;
            }

        if ( currentHunger == triggerEating )
            {
            _hungry = true;
            path.MoveMultipleRandom ( path.target );
            }
        if ( currentHunger == maxHunger )
            {
            StartCoroutine ( ResetHungerRate ( ) );
            }
        }

    public IEnumerator ResetHungerRate ( )
        {
            {
            if ( currentHunger == maxHunger )
                {
                random_waitTimeHunger = Random.Range ( 20, 30 );
                yield return new WaitForSeconds ( random_waitTimeHunger );
                currentHunger = 0;
                }
            }
        }

    public IEnumerator ResetThirstRate ( )
        {
            {
            if ( currentThirst == maxThirst )
                {
                random_waitTimeThirst = Random.Range ( 25, 35 );
                yield return new WaitForSeconds ( random_waitTimeThirst );
                currentThirst = 0;
                }
            }
        }

    public void ThirstRegen ( )
        {
        if ( currentThirst < maxThirst )
            {
            currentThirst++;
            }

        if ( currentThirst == triggerDrinking )
            {
            _thirsty = true;
            path.MoveMultipleRandom ( path.target );
            }
        if ( currentThirst == maxThirst )
            {
            StartCoroutine ( ResetThirstRate ( ) );
            }
        }

    // 1400 / 40 = 35 weken  - looptijd = 38 weken (  28/09/2015 - 24/06-2016  )
    // Start - eind = 32 weken = 2 weken     
    }