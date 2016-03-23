using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{

    public class BillEscalator : MonoBehaviour
    {
        public delegate void BillPayed(int billId);
        public static event BillPayed OnBillPayed;

        public int BillId { get; set; }
        private Bill bill;
        private GameObject dialog;
        private int escalationCount = 0;

        void Start()
        {
            bill = BillManager.Billholder[BillId];
            TimeManager.OnDayChange += CheckAndShowWarning;
        }

        public void CheckAndShowWarning()
        {
            if (IsBillOverDue())
            {
                CreateWarning();
                bill.escalation = EscalationType.Warning;
                escalationCount++;

                if (bill.escalation != EscalationType.Ok)
                {
                    switch (escalationCount)
                    {
                        case 1:
                            bill.escalation = EscalationType.Warning;
                            break;
                        case 2:
                            bill.escalation = EscalationType.WarningWithFine; //after this warning 
                            break;
                        case 4:
                            bill.escalation = EscalationType.DebtCollector;
                            break;
                    }


                }
            }

        }


        public bool IsBillOverDue()
        {
            return bill.DueDate.Ticks > TimeManager.currentTime.Ticks;
        }

        public void CreateWarning()
        {
            Text dialogueInfo;
            Button warning;
            Button pay;
            dialog = (GameObject)Instantiate(Resources.Load("WarningInfo"));
            dialogueInfo = dialog.GetComponentInChildren<Text>();
            dialogueInfo.text = string.Format("God damit you fagot, pay that bill, thats a {0}, its not that hard!", bill.Type.ToString());
            pay = dialog.transform.FindChild("Button_Pay_Warning").GetComponent<Button>();
            warning = dialog.transform.FindChild("Button_Return_Warning").GetComponent<Button>();
            pay.onClick.AddListener(() => PayWarning());
            warning.onClick.AddListener(() => ReturnWarning());
            dialog.transform.SetParent(BillManager.canvas.transform, false);

        }

        private void PayWarning()
        {
            var canAfford = false;

            if (Money.instance.currentMoney >= bill.Amount)
            {
                canAfford = true;
                Money.instance.currentMoney -= bill.Amount;
            }
            else
            {
                canAfford = false;
            }

            if (canAfford)
            {
                OnBillPayed(BillId);
                Destroy(dialog);
                Destroy(this);
            }
            else
            {
                Debug.Log("You cant afford to pay this warning!");
            }
        }
        private void ReturnWarning()
        {
            CanvasGroup canvasGroup = dialog.GetComponent<CanvasGroup>(); // nvm wtf im saying im an idiot go on
            canvasGroup = dialog.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
    }
}