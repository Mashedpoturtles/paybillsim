using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.BillSystem;


public class BillManager : MonoBehaviour {

    public GameObject _Holder;
    public static BillManager instance;
	public static List<Bill> Bills = new List<Bill>();

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
		if (!IsTuesday(TimeManager.currentTime.DayOfWeek) || !IsThursday(TimeManager.currentTime.DayOfWeek)) return;
       
        Bill bill = _Holder.AddComponent<Bill>();
	}
    public void IssueBills()
    {
        IssueBill(BillType.Internet);
        IssueBill(BillType.Electricity); 
    }
	public static bool IsTuesday(DayOfWeek day)
    {
        return day == DayOfWeek.Tuesday;
	}
    public static bool IsThursday(DayOfWeek day)
    {
        return day == DayOfWeek.Thursday;
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
 