using System;
using UnityEngine;

namespace Assets.BillSystem
    {
    public enum BillType
        {
        Electricity,
        Internet,
        }

    public class Bill
        {
        /// <summary>
        /// The amount you must pay for this bill initially.
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// The kind of bill, for e.g. internet, electricity or water bills.
        /// </summary>
        public BillType Type { get; set; }
        /// <summary>
        /// the current state of the due level of a bill.
        /// </summary>
        public int dueLevel { get; set; }
        /// <summary>
        /// Reference to the object this information is sent to.
        /// </summary>
        public GameObject Object;

        public int DaysUntilDue { get; set; }
        public int DaysPastDue { get; set; }

        public DateTime DueDate { get; set; }
        public bool normal { get; set; }
        public bool aanmaning { get; set; }
        public bool somatie { get; set; }
        public bool dagvaarding { get; set; }
        public bool vonnis { get; set; }
        public bool beslag { get; set; }
        public DateTime IssueDate { get; set; }

        public Bill ( BillType type )
            {
            Type = type;
            IssueDate = TimeManager.currentTime;
            DueDate = TimeManager.currentTime.AddDays ( 31 );
            this.DaysPastDue = DaysPastDue;
            Cost = 50;
            dueLevel = 0;
            }
        }
    }

