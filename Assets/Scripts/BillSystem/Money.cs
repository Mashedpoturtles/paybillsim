using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField]
    private Text CashInfo;
    [SerializeField]
    public int currentMoney;

    public static Money instance;

    [SerializeField]
    public void Initialize()
    {
        instance = this;
        CashInfo = GameObject.FindWithTag("Cash").GetComponent<Text>();
    }

    public void Start()
    {
        Initialize();
    }

    private void Update()
    {
        CashInfo.text = currentMoney.ToString();
    }
}
