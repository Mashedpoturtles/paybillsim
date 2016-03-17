using System;
using System.Collections.Generic;
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

        // this is what puts the UI together
        public void CreateUILogic(string billInformationText)
        {
            //spawn the ui prefab
            GameObject billInformation = (GameObject)GameObject.Instantiate(Resources.Load("billInfo"));

            //find the text component inside the prefab and assign it the billinformation 
            billInformation.GetComponentInChildren<Text>().text = billInformationText;

            //find the pay button in the prefab by its tag and give it an onlick listener with Paybill() assigned to it
            Button buttonPay = GameObject.FindWithTag("buttonPay").GetComponent<Button>();
            buttonPay.onClick.AddListener(() => PayBill());
            // find the return button in the prefab by its tag and give it an onlick listener with ReturnBill() assigned to it
            Button buttonReturn = GameObject.FindWithTag("buttonReturn").GetComponent<Button>();
            buttonReturn.onClick.AddListener(() => ReturnBill());

            //position the UI elements by parenting it to the canvas in the scene.
            billInformation.transform.SetParent(canvas.transform, false);
            billInformation.transform.localPosition = Vector3.zero;
        }

        void Update()
        {
            Debug.Log(Billholder.Count); // keep count of the total added bills to list
        }

        public void Start()
        {
            //grabs the Canvas component
            canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
            Application.runInBackground = true;

            TimeManager.OnDayChange += IssueBill; // this event makes sure that a bill wont issue bills more than once on its assigned day
        }

        //Create a bill on the set days below and add one to the list
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
            Debug.Log("You paid!");

            for (int i = 0; i >= Billholder.Count; i++) { /* other logic */ Billholder.RemoveAt(i); }
            {


            }


            Destroy(GameObject.FindWithTag("billinfo"));
        }

        public void ReturnBill()
        {
            Debug.Log("You returned the bill!");
        }
    }
}