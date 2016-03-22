using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorkingState : MonoBehaviour
{
    private Text workStateText;
    private Slider workSlider;
    private float WorkIntensity;
    [SerializeField]
    public WorkState currentState;
    public enum WorkState { NotWorking, Average, Hard, OverDrive }

    private void WorkStateSliderController()
    {
        workSlider.onValueChanged.AddListener(delegate { ChangeSliderValue(); });
    }

    private void Start()
    {
        workSlider = GameObject.FindWithTag("WorkSlider").GetComponent<Slider>();
        workStateText = workSlider.GetComponentInChildren<Text>();
        WorkStateSliderController();
        ChangeState(WorkState.NotWorking);
    }

    private void ChangeSliderValue()
    {
        WorkIntensity = workSlider.value;

        if (WorkIntensity <= 0.3f)
        {
            ChangeState(WorkState.NotWorking);
        }
        else if (WorkIntensity >= 0.3f && WorkIntensity <= 0.5f)
        {
            ChangeState(WorkState.Average);
        }
        else if (WorkIntensity >= 0.5f && WorkIntensity <= 0.8f)
        {
            ChangeState(WorkState.Hard);
        }
        else if (WorkIntensity >= 0.8f)
        {
            ChangeState(WorkState.OverDrive);
        }
    }

    IEnumerator NotWorkingState()
    {
        while (currentState == WorkState.NotWorking)
        {
            workStateText.text = "working pace:" + currentState;
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator AverageState()
    {
        while (currentState == WorkState.Average)
        {
            workStateText.text = "working pace:" + currentState;
            Money.currentMoney += 10;
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator HardState()
    {
        while (currentState == WorkState.Hard)
        {
            workStateText.text = "working pace:" + currentState;
            Money.currentMoney += 20;
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator OverDriveState()
    {
        while (currentState == WorkState.OverDrive)
        {
            workStateText.text = "working pace:" + currentState;
            Money.currentMoney += 30;
            yield return new WaitForSeconds(3);
        }
    }
    /// <summary>
    /// Coroutine names must match the enum statenames exactly followed by "State"
    /// </summary>
    /// <param name="newstate"></param>
    public void ChangeState(WorkState newstate)
    {
        currentState = newstate;
        StartCoroutine(newstate.ToString() + "State");
    }
}

