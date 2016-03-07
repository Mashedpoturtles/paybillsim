
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
    int DueDate
    {
        get;
        set;
    }
    int IssueDate
    {
        get;
        set;
    }

    int Counter
    {
        get;
    }
}
