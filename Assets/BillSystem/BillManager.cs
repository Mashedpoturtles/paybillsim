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
        TimeManager.OnDayChange += IssueBill; // Subscribe to the OnDayChange event.
    }

    //issue all the bills at their set times in update.
    void Update()
    {
       
    }

	public void IssueBill(BillType type)
    {
		if (!IsBillDay(TimeManager.currentTime.DayOfWeek)) return;
        Debug.Log("issuebill triggered");
        Bill bill = _Holder.AddComponent<Bill>();
	}

	public static bool IsBillDay(DayOfWeek day)
    {
		return day == DayOfWeek.Tuesday ||
			   day == DayOfWeek.Thursday;
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
 