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

        public DateTime DueDate { get; set; }
        public DateTime aanmaning { get; set; }
        public DateTime somatie { get; set; }
        public DateTime dagvaarding { get; set; }
        public DateTime vonnis { get; set; }
        public DateTime beslag { get; set; }
        public DateTime IssueDate { get; set; }

        public Bill ( BillType type )
            {
            Type = type;
            IssueDate = TimeManager.currentTime;
            DueDate = TimeManager.currentTime.AddDays ( 31 );
            aanmaning = TimeManager.currentTime.AddDays ( 62 );
            somatie = TimeManager.currentTime.AddDays ( 93 );
            vonnis = TimeManager.currentTime.AddDays ( 124 );
            beslag = TimeManager.currentTime.AddDays ( 155 );
            Cost = 50;
            dueLevel = 0;
            }
        }
    }

