using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Debt : MonoBehaviour
    {
    private Text debtInfo;
    [SerializeField]
    private Slider debtSlider;
    [SerializeField]
    public int currentDebt;
    public static Debt instance;

    public void Start ( )
        {
        instance = this;
        debtInfo = GameObject.FindWithTag ( "Debt" ).GetComponent<Text> ( );
        debtSlider.maxValue = 10000;
        debtSlider.minValue = 0;
        }

    private void Update ( )
        {
        debtInfo.text = "Schuld: " + currentDebt;
        debtSlider.value = currentDebt;
        }
    }
