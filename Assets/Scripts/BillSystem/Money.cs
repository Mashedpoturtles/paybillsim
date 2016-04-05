using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
    {
    [SerializeField]
    private Text CashInfo;
    [SerializeField]
    public int currentMoney;
    public static Money instance;


    public void Start ( )
        {
        instance = this;
        CashInfo = GameObject.FindWithTag ( "Cash" ).GetComponent<Text> ( );
        }

    private void Update ( )
        {
        CashInfo.text = "Geld: " + currentMoney;
        }
    }
