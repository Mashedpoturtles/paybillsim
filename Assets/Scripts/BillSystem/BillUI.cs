using UnityEngine;
using Assets.BillSystem;
using System;
using System.Globalization;
using System.Threading;

/// <summary>
/// This class handles adding the UI information onto bill objects.
/// </summary>
public class BillUI : MonoBehaviour
    {
    public TextMesh InformationTextLabel;
    public Bill bill;
    public string IssueDate;
    public string DueDate;
    public string RecievedCost;
    public string Cost;
    public string Type;
    /// <summary>
    /// This method sets the UI for billprefabs when they are instantiated.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="bill"></param>
    public void SetUI ( BillManager manager, Bill bill )
        {
        // Sets the CurrentCulture property to dutch
        CultureInfo ci = new CultureInfo ( "nl-NL" );

        Type = Enum.GetName ( typeof ( BillType ), bill.Type );
        IssueDate = bill.IssueDate.ToString ( "d MMMM yyyy", ci );
        DueDate = bill.DueDate.ToString ( "d MMMM yyyy", ci );
        Cost = bill.Cost.ToString ( );
        RecievedCost = bill.RecievedCost.ToString ( );

        // Sets the CurrentCulture property to dutch
        Thread.CurrentThread.CurrentCulture = new CultureInfo ( "nl-NL" );
        this.InformationTextLabel.text = string.Format ( " Rekening: {0}\\n Datum:\\n {1}\\n Opleverings Datum:\\n {2}\\n Te betalen: {3}\\n",
            Type,
            IssueDate,
            DueDate,
            Cost
            ).Replace ( "\\n", "\n" );
        }
    /// <summary>
    /// This method replaces the text on an instantiated bill with new information.
    /// </summary>
    /// <param name="bill"></param>
    public void ReplaceInfo ( Bill bill )
        {
        // Sets the CurrentCulture property to dutch
        CultureInfo ci = new CultureInfo ( "nl-NL" );

        Type = Enum.GetName ( typeof ( BillType ), bill.Type );
        IssueDate = bill.IssueDate.ToString ( "d MMMM yyyy", ci );
        DueDate = bill.DueDate.ToString ( "d MMMM yyyy", ci );
        Cost = bill.Cost.ToString ( );
        RecievedCost = bill.RecievedCost.ToString ( );

        this.InformationTextLabel.text = string.Format ( " Rekening: {0}\\n Datum:\\n {1}\\n Opleverings Datum:\\n {2}\\n Te betalen: {3}\\n",
            Type,
            IssueDate,
            DueDate,
            Cost
            ).Replace ( "\\n", "\n" );
        }
    public void ReplaceInfoToEventNegative ( Bill bill )
        {
        // Sets the CurrentCulture property to dutch
        CultureInfo ci = new CultureInfo ( "nl-NL" );

        Type = Enum.GetName ( typeof ( BillType ), bill.Type );
        IssueDate = bill.IssueDate.ToString ( "d MMMM yyyy", ci );
        DueDate = bill.DueDate.ToString ( "d MMMM yyyy", ci );
        Cost = bill.Cost.ToString ( );
        RecievedCost = bill.RecievedCost.ToString ( );

        Thread.CurrentThread.CurrentCulture = new CultureInfo ( "nl-NL" );
        this.InformationTextLabel.text = string.Format ( " {0}\\n Vergissing van de bank\\n Datum:\\n {1}\\n Opleverings Datum:\\n {2}\\n Te betalen: {3}\\n ",
            Type,
            IssueDate,
            DueDate,
            Cost
            ).Replace ( "\\n", "\n" );
        }
    public void ReplaceInfoToEventPositive ( Bill bill )
        {
        // Sets the CurrentCulture property to dutch
        CultureInfo ci = new CultureInfo ( "nl-NL" );

        Type = Enum.GetName ( typeof ( BillType ), bill.Type );
        IssueDate = bill.IssueDate.ToString ( "d MMMM yyyy", ci );
        DueDate = bill.DueDate.ToString ( "d MMMM yyyy", ci );
        Cost = bill.Cost.ToString ( );
        RecievedCost = bill.RecievedCost.ToString ( );


        Thread.CurrentThread.CurrentCulture = new CultureInfo ( "nl-NL" );
        this.InformationTextLabel.text = string.Format ( " {0}\\n Vergissing van de bank\\n Datum:\\n {1}\\n Je krijgt: {2}\\n",
            Type,
            IssueDate,
            RecievedCost
            ).Replace ( "\\n", "\n" );
        }

    public void AddWarning ( Bill bill )
        {
        GlobalAudio.instance.SoundWarning ( );
        }
    }

