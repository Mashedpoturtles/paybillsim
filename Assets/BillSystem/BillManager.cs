using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.BillSystem;


public class BillManager : MonoBehaviour {
    private bool _billingDone = false;
    public GameObject _Holder;
    public static BillManager instance;
	public static List<Bill> Bills = new List<Bill>();

	public void Start()
 
    {
        Application.runInBackground = true;
        instance = this;
    }

    //issue all the bills at their set times in update.
    void Update()
    {
        if (_billingDone)
            return;

        IssueBill(BillType.Electricity);
		IssueBill(BillType.Internet);
        _billingDone = true;
    }

	public void IssueBill(BillType type)
    {
		if (!IsBillDay(TimeManager.currentTime.DayOfWeek)) return;
         
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
 