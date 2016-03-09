using System;
using System.Net.Mime;

namespace Assets.BillSystem {
	public class Bill : MonoBehaviour, IBill
	{
		public Text BillInformation;
		public static int numberOfBills;

<<<<<<< HEAD
    public string BillType { get; set; }
=======
		public int Amount { get; set; }
>>>>>>> 37a16440b6db5249009a8e29adeab002d40fd2e1

		public BillType Type { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime IssueDate { get; set; }

		public Bill(BillType type) {

			Type = type;
			DueDate = TimeManager.currentTime.AddDays(32);
			IssueDate = TimeManager.currentTime;
			Amount = 50;
			BillInformation.text = $"Bill number: {BillManager.GetBillCount(type)} Bill type: {Enum.GetName(typeof(BillType), Type)} Issue date: {IssueDate.ToString("d")}  Due date: {DueDate.ToString("d")} Amount to pay: {Amount}";
		}
	}
}

 