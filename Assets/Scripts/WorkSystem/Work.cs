using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Work : MonoBehaviour {

    private Slider workSlider;
    private float WorkIntensity;
  
    private void ChangeWorkState()
    {
        workSlider.onValueChanged.AddListener(delegate { ChangeValue(); } );
    }

    public void ChangeValue()
    {
        WorkIntensity = workSlider.value;

        if(WorkIntensity <= 0.4f)
        {
            NoWork();
        }
        else if(WorkIntensity >= 0.4f && WorkIntensity <= 0.8f)
        {
            WorkAverage();
        }
        else if(WorkIntensity >= 0.8f && WorkIntensity <= 0.9f)
        {
            WorkHard();
        }
        else if(WorkIntensity >= 0.9f)
        {
            OverDrive();
        }
    }

    private void NoWork()
    {
        Debug.Log("not working");
    }

    private void WorkAverage()
    {
        Debug.Log("Working at average pace");
    }

    private void WorkHard()
    {
        Debug.Log("working at a hard pace");
    }

    private void OverDrive()
    {
        Debug.Log("Youre a maniac");
    }

    private void Start()
    {
        workSlider = GameObject.FindWithTag("WorkSlider").GetComponent<Slider>();
        ChangeWorkState();
    }
}
