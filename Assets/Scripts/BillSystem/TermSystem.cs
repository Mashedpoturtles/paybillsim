using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TermSystem : MonoBehaviour
    {
    public bool paymentInTerms;
    public static TermSystem instance;
    public Slider sliderPaymentSplitter;
    public int Terms;
    void Start ( )
        {
        instance = this;
        }

    // Update is called once per frame
    void Update ( )
        {
        Terms = ( int ) sliderPaymentSplitter.value;
        }
    }
