using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{
    public class BillManager : MonoBehaviour
    {
        public static BillManager instance;
        static Canvas canvas;
        public static List<Bill> Billholder = new List<Bill>();

        public void AddToList()
        {
            foreach (Bill bill in Billholder)
            {
                GameObject billInformation = (GameObject)(Resources.Load("billInfo"));

                Button buttonPay = GameObject.FindWithTag("buttonPay").GetComponent<Button>();
                buttonPay.onClick.AddListener(() => PayBill());

                Button buttonReturn = GameObject.FindWithTag("buttonReturn").GetComponent<Button>();
                buttonReturn.onClick.AddListener(() => ReturnBill());
                billInformation.GetComponentInChildren<Text>().text =
                (string.Format("Bill type: {0} \\n Issue date: {1} \\n  Due date: {2} \\n Amount to pay: {3} \\n",

           Enum.GetName(typeof(BillType), bill.Type),
                 bill.IssueDate.ToString("d"),
                 bill.DueDate.ToString("d"),
                 bill.Amount).Replace("\\n", "\n"));
            }

        }

        void Update()
        {
            AddToList();
            Debug.Log(Billholder.Count);
        }

        public void Start()
        {
            canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
            Application.runInBackground = true;
            instance = this;
            TimeManager.OnDayChange += IssueBill;
        }

        public static void IssueBill()
        {
            switch (TimeManager.currentTime.DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    Billholder.Add(new Bill(BillType.Internet));
                    GameObject billInformation = (GameObject)GameObject.Instantiate(Resources.Load("billInfo"));
                    billInformation.transform.SetParent(canvas.transform, false);
                    billInformation.transform.localPosition = Vector3.zero;
                    break;
                case DayOfWeek.Thursday:
                    Billholder.Add(new Bill(BillType.Electricity));
                    GameObject billInformationTwo = (GameObject)GameObject.Instantiate(Resources.Load("billInfo"));
                    billInformationTwo.transform.SetParent(canvas.transform, false);
                    billInformationTwo.transform.localPosition = Vector3.zero;
                    break;
            }
        }
        public void PayBill()
        {
            Debug.Log("You paid!");
        }

        public void ReturnBill()
        {
            Debug.Log("You returned the bill!");
        }
    }
}