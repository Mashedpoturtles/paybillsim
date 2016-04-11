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
        public List<Bill> Bills { get; private set; }
        private void Update ( )
            {
            foreach ( var bill in Bills )
                {
                if ( bill.DaysUntilDue == 0 )
                    bill.normal = true;
                bill.DaysPastDue++;
                if ( bill.DaysPastDue >= 62 )
                    {
                    bill.aanmaning = true;
                    }
                else if ( bill.DaysPastDue >= 93 )
                    {
                    bill.somatie = true;
                    }
                else if ( bill.DaysPastDue >= 124 )
                    {
                    bill.vonnis = true;
                    }
                else if ( bill.DaysPastDue >= 155 )
                    {
                    bill.beslag = true;
                    }
                Debug.Log ( "days past due:  " + bill.DaysPastDue );
                Debug.Log ( "days util due  " + bill.DaysUntilDue );
                Debug.Log ( bill.normal );
                Debug.Log ( bill.aanmaning );
                Debug.Log ( bill.somatie );
                Debug.Log ( bill.vonnis );
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
            TimeManager.OnDayChange += OverdueAction;
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
        /// <summary>
        /// This method sets the duelevels to their appropriate consequence
        /// </summary>
        /// <param name="bill"></param>
        private void BillCheckIsOverDue ( Bill bill )
            {
            if ( bill.normal )
                {
                bill.dueLevel = 0;
                return;
                }
            if ( bill.aanmaning )
                {
                bill.dueLevel = 1;
                return;
                }
            if ( bill.somatie )
                {
                bill.dueLevel = 2;
                return;
                }
            if ( bill.dagvaarding )
                {
                bill.dueLevel = 3;
                return;
                }
            if ( bill.vonnis )
                {
                bill.dueLevel = 4;
                return;
                }
            if ( bill.beslag )
                {
                bill.dueLevel = 5;
                return;
                }
            }
        /// <summary>
        /// This method triggers the consequences regarding their duelevel.
        /// </summary>
        private void OverdueAction ( )
            {
            foreach ( var bill in Bills )
                {
                BillCheckIsOverDue ( bill );
                switch ( bill.dueLevel )
                    {
                    case 0:
                        Normal ( bill );
                        break;
                    case 1:
                        AanManing ( bill );
                        break;
                    case 2:
                        Somatie ( bill );
                        break;
                    case 3:
                        DagVaarding ( bill );
                        break;
                    case 4:
                        Vonnis ( bill );
                        break;
                    case 5:
                        Beslag ( bill );
                        break;
                    }
                }
            }
        private void Normal ( Bill bill )
            {
            Debug.Log ( "You can still pay the bill on time." );
            }
        /// <summary>
        /// Sends a reminder to pay the bill past its due date.
        /// </summary>
        /// <param name="bill"></param>
        private void AanManing ( Bill bill )
            {
            Debug.Log ( "We are on case  which is aanmaning" );
            return;
            }
        /// <summary>
        /// Sends one last reminder to pay the bill past the duedate but with increased cost.
        /// </summary>
        /// <param name="bill"></param>
        private void Somatie ( Bill bill )
            {
            Debug.Log ( "We are on case which is Vonnis" );
            var fine = 500;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            return;
            }
        /// <summary>
        /// Sends a debt collector personally telling you to pay up, with increased costs.
        /// </summary>
        /// <param name="bill"></param>
        private void DagVaarding ( Bill bill )
            {
            Debug.Log ( "We are on case which is Dagvaarding" );
            return;
            }
        /// <summary>
        /// The judge will get involved and a debt collector will bring the verdict.
        /// </summary>
        /// <param name="bill"></param>
        private void Vonnis ( Bill bill )
            {
            Debug.Log ( "We are on case which is Vonnis" );
            return;
            }
        /// <summary>
        /// Seizure of funds and possessions.
        /// </summary>
        /// <param name="bill"></param>
        private void Beslag ( Bill bill )
            {
            Debug.Log ( "We are on case which is Beslag" );
            return;
            }
        }
    }
