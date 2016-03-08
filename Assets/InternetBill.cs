using UnityEngine;
using UnityEngine.UI;

public class InternetBill : MonoBehaviour {

    public Text BillInformation;
    public bool Exists;
    public static InternetBill instance;

    void Start()
    {
        instance = this;
    }

    public void Internet()
    {
       Bill internet = new Bill();
       internet.BillType = "Internet";
       internet.DueDate = TimeManager.currentTime.AddDays(32);
       internet.IssueDate = TimeManager.currentTime;
       internet.Amount = 500;

        BillInformation.text = string.Format("Bill number: {0} Bill type: {1} Due date: {2} Issue date: {3} Amount to pay: {4}",
            internet.Counter, internet.BillType, 
            internet.DueDate.ToString("d"), internet.IssueDate.ToString("d"), internet.Amount);

        // if an instance of this bill already exists then it is not null so exists is set to true.
        if (internet != null)
        {
            Exists = true;
        }
    }
}
