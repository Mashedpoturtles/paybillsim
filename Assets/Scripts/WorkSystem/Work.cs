using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Work : MonoBehaviour
{
    private Slider workSlider;
    private float WorkIntensity;

    private void ChangeWorkState()
    {
        workSlider.onValueChanged.AddListener(delegate { ChangeValue(); });
    }

    private void ChangeValue()
    {
        WorkIntensity = workSlider.value;

        if (WorkIntensity <= 0.4f)
        {
            StartCoroutine(NoWork());
        }
        else if (WorkIntensity >= 0.4f && WorkIntensity <= 0.8f)
        {
            StartCoroutine(WorkAverage());
        }
        else if (WorkIntensity >= 0.8f && WorkIntensity <= 0.9f)
        {
            StartCoroutine(WorkHard());
        }
        else if (WorkIntensity >= 0.9f)
        {
            StartCoroutine(OverDrive());
        }
    }

    private IEnumerator NoWork()
    {
        Debug.Log("not working");
        while (true)
        {
            yield return new WaitForSeconds(3);
            Money.currentMoney += 0;
            Debug.Log("0 money earned");
        }
    }

    private IEnumerator WorkAverage()
    {
        Debug.Log("Working at average pace");
       while(true)
        {
            yield return new WaitForSeconds(3);
            Money.currentMoney += 10;
            Debug.Log("10 money earned");
        }
    }

    private IEnumerator WorkHard()
    {
        Debug.Log("working at a hard pace");
       while(true)
        {
            yield return new WaitForSeconds(3);
            Money.currentMoney += 20;
            Debug.Log("20 money earned");
        }
    }

    private IEnumerator OverDrive()
    {
        Debug.Log("Youre a maniac");
       while(true)
        {
            yield return new WaitForSeconds(3);
            Money.currentMoney += 30;
            Debug.Log("30 money earned");
        }
    }

    private void Start()
    {
        workSlider = GameObject.FindWithTag("WorkSlider").GetComponent<Slider>();
        ChangeWorkState();
    }
}
