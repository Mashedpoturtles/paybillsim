using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{
    public class Bill : MonoBehaviour, IBill
    {
        public Text BillInformation;
        public static int numberOfBills;

        public string BillType { get; set; }
        /// <summary>
        /// amount to pay
        /// </summary>
        public int Amount { get; set; }
        public BillType Type { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime IssueDate { get; set; }
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

