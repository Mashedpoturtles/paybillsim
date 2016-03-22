using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{
    public class BillManager : MonoBehaviour
    {
        static Canvas canvas;
        public static bool canAfford;
        public static Dictionary<int, Bill> Billholder = new Dictionary<int, Bill>();
        public static Dictionary<int, GameObject> InfoHolder = new Dictionary<int, GameObject>();

        public void CreateBill()
        {
            foreach (KeyValuePair<int, Bill> pair in Billholder)
            {
                if (!InfoHolder.ContainsKey(pair.Key))
                {
                    pair.Value.IsShown = true;

                    string billInformation = "";
                    billInformation = "Random";
                    billInformation = string.Format("Bill type: {0} \\n Issue date: {1} \\n  Due date: {2} \\n Amount to pay: {3} \\n",
                                        Enum.GetName(typeof(BillType), pair.Value.Type),
                                        pair.Value.IssueDate.ToString("d"),
                                        pair.Value.DueDate.ToString("d"),
                                        pair.Value.Amount).Replace("\\n", "\n");

                    CreateUILogic(billInformation, pair.Key);
                }
            }
        }

        public void CreateUILogic(string billInformationText, int billID)
        {
            InfoHolder.Add(billID, (GameObject)Instantiate(Resources.Load("billInfo")));
            InfoHolder[billID].GetComponentInChildren<Text>().text = billInformationText;

            Button buttonPay = InfoHolder[billID].transform.FindChild("Button_Pay").GetComponent<Button>();
            buttonPay.onClick.AddListener(() => PayBill(buttonPay.name));

            buttonPay.name = billID.ToString();

            Button buttonReturn = InfoHolder[billID].transform.FindChild("Button_Return").GetComponent<Button>();
            buttonReturn.onClick.AddListener(() => ReturnBill(buttonReturn.name));
            buttonReturn.name = billID.ToString();

            InfoHolder[billID].transform.SetParent(canvas.transform, false);
            InfoHolder[billID].transform.localPosition = Vector3.zero;
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
                    Billholder.Add(GetKey(), new Bill(BillType.Internet));
                    CreateBill();
                    break;
                case DayOfWeek.Thursday:
                    Billholder.Add(GetKey(), new Bill(BillType.Electricity));
                    CreateBill();
                    break;
            }
        }

        public int GetKey()
        {
            for (int i = 0; i < Billholder.Count + 1; i++)
            {
                if (!Billholder.ContainsKey(i))
                    return i;
            }
            return 0;
        }

        public void PayBill(string billId)
        {
            Debug.Log("Amount of bills currently in queue :" + Billholder.Count);
            if (Money.instance.currentMoney >= Billholder[Convert.ToInt32(billId)].Amount)
            {
                canAfford = true;
                Money.instance.currentMoney -= Billholder[Convert.ToInt32(billId)].Amount;
            }
            else
            {
                canAfford = false;
            }

            if (canAfford)
            {
                Billholder.Remove(Convert.ToInt32(billId));
                Destroy(InfoHolder[Convert.ToInt32(billId)]);
                InfoHolder.Remove(Convert.ToInt32(billId));
            }
            else
            {
                Debug.Log("You cant afford to pay this bill!");
            }
        }

        public void ReturnBill(string billId)
        {
            CanvasGroup canvasGroup = (InfoHolder[Convert.ToInt32(billId)]).GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            Billholder[Convert.ToInt32(billId)].IsShown = false;
        }

        public void OnClickShowBill()
        {
            foreach (KeyValuePair<int, Bill> bill in Billholder)
            {
                if (!bill.Value.IsShown)
                {
                    bill.Value.IsShown = true;

                    CanvasGroup canvasGroup = (InfoHolder[Convert.ToInt32(bill.Key)]).GetComponent<CanvasGroup>();
                    canvasGroup.alpha = 1f;
                    canvasGroup.blocksRaycasts = true;
                }
            }
        }
    }
}