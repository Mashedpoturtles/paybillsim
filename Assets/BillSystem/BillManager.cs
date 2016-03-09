using System;
using System.Collections.Generic;
using System.Linq;
using Assets.BillSystem;

public class BillManager : MonoBehaviour {

    public static BillManager instance;

	public static List<Bill> Bills = new List<Bill>();

	public void Start()
    {
        instance = this;
    }

    //issue all the bills at their set times in update.
    void Update()
   { 
        IssueBill(BillType.Electricity);
		IssueBill(BillType.Internet);
    }

	public void IssueBill(BillType type) {
		if (!IsBillDay(TimeManager.currentTime.DayOfWeek)) return;

		Bills.Add(new Bill(type));
	}

	public static bool IsBillDay(DayOfWeek day) {
		return day == DayOfWeek.Tuesday ||
			   day == DayOfWeek.Thursday;
	}

	public static int GetBillCount(BillType type) {
		return Bills.Select(bill => bill.Type == type).ToList().Count;
	}
}
 