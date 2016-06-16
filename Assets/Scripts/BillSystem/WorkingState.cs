using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//todo: refactor this whole coroutine statemachine.
public class WorkingState : MonoBehaviour
    {
    private Slider workSlider;
    [SerializeField]
    private BillManager billManager;
    [SerializeField]
    private Slider workingProgressSlider;
    [SerializeField]
    private Text energy;
    [SerializeField]
    private int workEnergy;
    [SerializeField]
    private float workSpeed;
    [SerializeField]
    public WorkState currentState;
    public enum WorkState { NotWorking, Normal, Hard, Workaholic, Drained }
    public Canvas canvas;
    float timer;

    private void Start ( )
        {
        UI ( );
        SetWorkState ( WorkState.NotWorking );
        workEnergy = 300;
        Invoke ( "Energy", 1 );
        workingProgressSlider.interactable = false;
        workingProgressSlider.maxValue = 300;
        workingProgressSlider.minValue = 0;
        }

    private void Update ( )
        {
        workingProgressSlider.value = workEnergy;

        if ( workEnergy <= 0 )
            {
            energy.text = "Energy: 0";
            }
        else
            {
            energy.text = "Energy: " + workEnergy;
            }
        WarningLowEnergy ( );
        }

    private void WarningLowEnergy ( )
        {
        if ( workEnergy > 300 )
            {
            workEnergy = 300;
            }
        }

    private void SetWorkState ( int workIntensity )
        {
        WorkState newWorkState;

        switch ( workIntensity )
            {
            case 0:
                newWorkState = WorkState.Drained;
                workEnergy = 0;
                break;

            case 1:
                newWorkState = WorkState.NotWorking;
                break;

            case 2:
                newWorkState = WorkState.Normal;
                break;

            case 3:
                newWorkState = WorkState.Hard;
                break;

            case 4:
                newWorkState = WorkState.Workaholic;
                break;

            default:
                newWorkState = WorkState.NotWorking;
                break;
            }
        currentState = newWorkState;
        StartCoroutine ( newWorkState.ToString ( ) + "State" );
        }

    private void SetWorkState ( WorkState newWorkState )
        {
        currentState = newWorkState;
        StartCoroutine ( newWorkState.ToString ( ) + "State" );
        }

    private int AddMoney ( int money )
        {
        return money;
        }

    IEnumerator NotWorkingState ( )
        {
        while ( currentState == WorkState.NotWorking )
            {
            yield return new WaitForSeconds ( 3 );
            if ( GameManager.Instance.IsPaused == false )
                {
                if ( workEnergy < 300 )
                    {
                    workEnergy += 4;
                    }
                }
            }
        yield return null;
        }

    IEnumerator NormalState ( )
        {
        while ( currentState == WorkState.Normal )
            {
            yield return new WaitForSeconds ( 1.5f );
            if ( GameManager.Instance.IsPaused == false )
                {
                if ( workEnergy < 300 )
                    {
                    workEnergy += 4;
                    }
                Money.instance.currentMoney += AddMoney ( 50 );
                }
            }
        yield return null;
        }

    IEnumerator HardState ( )
        {
        while ( currentState == WorkState.Hard )
            {
            yield return new WaitForSeconds ( 2 );
            if ( GameManager.Instance.IsPaused == false )
                {
                if ( workEnergy > 0 )
                    {
                    workEnergy -= 6;
                    }
                Money.instance.currentMoney += AddMoney ( 75 );
                }
            }
        yield return null;
        }

    IEnumerator WorkaholicState ( )
        {
        while ( currentState == WorkState.Workaholic )
            {
            yield return new WaitForSeconds ( 2 );
            if ( GameManager.Instance.IsPaused == false )
                {
                if ( workEnergy > 0 )
                    {
                    workEnergy -= 12;
                    }
                Money.instance.currentMoney += AddMoney ( 95 );
                }
            }
        yield return null;
        }

    IEnumerator DrainedState ( )
        {
        while ( currentState == WorkState.Drained )
            {
            workSlider.interactable = false;
            yield return null;
            }
        workSlider.interactable = true;
        yield return null;
        }

    private void Energy ( )
        {
        if ( workEnergy <= 0 )
            {
            SetWorkState ( WorkState.Drained );
            CancelInvoke ( "Energy" );
            workEnergy = 0;
            GlobalAudio.instance.SoundGameOver ( );
            LoadLevelsOnClick.instance.LoadMenuScene ( );
            return;
            }
        else if ( workEnergy >= 1 )
            {
            Invoke ( "Energy", 1 );
            return;
            }
        }

    private void UI ( )
        {
        workSlider = GameObject.FindWithTag ( "WorkSlider" ).GetComponent<Slider> ( );
        workingProgressSlider = GameObject.FindWithTag ( "WorkingProgressBar" ).GetComponent<Slider> ( );
        workSlider.onValueChanged.AddListener ( delegate { SetWorkState ( ( int ) workSlider.value ); } );
        }
    }
