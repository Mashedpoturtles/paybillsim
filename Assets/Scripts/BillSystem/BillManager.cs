using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
    {
    /// <summary>
    /// This class handles the behaviour of instantiating bills and their behaviour once instantiated.
    /// </summary>
    public class BillManager : MonoBehaviour
        {
        [SerializeField]
        private GameObject spawnZone;
        [SerializeField]
        private RectTransform storageZone;
        public static List<Bill> Bills { get; set; }
        public static BillManager instance;
        public Text gameInfo;
        [SerializeField]

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
        private void Awake ( )
            {
            Initialize ( );
            }

        /// <summary>
        /// Setting everything that needs to be prepared in start.
        /// </summary>
        private void Initialize ( )
            {
            instance = this;
            Bills = new List<Bill> ( );
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
                GlobalAudio.instance.SoundPaidBill ( );
                Destroy ( bill.Object );
                Bills.Remove ( bill );
                }
            }
        public void InsufficientFunds ( Bill bill )
            {
            gameInfo.text = "Je hebt niet genoeg geld.";
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
                    createBill ( BillType.Electriciteit );
                    break;
                }
            }
        /// <summary>
        /// This method creates new bills on the specified days of the month.
        /// </summary>
        /// <param name="type"></param>
        private void createBill ( BillType type )
            {
            if ( spawnZone )
                {
                Bill newBill = new Bill ( type );
                Bills.Add ( newBill );
                GameObject billObject = Instantiate ( Resources.Load<GameObject> ( "billprefab" ) );
                newBill.Object = billObject;
                if ( billObject )
                    {
                    BillUI ui = billObject.GetComponent<BillUI> ( );
                    if ( ui )
                        {
                        newBill.Object.transform.SetParent ( spawnZone.transform, false );
                        ui.SetUI ( this, newBill );
                        }
                    }
                }
            }
        private void Normal ( Bill bill )
            {
            // throw new NotImplementedException ( );
            }
        private void billOverDueDate ( Bill bill )
            {
            // throw new NotImplementedException ( );
            }
        /// <summary>
        /// Sends a reminder to pay the bill past its due date.
        /// </summary>
        /// <param name="bill"></param>
        private void AanManing ( Bill bill )
            {
            // throw new NotImplementedException ( );
            }
        /// <summary>
        /// Sends one last reminder to pay the bill past the duedate but with increased cost.
        /// </summary>
        /// <param name="bill"></param>
        private void Somatie ( Bill bill )
            {
            var fine = 100;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            ui.AddWarning ( bill );
            }
        /// <summary>
        /// Sends a debt collector personally telling you to pay up, with increased costs.
        /// </summary>
        /// <param name="bill"></param>
        private void DagVaarding ( Bill bill )
            {
            var fine = 150;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponent<BillUI> ( );
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            }
        /// <summary>
        /// The judge will get involved and a debt collector will bring the verdict.
        /// </summary>
        /// <param name="bill"></param>
        private void Vonnis ( Bill bill )
            {
            var fine = 250;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponent<BillUI> ( );
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            }
        /// <summary>
        /// Seizure of funds and possessions.
        /// </summary>
        /// <param name="bill"></param>
        private void Beslag ( Bill bill )
            {
            Money.instance.currentMoney = 0;
            gameInfo.text = "Verloren.";
            LoadLevelsOnClick.instance.LoadMenuScene ( );
            
            GlobalAudio.instance.SoundGameOver ( );
            }
        }
    }