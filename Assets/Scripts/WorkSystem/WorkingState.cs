using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorkingState : MonoBehaviour
{
    private Text workStateText;
    private Slider workSlider;
    private float WorkIntensity;
    private float WorkEnergy;
    [SerializeField]
    public WorkState currentState;
    public enum WorkState { NotWorking, Average, Hard, OverDrive }

    private void ChangeWorkState()
    {
        workSlider.onValueChanged.AddListener(delegate { ChangeSliderValue(); });
    }

    private void Start()
    {
        workSlider = GameObject.FindWithTag("WorkSlider").GetComponent<Slider>();
        workStateText = workSlider.GetComponentInChildren<Text>();
        ChangeWorkState();
        ChangeState(WorkState.NotWorking);
    }

    private void ChangeSliderValue()
    {
        WorkIntensity = workSlider.value;

        if (WorkIntensity <= 0.3f)
        {
            NotWorking();
        }
        else if (WorkIntensity >= 0.3f && WorkIntensity <= 0.5f)
        {
            WorkingAverage();
        }
        else if (WorkIntensity >= 0.5f && WorkIntensity <= 0.8f)
        {
            WorkingHard();
        }
        else if (WorkIntensity >= 0.8f)
        {
            WorkingOverdrive();
        }
    }

    IEnumerator WorkNoState()
    {
        while (currentState == WorkState.NotWorking)
        {
            yield return new WaitForSeconds(3);
            workStateText.text = "working pace:" + currentState;
            Money.currentMoney += 0;
        }
    }

    IEnumerator WorkAverageState()
    {
        while (currentState == WorkState.Average)
        {
            yield return new WaitForSeconds(3);
            workStateText.text = "working pace:" + currentState;
            Money.currentMoney += 10;
        }
    }

    IEnumerator WorkHardState()
    {
        while (currentState == WorkState.Hard)
        {
            yield return new WaitForSeconds(3);
            workStateText.text = "working pace:" + currentState;
            Money.currentMoney += 20;
        }
    }
    IEnumerator WorkOverdriveState()
    {
        yield return new WaitForSeconds(3);
        workStateText.text = "working pace:" + currentState;
        Money.currentMoney += 30;
    }

    private void ChangeState(WorkState newstate)
    {
        currentState = newstate;
        StartCoroutine(newstate.ToString() + "State");
    }

    public void NotWorking()
    {
        ChangeState(WorkState.NotWorking);
    }

    private void WorkingAverage()
    {
        ChangeState(WorkState.Average);
    }

    private void WorkingHard()
    {
        ChangeState(WorkState.Hard);
    }

    private void WorkingOverdrive()
    {
        ChangeState(WorkState.OverDrive);
    }
}

