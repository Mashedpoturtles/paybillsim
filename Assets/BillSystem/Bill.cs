using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{
    public class Bill :  IBill
    {
        /// <summary>
        /// Display the bill information to text components.
        /// </summary>
        /// <summary>
        /// BillType comparrison to string?
        /// </summary>
        public string BillType { get; set; }
        /// <summary>
        /// The amount you must pay for this bill initially.
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// The kind of bill, for e.g. internet, electricity or water bills.
        /// </summary>
        public BillType Type { get; set; }
        /// <summary>
        /// The Date the bill is due for payment.
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// The date the bill is issued.
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// This will add to the total amount of issued bills.
        /// </summary>
        public int Counter { get; set; }
        /// <summary>
        /// Stores the bill information such as the type, issue date, due date and amount to be paid.
        /// </summary>
        /// <param name="type"></param>
        public Bill(BillType type)
        {
            Text BillInformation = GameObject.FindGameObjectWithTag("billInfo").GetComponent<Text>();
            Debug.Log("bill created");
            Type = type;
            DueDate = TimeManager.currentTime.AddDays(1);
            IssueDate = TimeManager.currentTime;
            Amount = 50;

            BillInformation.text = (string.Format("Bill type: {0} Issue date: {1}  Due date: {2} Amount to pay: {3}",
           
                 Enum.GetName(typeof(BillType), type),
                 IssueDate.ToString("d"),
                 DueDate.ToString("d"),
                 Amount));
          Debug.Log(string.Format("Bill type: {0} Issue date: {1}  Due date: {2} Amount to pay: {3}",
           
                 Enum.GetName(typeof(BillType), type),
                 IssueDate.ToString("d"),
                 DueDate.ToString("d"),
                 Amount));
        }
    }
}

