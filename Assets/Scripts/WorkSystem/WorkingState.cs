using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorkingState : MonoBehaviour
{
    private Text workStateText;
    private Slider workSlider;
    [SerializeField]
    private float workIntensity;
    [SerializeField]
    private float workEnergy;
    [SerializeField]
    public WorkState currentState;
    public enum WorkState { NotWorking, Average, Hard, OverDrive, Drained }

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
        workEnergy = 300;
        Invoke("Energy", 1);
    }

    private void ChangeSliderValue()
    {
        workIntensity = workSlider.value;

        if (workIntensity <= 0.3f)
        {
            ChangeState(WorkState.NotWorking);
        }
        else if (workIntensity >= 0.3f && workIntensity <= 0.5f)
        {
            ChangeState(WorkState.Average);
        }
        else if (workIntensity >= 0.5f && workIntensity <= 0.8f)
        {
            ChangeState(WorkState.Hard);
        }
        else if (workIntensity >= 0.8f)
        {
            ChangeState(WorkState.OverDrive);
        }
    }
    private int AddMoney(int money)
    {
        return money;
    }

    IEnumerator NotWorkingState()
    {
        while (currentState == WorkState.NotWorking)
        {
            workStateText.text = "working pace:" + currentState;
            yield return new WaitForSeconds(3);
        }
        yield return null;
    }

    IEnumerator AverageState()
    {
        while (currentState == WorkState.Average)
        {
            workStateText.text = "working pace:" + currentState;
            workEnergy -= 3;
            Money.instance.currentMoney += AddMoney(10);
            yield return new WaitForSeconds(3);
        }
        yield return null;
    }

    IEnumerator HardState()
    {
        while (currentState == WorkState.Hard)
        {
            workStateText.text = "working pace:" + currentState;
            workEnergy -= 6;
            Money.instance.currentMoney += AddMoney(20);
            yield return new WaitForSeconds(3);
        }
        yield return null;
    }

    IEnumerator OverDriveState()
    {
        while (currentState == WorkState.OverDrive)
        {
            workStateText.text = "working pace:" + currentState;
            workEnergy -= 12;
            Money.instance.currentMoney += AddMoney(30);
            yield return new WaitForSeconds(3);
        }
        yield return null;
    }

    IEnumerator DrainedState()
    {
        while (currentState == WorkState.Drained)
        {
            workStateText.text = "working pace:" + currentState;
            workSlider.maxValue = 0.4f;
            yield return new WaitForSeconds(10);
        }
        yield return null;
    }

    /// <summary>
    /// Coroutine names must match the enum statenames exactly followed by "State"
    /// </summary>
    /// <param name="newstate"></param>
    private void ChangeState(WorkState newstate)
    {
        currentState = newstate;
        StartCoroutine(newstate.ToString() + "State");
    }

    private void Energy()
    {
        if (workEnergy <= 0)
        {
            ChangeState(WorkState.Drained);
            CancelInvoke("Energy");
        }
        else if (workEnergy >= 0)
        {
            Invoke("Energy", 1);
        }
    }
}

