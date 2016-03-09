using System;

namespace Assets.BillSystem {
	internal interface IBill {
		BillType Type { get; set; }
		int Amount { get; set; }
		DateTime DueDate { get; set; }
		DateTime IssueDate { get; set; }

		int Counter { get; }
	}

	public enum BillType {
		Electricity = 0,
		Internet,
	}
}