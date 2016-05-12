using UnityEngine;
using UnityEngine.UI;
using Assets.BillSystem;

public class PhoneButtonController : MonoBehaviour
    {
    [SerializeField]
    private Slider paymentPlanSlider;
    [SerializeField]
    private GameObject Slider;

    public void OnClick ( )
        {
        Slider.SetActive ( true );
        }

    }


