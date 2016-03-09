using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{
    public class Bill : MonoBehaviour, IBill
    {
        /// <summary>
        /// Display the bill information to text components.
        /// </summary>
        public Text BillInformation;
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

        public Bill(BillType type)
        {

            Type = type;
            DueDate = TimeManager.currentTime.AddDays(1);
            IssueDate = TimeManager.currentTime;
            Amount = 50;

            BillInformation.text = string.Format("Bill number: {0} Bill type: {1} Issue date: {2}  Due date: {3} Amount to pay: {4}",
                 BillManager.GetBillCount(type),
                 Enum.GetName(typeof(BillType), type),
                 IssueDate.ToString("d"),
                 DueDate.ToString("d"),
                 Amount);

        }
    }
}

