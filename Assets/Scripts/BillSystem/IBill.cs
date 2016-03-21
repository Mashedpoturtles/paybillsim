using System;

namespace Assets.BillSystem
{
    internal interface IBill
    {
        BillType Type { get; set; }
        int Amount { get; set; }
        int Counter { get; }
        DateTime DueDate { get; set; }
        DateTime IssueDate { get; set; }


    }
    /// <summary>
    /// For e.g. electricity , internet, water or gas bill.
    /// </summary>
    public enum BillType
    {
        Electricity = 0,
        Internet,
    }
}