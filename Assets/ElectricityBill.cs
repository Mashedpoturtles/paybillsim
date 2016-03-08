using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ElectricityBill : MonoBehaviour {
    public Text BillInformation;
    public bool Exists;
    public static ElectricityBill instance;
    public void Electricity()
    {
        Bill electricity = new Bill();     
        electricity.BillType = "Electricity";
        electricity.DueDate = TimeManager.instance.curentTime;
        electricity.IssueDate = TimeManager.instance.curentTime;
        electricity.Amount = 500;

        BillInformation.text = string.Format("Bill number: {0} Bill type: {1} Due date: {2} Issue date: {3} Amount to pay: {4}",
            electricity.Counter, electricity.BillType, electricity.DueDate, electricity.IssueDate, electricity.Amount);

        // if an instance of this bill already exists then it is not null so exists is set to true.
        if (electricity != null)
        {
           Exists = true;
        }
    }
    void Start()
    {
        instance = this;
    }
}


 