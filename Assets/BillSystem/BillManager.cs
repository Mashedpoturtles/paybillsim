<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.BillSystem;
>>>>>>> 37a16440b6db5249009a8e29adeab002d40fd2e1

public class BillManager : MonoBehaviour {

    public static BillManager instance;
<<<<<<< HEAD
    public static List<ElectricityBill> electricitybills;
    void Start()
=======

	public static List<Bill> Bills = new List<Bill>();

	public void Start()
>>>>>>> 37a16440b6db5249009a8e29adeab002d40fd2e1
    {
        instance = this;
        electricitybills = new List<ElectricityBill>();
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

<<<<<<< HEAD
    }

    IEnumerable<ElectricityBill> electricitybill =
        from ElectricityBill in electricitybills     
        where ElectricityBill.Exists = true
        select ElectricityBill;
=======
	public static bool IsBillDay(DayOfWeek day) {
		return day == DayOfWeek.Tuesday ||
			   day == DayOfWeek.Thursday;
	}

	public static int GetBillCount(BillType type) {
		return Bills.Select(bill => bill.Type == type).ToList().Count;
	}
>>>>>>> 37a16440b6db5249009a8e29adeab002d40fd2e1
}
 