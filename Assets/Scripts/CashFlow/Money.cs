using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private Text CashInfo;
    private double currentMoney;


    private void StartingValue()
    {
        currentMoney = 0;
    }

    private void Start()
    {
        CashInfo = GameObject.FindWithTag("Cash").GetComponent<Text>();
        StartingValue();
    }

    private void Update()
    {

    }
}
