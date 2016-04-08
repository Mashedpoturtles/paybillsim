using System;
using UnityEngine;

namespace Assets.BillSystem
    {
    public class Bill
        {
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
        /// Stores the bill information such as the type, issue date, due date and amount to be paid.
        /// </summary>
        /// <param name="type"></param>
        public int dueLevel { get; set; }
        public GameObject Object;
        public Bill ( BillType type )
            {
            Type = type;
            IssueDate = TimeManager.currentTime;
            DueDate = TimeManager.currentTime.AddDays ( 31 );
            Amount = 50;
            dueLevel = 0;
            }
        }
    }

