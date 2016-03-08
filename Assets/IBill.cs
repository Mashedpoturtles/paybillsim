
using System;

interface IBill  
{
    string BillType
    {
        get;
        set;
    }
    int Amount
    {
        get;
        set;
    }
    DateTime DueDate
    {
        get;
        set;
    }
    DateTime IssueDate
    {
        get;
        set;
    }

    int Counter
    {
        get;
    }
}
