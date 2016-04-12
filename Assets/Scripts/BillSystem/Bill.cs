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
        /// Reference to the object this information is sent to.
        /// </summary>
        public GameObject Object;

        public DateTime Aanmaning { get; set; }
        public DateTime Somatie { get; set; }
        public DateTime Dagvaarding { get; set; }
        public DateTime Vonnis { get; set; }
        public DateTime Beslag { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime IssueDate { get; set; }

        public Bill ( BillType type )
            {
            Type = type;
            IssueDate = TimeManager.currentTime;
            DueDate = TimeManager.currentTime.AddDays ( 31 );
            Aanmaning = TimeManager.currentTime.AddDays ( 62 );
            Somatie = TimeManager.currentTime.AddDays ( 93 );
            Dagvaarding = TimeManager.currentTime.AddDays ( 124 );
            Vonnis = TimeManager.currentTime.AddDays ( 155 );
            Beslag = TimeManager.currentTime.AddDays ( 186 );
            Cost = 50;
            }
        }
    }

