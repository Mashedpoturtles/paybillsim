using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BillTypes : MonoBehaviour {

    public Text BillInformation;
    public static BillTypes instance;
    public bool Exists;

    void Start()
    {
        instance = this;
    }
 
   public void ElectricityBill()
    {
        Bill electricity = new Bill();     
        
        electricity.BillType = "Electricity";
        electricity.DueDate = 10;
        electricity.IssueDate = 
        electricity.Amount = 500;

        BillInformation.text = string.Format("Bill number: {0} Bill type: {1} Due date: {2} Issue date: {3} Amount to pay: {4}",
            electricity.Counter, electricity.BillType, electricity.DueDate, electricity.IssueDate, electricity.Amount); 
        
        if(electricity != null)
        {
            Exists = false;
        } 
    }

   public void InternetBill()
    {
        Bill internet = new Bill();

        BillInformation.text = string.Format("Bill number: {0} Bill type: {1} Due date: {2} Issue date: {3} Amount to pay: {4}",
            internet.Counter, internet.BillType, internet.DueDate, internet.IssueDate, internet.Amount);
    }
}
