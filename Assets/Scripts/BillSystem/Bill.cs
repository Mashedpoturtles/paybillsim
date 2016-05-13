using System;
using UnityEngine;

namespace Assets.BillSystem
    {
    public enum BillType
        {
        Electriciteit,
        Internet,
        GasEnLicht,
        Telefoon,
        ZorgVerzekering,
        Event
        }

    public class Bill
        {
        /// <summary>
        /// The amount you must pay for this bill initially.
        /// </summary>
        public int Cost { get; set; }
        public int RecievedCost { get; set; }
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
            IssueDate = GameManager.currentTime;
            DueDate = GameManager.currentTime.AddDays ( 31 );
            Aanmaning = GameManager.currentTime.AddDays ( 62 );
            Somatie = GameManager.currentTime.AddDays ( 93 );
            Dagvaarding = GameManager.currentTime.AddDays ( 124 );
            Vonnis = GameManager.currentTime.AddDays ( 155 );
            Beslag = GameManager.currentTime.AddDays ( 186 );
            Cost = 0;
            RecievedCost = 0;
            }
        }
    }

