using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Assets.BillSystem {
    public class BillManager : MonoBehaviour {

        #region
        public GameObject _Holder;
        public static BillManager instance;
        public static List<Bill> Bills = new List<Bill>();
        public  Dictionary<DayOfWeek, List<BillType>> IssueDays = new Dictionary<DayOfWeek, List<BillType>>();
        #endregion


        public void Start()

        {
            Application.runInBackground = true;
            instance = this;
            TimeManager.OnDayChange += IssueBills; // Subscribe to the OnDayChange event.
        }

        //issue all the bills at their set times in update.
        void Update()
        {

        }

        public void IssueBill(BillType type)
        {
            if (!IsDay(TimeManager.currentTime.DayOfWeek))
                return;
            Bill bill = _Holder.AddComponent<Bill>();
        }

        public void IssueBills()
        {
            IssueBill(BillType.Internet);
            IssueBill(BillType.Electricity);
        }
        public static bool IsDay(DayOfWeek day)
        {
            return day == DayOfWeek.Tuesday;
        }


        public void IssueStartingPoint()
        {
            AddIssueDay(DayOfWeek.Thursday, List<BillType>; // issue here
           
        }

        public void AddIssueDay(DayOfWeek day, List<BillType> type)
        {
            IssueDays.Add(day, type);
            /// issue here 
        }
    

        public void RemoveIssueDay(DayOfWeek day)
        {
            IssueDays.Remove(DayOfWeek.Tuesday);
            IssueDays.Remove(DayOfWeek.Thursday);
        }

        public static int GetBillCount(BillType type)
        {
            return Bills.Select(bill => bill.Type == type).ToList().Count;
        }

        public static List<Bill> GetBillsByType(BillType type)
        {
            return Bills.Select(bill => bill).ToList();
        }
    }
}