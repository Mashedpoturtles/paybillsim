using UnityEngine;
using UnityEngine.UI;
using Assets.BillSystem;
using System;
/// <summary>
/// This class handles adding the UI information onto bill objects.
/// </summary>
public class BillUI : MonoBehaviour
    {
    [HideInInspector]
    public Text informationTextLabel;
    [SerializeField]
    private Button payButton;
    public Bill bill;

    /// <summary>
    /// This method sets the UI for billprefabs when they are instantiated.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="bill"></param>
    public void SetUI ( BillManager manager, Bill bill )
        {
        payButton.onClick.AddListener ( ( ) => manager.PayBill ( bill ) );
        this.informationTextLabel.text = string.Format ( "Bill type: {0} \\n Issue date: {1} \\n  Due date: {2} \\n Amount to pay: {3} \\n",
                                    Enum.GetName ( typeof ( BillType ), bill.Type ),
                                    bill.IssueDate.ToString ( "d" ),
                                    bill.DueDate.ToString ( "d" ),
                                    bill.Cost ).Replace ( "\\n", "\n" );
        }
    /// <summary>
    /// This method replaces the text on an instantiated bill with new information.
    /// </summary>
    /// <param name="bill"></param>
    public void ReplaceInfo ( Bill bill )
        {
        this.informationTextLabel.text = string.Format ( "Bill type: {0} \\n Issue date: {1} \\n  Due date: {2} \\n Amount to pay: {3} \\n",
                               Enum.GetName ( typeof ( BillType ), bill.Type ),
                               bill.IssueDate.ToString ( "d" ),
                               bill.DueDate.ToString ( "d" ),
                               bill.Cost ).Replace ( "\\n", "\n" );
        }
    }

