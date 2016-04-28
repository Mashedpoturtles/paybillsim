using UnityEngine;
using UnityEngine.UI;
using Assets.BillSystem;
using System;

/// <summary>
/// This class handles adding the UI information onto bill objects.
/// </summary>
public class BillUI : MonoBehaviour
    {
    public TextMesh InformationTextLabel;
    public Bill bill;

    /// <summary>
    /// This method sets the UI for billprefabs when they are instantiated.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="bill"></param>
    public void SetUI ( BillManager manager, Bill bill )
        {
        this.InformationTextLabel.text = string.Format ( " Rekening: {0}\\n Datum: {1}\\n Opleverings Datum:\\n {2}\\n Te betalen: {3}\\n",
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
        this.InformationTextLabel.text = string.Format ( " Rekening: {0}\\n Datum: {1}\\n  Opleverings Datum: \\n {2}\\n Te betalen: {3}\\n",
                                Enum.GetName ( typeof ( BillType ), bill.Type ),
                                bill.IssueDate.ToString ( "d" ),
                                bill.DueDate.ToString ( "d" ),
                                bill.Cost ).Replace ( "\\n", "\n" );
        }
    public void AddWarning ( Bill bill )
        {
        GlobalAudio.instance.SoundWarning ( );
        bill.Object.transform.GetChild ( 1 ).GetComponent<SpriteRenderer> ( ).enabled = true;
        }
    }

