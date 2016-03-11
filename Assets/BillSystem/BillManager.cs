using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Assets.BillSystem {
    public class BillManager : MonoBehaviour {

        #region <public variables>
        public GameObject _Holder;
        public static BillManager instance;
        public static List<Bill> Bills = new List<Bill>();
        #endregion
        public static List<Bill> GetBillsByType(BillType type)
        {
            return Bills.Select(bill => bill).ToList();
        }

        public void Start()
        {
            Application.runInBackground = true;
            instance = this;
            TimeManager.OnDayChange += IssueBills;// Subscribe to the OnDayChange event.
        }
      
        /// <summary>
        /// Sets which days to issue a bill.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool IsDay(DayOfWeek day)
        {
            return day == DayOfWeek.Tuesday;
        }
        /// <summary>
        /// Issues the bills into the scene depending on the day.
        /// </summary>
        /// <param name="type"></param>
        public static void IssueBill(BillType type)
        {
            if (!IsDay(TimeManager.currentTime.DayOfWeek))
                return;
            Bill bill = new Bill(type);
            Billing.AddIssueBill(DayOfWeek.Monday, BillType.Electricity);
            Billing.AddIssueBill(DayOfWeek.Monday, BillType.Internet);
            Billing.ReadFromDict();
        }
        public void IssueBills()
        {
            IssueBill(BillType.Internet);
            IssueBill(BillType.Electricity);
        }
    }
    /// <summary>
    /// This handles the dictionary and list of days and billtypes.
    /// </summary>
        static class Billing
        {
            private static Dictionary<DayOfWeek, List<BillType>> dict = new Dictionary<DayOfWeek, List<BillType>>();

            public static void ReadFromDict()
            {
                foreach (var issueDays in dict)
                {
                    Console.WriteLine("Key: " + issueDays.Key.ToString());
                    foreach (var billTypes in issueDays.Value)
                        Console.WriteLine("Value: " + billTypes);
                }
            }
      
            public static void AddIssueBill(DayOfWeek day, BillType type)
            {
                List<BillType> bill = new List<BillType>();
                foreach (var issueDays in dict)
                {
                    if (issueDays.Key == day)
                    {
                        bill = issueDays.Value;
                        bill.Add(type);
                    }
                }

                if (bill.Count == 0)
                    bill.Add(type);
                else
                    dict.Remove(day);

                dict.Add(day, bill);
            }     
    }
}