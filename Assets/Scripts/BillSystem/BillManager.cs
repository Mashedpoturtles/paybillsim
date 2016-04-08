using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.BillSystem
    {
    public class BillManager : MonoBehaviour
        {
        [SerializeField]
        private RectTransform SpawnZone;
        public List<Bill> Bills { get; private set; }
        public static BillManager instance;
        void Start ( )
            {
            this.Bills = new List<Bill> ( );
            instance = this;
            Application.runInBackground = true;
            TimeManager.OnDayChange += onDayChanged;
            }

        void Update ( )
            {
            foreach ( var bill in Bills )
                {
                if ( TimeManager.currentTime > bill.DueDate )
                    {
                    billOverDueDate ( bill );
                    }
                }
            }

        public void PayBill ( Bill bill )
            {
            if ( Money.instance.currentMoney >= bill.Amount )
                {
                Money.instance.currentMoney -= bill.Amount;
                Debug.Log ( "I'm about to pay " + bill.Amount + " and i have " + Money.instance.currentMoney + " my obj is : " + bill.Object, bill.Object );
                Destroy ( bill.Object );
                this.Bills.Remove ( bill );
                }
            else {
                Debug.Log ( "You cant afford to pay this bill!" );
                }
            }
        private void onDayChanged ( )
            {
            switch ( TimeManager.currentTime.DayOfWeek )
                {
                case DayOfWeek.Tuesday:
                    createBill ( BillType.Internet );
                    break;

                case DayOfWeek.Thursday:
                    createBill ( BillType.Electricity );
                    break;
                }
            }

        private void createBill ( BillType type )
            {
            Bill newBill = new Bill ( type );
            Bills.Add ( newBill );
            GameObject billObject = Instantiate ( Resources.Load ( "billInfo" ) ) as GameObject;
            newBill.Object = billObject;
            BillUI ui = billObject.GetComponent<BillUI> ( );
            ui.transform.SetParent ( SpawnZone.transform, false );
            ui.SetInfo ( this, newBill );
            }

        private void billOverDueDate ( Bill bill )
            {
            print ( "past duedate" );
            }
        }
    }

