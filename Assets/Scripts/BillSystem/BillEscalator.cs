using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
{
    public class BillEscalator : MonoBehaviour
    {
        public delegate void BillPayed(int billId);
        public static event BillPayed OnBillPayed;
        public int BillId { get; set; }
        private GameObject SpawnZone;
        private GameObject dialog;
        private int escalationCount = 0;

        void Start()
        {

        }

        public void CheckAndShowWarning()
        {
            if (BillManager.Billholder[BillId].WarningSent == false && IsBillOverDue())
            {
                CreateWarning();
                BillManager.Billholder[BillId].escalation = EscalationType.Warning;
                BillManager.Billholder[BillId].WarningSent = true;
                escalationCount++;
            }

            else if (BillManager.Billholder[BillId].escalation != EscalationType.Ok)
            {
                switch (escalationCount)
                {
                    case 1:
                        BillManager.Billholder[BillId].escalation = EscalationType.Warning;
                        break;
                    case 2:
                        BillManager.Billholder[BillId].escalation = EscalationType.WarningWithFine;
                        break;
                    case 4:
                        BillManager.Billholder[BillId].escalation = EscalationType.DebtCollector;
                        break;
                }
            }
        }

        public bool IsBillOverDue()
        {
            if ((BillManager.Billholder[BillId].DueDate < TimeManager.currentTime))
            {
                return true;
            }
            return false;
        }

        public void CreateWarning()
        {
            Text dialogueInfo;
            Button warning;
            Button pay;
            dialog = (GameObject)Instantiate(Resources.Load("WarningInfo"));
            dialogueInfo = dialog.GetComponentInChildren<Text>();
            dialogueInfo.text = string.Format("pay that bill, thats a {0}~!", BillManager.Billholder[BillId].Type.ToString());
            pay = dialog.transform.FindChild("Button_Pay_Warning").GetComponent<Button>();
            warning = dialog.transform.FindChild("Button_Return_Warning").GetComponent<Button>();
            pay.onClick.AddListener(() => PayWarning());
            SpawnZone = GameObject.FindWithTag("SpawnZone");
            dialog.transform.SetParent(SpawnZone.transform, false);
        }

        private void PayWarning()
        {
            var canAfford = false;

            if (Money.instance.currentMoney >= BillManager.Billholder[BillId].Amount)
            {
                canAfford = true;
                Money.instance.currentMoney -= BillManager.Billholder[BillId].Amount;
            }
            else
            {
                canAfford = false;
            }

            if (canAfford)
            {
                OnBillPayed(BillId);
                Destroy(dialog);
            }
            else
            {
                Debug.Log("You cant afford to pay this warning!");
            }
        }

    }
}