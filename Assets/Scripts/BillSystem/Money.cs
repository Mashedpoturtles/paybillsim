using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
    {
    [SerializeField]
    private Text cashInfo;
    [SerializeField]
    private Slider moneySlider;
    [SerializeField]
    public int currentMoney;
    public static Money instance;


    public void Start ( )
        {
        instance = this;
        cashInfo = GameObject.FindWithTag ( "Cash" ).GetComponent<Text> ( );
        moneySlider.maxValue = 10000;
        }

    private void Update ( )
        {
        cashInfo.text = "Geld: " + currentMoney;
        moneySlider.value = currentMoney;
        }
    }
