
using System;

public class Bill : IBill
{
    public static int numberOfBills;
    private int amount;
    private DateTime issue;
    private DateTime due;
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
    public DateTime DueDate
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
    public DateTime IssueDate
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

 