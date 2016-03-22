using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Work : MonoBehaviour
{
    private Text workStateText;
    private Slider workSlider;
    private float WorkIntensity;
    private float WorkEnergy;

    private void ChangeWorkState()
    {
        workSlider.onValueChanged.AddListener(delegate { ChangeValue(); });
    }

    private void ChangeValue()
    {
        WorkIntensity = workSlider.value;

        if (WorkIntensity <= 0.3f)
        {
            StartCoroutine(NoWork());
        }
        else if (WorkIntensity >= 0.3f && WorkIntensity <= 0.5f)
        {
            StartCoroutine(WorkAverage());
        }
        else if (WorkIntensity >= 0.5f && WorkIntensity <= 0.8f)
        {
            StartCoroutine(WorkHard());
        }
        else if (WorkIntensity >= 0.8f)
        {
            StartCoroutine(OverDrive()); }
        
    }

    private IEnumerator NoWork()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            workStateText.text = "Working pace: Not working";
            Money.currentMoney += 0;
            
        }
    }

    private IEnumerator WorkAverage()
    {
       while(true)
        {
            yield return new WaitForSeconds(3);
            workStateText.text = "Working pace: Average";
            Money.currentMoney += 10;
        }
    }

    private IEnumerator WorkHard()
    {
       while(true)
        {
            yield return new WaitForSeconds(3);
            workStateText.text = "Working pace: Hard";
            Money.currentMoney += 20;
        }
    }

    private IEnumerator OverDrive()
    {
       while(true)
        {
            yield return new WaitForSeconds(3);
            workStateText.text = "working pace: OverDrive";
            Money.currentMoney += 30;
        }
    }

    private void Start()
    {
        workSlider = GameObject.FindWithTag("WorkSlider").GetComponent<Slider>();
        workStateText = workSlider.GetComponentInChildren<Text>();
        ChangeWorkState();
    }
}
