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
                GameObject billInformation = (GameObject)GameObject.Instantiate(Resources.Load("billInfo"));
                billInformation.GetComponentInChildren<Text>().text =
                (string.Format("Bill type:\\n {0} Issue date:\\n {1}  Due date:\\n {2} Amount to pay:\\n {3}",

                Enum.GetName(typeof(BillType), bill.Type).Replace("\\n", "\n"),
                 bill.IssueDate.ToString("d").Replace("\\n", "\n"),
                 bill.DueDate.ToString("d").Replace("\\n", "\n"),
                 bill.Amount).Replace("\\n", "\n"));

                billInformation.transform.SetParent(canvas.transform, false);
                billInformation.transform.localPosition = Vector3.zero;
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
                    break;
                case DayOfWeek.Thursday:
                    Billholder.Add(new Bill(BillType.Electricity));
                    break;
            }
        }
    }
}