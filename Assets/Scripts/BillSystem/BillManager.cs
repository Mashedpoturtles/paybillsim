using System.Collections.Generic;
using UnityEngine;

namespace Assets.BillSystem
    {
    /// <summary>
    /// This class handles the behaviour of instantiating bills and their behaviour once instantiated.
    /// </summary>
    public class BillManager : MonoBehaviour
        {
        [SerializeField]
        private RectTransform SpawnZone;
        private BillUI ui;
        public List<Bill> Bills { get; private set; }

        void Update ( )
            {
            foreach ( var bill in Bills )
                {
                if ( TimeManager.currentTime == bill.DueDate && TimeManager.currentTime < bill.Aanmaning )
                    {
                    billOverDueDate ( bill );
                    }
                if ( TimeManager.currentTime == bill.Aanmaning && TimeManager.currentTime < bill.Somatie )
                    {
                    AanManing ( bill );
                    }
                if ( TimeManager.currentTime == bill.Somatie && TimeManager.currentTime < bill.Dagvaarding )
                    {
                    Somatie ( bill );
                    }
                if ( TimeManager.currentTime == bill.Dagvaarding && TimeManager.currentTime < bill.Vonnis )
                    {
                    DagVaarding ( bill );
                    }
                if ( TimeManager.currentTime == bill.Vonnis && TimeManager.currentTime < bill.Beslag )
                    {
                    Vonnis ( bill );
                    }
                if ( TimeManager.currentTime == bill.Beslag )
                    {
                    Beslag ( bill );
                    }
                else
                    {
                    Normal ( bill );
                    }
                }
            }
        private void Start ( )
            {
            Initialize ( );
            }
        /// <summary>
        /// Setting everything that needs to be prepared in start.
        /// </summary>
        private void Initialize ( )
            {
            this.Bills = new List<Bill> ( );
            Application.runInBackground = true;
            TimeManager.OnDayChange += onDayChanged;
            }
        /// <summary>
        /// This method checks whether the player has sufficient money to pay the bill and takes care of removing it if so.
        /// </summary>
        /// <param name="bill"></param>
        public void PayBill ( Bill bill )
            {
            if ( Money.instance.currentMoney >= bill.Cost )
                {
                Money.instance.currentMoney -= bill.Cost;
                Debug.Log ( "I'm about to pay " + bill.Cost + " and i have " + Money.instance.currentMoney + " my obj is : " + bill.Object, bill.Object );
                Destroy ( bill.Object );
                this.Bills.Remove ( bill );
                }
            else {
                Debug.Log ( "You cant afford to pay this bill!" );
                }
            }
        /// <summary>
        /// This method is used to set the day of the month on which a bill is ment to instantiate.
        /// </summary>
        private void onDayChanged ( )
            {
            switch ( TimeManager.currentTime.Day )
                {
                case 21:
                    createBill ( BillType.Internet );
                    break;

                case 24:
                    createBill ( BillType.Electricity );
                    break;
                }
            }
        /// <summary>
        /// This method creates new bills on the specified days of the month.
        /// </summary>
        /// <param name="type"></param>
        private void createBill ( BillType type )
            {
            Bill newBill = new Bill ( type );
            Bills.Add ( newBill );
            GameObject billObject = Instantiate ( Resources.Load ( "billprefab" ) ) as GameObject;
            newBill.Object = billObject;
            BillUI ui = billObject.GetComponent<BillUI> ( );
            ui.transform.SetParent ( SpawnZone.transform, false );
            ui.SetUI ( this, newBill );
            }
        private void Normal ( Bill bill )
            {
            Debug.Log ( "You can still pay the bill on time." );
            }
        private void billOverDueDate ( Bill bill )
            {
            Debug.Log ( "bill is due!" );
            }
        /// <summary>
        /// Sends a reminder to pay the bill past its due date.
        /// </summary>
        /// <param name="bill"></param>
        private void AanManing ( Bill bill )
            {
            Debug.Log ( "aanmaning" );
            }
        /// <summary>
        /// Sends one last reminder to pay the bill past the duedate but with increased cost.
        /// </summary>
        /// <param name="bill"></param>
        private void Somatie ( Bill bill )
            {
            Debug.Log ( "Somatie" );
            var fine = 100;
            bill.Cost += fine;
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            ui.AddWarning ( bill );
            }
        /// <summary>
        /// Sends a debt collector personally telling you to pay up, with increased costs.
        /// </summary>
        /// <param name="bill"></param>
        private void DagVaarding ( Bill bill )
            {
            Debug.Log ( "Dagvaarding" );
            var fine = 150;
            bill.Cost += fine;
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            }
        /// <summary>
        /// The judge will get involved and a debt collector will bring the verdict.
        /// </summary>
        /// <param name="bill"></param>
        private void Vonnis ( Bill bill )
            {
            Debug.Log ( "Vonnis" );
            var fine = 250;
            bill.Cost += fine;
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            }
        /// <summary>
        /// Seizure of funds and possessions.
        /// </summary>
        /// <param name="bill"></param>
        private void Beslag ( Bill bill )
            {
            Debug.Log ( "Beslag" );
            Debug.Log ( "Game Over!" );
            }
        }
    }