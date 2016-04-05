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
    public enum BillType
        {
        Electricity,
        Internet,
        }
    }