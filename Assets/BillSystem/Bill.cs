using System;
using System.Net.Mime;

namespace Assets.BillSystem {
	public class Bill : MonoBehaviour, IBill
	{
		public Text BillInformation;
		public static int numberOfBills;

		public int Amount { get; set; }

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

 