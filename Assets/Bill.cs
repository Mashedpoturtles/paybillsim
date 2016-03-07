
public class Bill : IBill
{
    public static int numberOfBills;
    private int amount;
    private int issue;
    private int due;
    private string name;
    public string BillType  // read-write instance property
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public int DueDate
    {
        get
        {
            return due;
        }
        set
        {
            due = value;
        }
    }
    public int IssueDate
    {
        get
        {
            return issue;
        }
        set
        {
            issue = value;
        }
    }
    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            amount = value;
        }
    }
       
    private static int counter;
    public int Counter  // read-only instance property
    {
        get
        {
            return counter;
        }
    }

    public Bill()  // constructor
    {
        counter++;
    }
}

 