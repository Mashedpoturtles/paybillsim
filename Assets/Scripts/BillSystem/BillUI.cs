using UnityEngine;
using UnityEngine.UI;
using Assets.BillSystem;
using System;

public class BillUI : MonoBehaviour
    {
    [SerializeField]
    private Text informationTextLabel;
    [SerializeField]
    private Button payButton;

    public void SetInfo ( BillManager manager, Bill bill )
        {
        payButton.onClick.AddListener ( ( ) => manager.PayBill ( bill ) );

        this.informationTextLabel.text = string.Format ( "Bill type: {0} \\n Issue date: {1} \\n  Due date: {2} \\n Amount to pay: {3} \\n",
                                    Enum.GetName ( typeof ( BillType ), bill.Type ),
                                    bill.IssueDate.ToString ( "d" ),
                                    bill.DueDate.ToString ( "d" ),
                                    bill.Amount ).Replace ( "\\n", "\n" );
        }
    }