using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{
    public class BillManager : MonoBehaviour
    {

        static Canvas canvas;
        public static List<Bill> Billholder = new List<Bill>();

        public void CreateBill()
        {
            foreach (Bill bill in Billholder)
            {
                if (!bill.IsShown)
                {
                    bill.IsShown = true;



                    string billInformation = "";
                    billInformation = "Random";
                    billInformation = string.Format("Bill type: {0} \\n Issue date: {1} \\n  Due date: {2} \\n Amount to pay: {3} \\n",
                                      Enum.GetName(typeof(BillType), bill.Type),
                                      bill.IssueDate.ToString("d"),
                                      bill.DueDate.ToString("d"),
                                      bill.Amount).Replace("\\n", "\n");

                    CreateUILogic(billInformation);
                }
            }
        }

        public void CreateUILogic(string billInformationText)
        {
            GameObject billInformation = (GameObject)GameObject.Instantiate(Resources.Load("billInfo"));

            billInformation.GetComponentInChildren<Text>().text = billInformationText;

            Button buttonPay = billInformation.transform.FindChild("Button_Pay").GetComponent<Button>();

            buttonPay.onClick.AddListener(() => PayBill());



            Button buttonReturn = billInformation.transform.FindChild("Button_Return").GetComponent<Button>();

            buttonReturn.onClick.AddListener(() => ReturnBill());


            billInformation.transform.SetParent(canvas.transform, false);
            billInformation.transform.localPosition = Vector3.zero;
        }

        void Update()
        {
            Debug.Log(Billholder.Count);
        }

        public void Start()
        {
            canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
            Application.runInBackground = true;

            TimeManager.OnDayChange += IssueBill;
        }

        public void IssueBill()
        {
            switch (TimeManager.currentTime.DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    Billholder.Add(new Bill(BillType.Internet));
                    CreateBill();
                    break;
                case DayOfWeek.Thursday:
                    Billholder.Add(new Bill(BillType.Electricity));
                    CreateBill();
                    break;
            }
        }

        public void PayBill()
        {
            Destroy(GameObject.FindWithTag("billinfo"));
            foreach (Bill bill in Billholder.ToList())
            {
                if (bill.IsShown)
                {
                    bill.IsShown = false;
                    Debug.Log("You paid!");
                }
                Billholder.Remove(bill);
            }
        }

        public void ReturnBill()
        {
            Debug.Log("You returned the bill!");

            GameObject.FindWithTag("billinfo");
        }

        public void OnClickShowBill()
        {
            GameObject.FindWithTag("billinfo").layer = 8; // show bill on layer
        }
    }
}