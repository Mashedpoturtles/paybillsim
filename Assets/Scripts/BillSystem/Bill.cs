using System;



namespace Assets.BillSystem
{
    public enum EscalationType
    {
        Ok,
        Warning,
        WarningWithFine,
        DebtCollector
    }

    public class Bill : IBill
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
        /// This will add to the total amount of issued bills.
        /// </summary>
        public int Counter { get; set; }
        /// <summary>
        /// Stores the bill information such as the type, issue date, due date and amount to be paid.
        /// </summary>
        /// <param name="type"></param>
        public bool IsShown { get; set; }
        public bool WarningSent { get; set; }
        public EscalationType escalation;

        public Bill(BillType type)
        {
            Type = type;
            IssueDate = TimeManager.currentTime;
            DueDate = TimeManager.currentTime.AddDays(31);

            Amount = 50;
        }
    }
}

