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
        [SerializeField]
        private GameObject Inbox;
        public static List<Bill> Bills { get; set; }
        public static List<GameObject> envelopes { get; set; }
        [HideInInspector]
        public GameObject envelope;
        public static BillManager instance;
        public Text gameInfo;
        private float speed = 800f;
        [SerializeField]
        private Text _textBillCounterNumber;
        [SerializeField]
        private Text _textBillCounter;[SerializeField]
        private GameObject pressSpace;


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
            envelopes = new List<GameObject> ( );
            envelope = ( Resources.Load<GameObject> ( "Envelope" ) );
            Application.runInBackground = true;
            TimeManager.OnDayChange += onDayChanged;
            }

        void Update ( )
            {
            DueDateCheck ( );
            EnvelopeHandler ( );
            BillCounter ( );
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
        /// This method creates new bills on the specified days of the month.
        /// </summary>
        /// <param name="type"></param>
        private void createBill ( BillType type )
            {
            if ( spawnZone )
                {
                Bill newBill = new Bill ( type );
                Bills.Add ( newBill );
                GameObject envelope = Instantiate ( Resources.Load<GameObject> ( "Envelope" ) );
                envelopes.Add ( envelope );
                envelope.transform.SetParent ( Inbox.transform, false );
                GlobalAudio.instance.SoundBillPending ( );
                GameObject billObject = Instantiate ( Resources.Load<GameObject> ( "billprefab" ) );
                newBill.Object = billObject;
                if ( billObject )
                    {
                    BillUI ui = billObject.GetComponentInChildren<BillUI> ( );
                    if ( ui )
                        {
                        newBill.Object.transform.SetParent ( envelope.transform.FindChild ( "SpawnZone" ).transform, false );

                        ui.SetUI ( this, newBill );
                        }
                    }
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
                    createBill ( BillType.Electriciteit );
                    break;
                }
            }

        private void Normal ( Bill bill )
            {
            // throw new NotImplementedException ( );
            }

        private void billOverDueDate ( Bill bill )
            {
            GlobalAudio.instance.SoundWarning ( );
            }

        /// <summary>
        /// Sends a reminder to pay the bill past its due date.
        /// </summary>
        /// <param name="bill"></param>
        private void AanManing ( Bill bill )
            {
            GlobalAudio.instance.SoundWarning ( );
            }

        /// <summary>
        /// Sends one last reminder to pay the bill past the duedate but with increased cost.
        /// </summary>
        /// <param name="bill"></param>
        private void Somatie ( Bill bill )
            {
            var fine = 100;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.ReplaceInfo ( bill );
            ui.AddWarning ( bill );

            if ( pressSpace != null )
                {
                pressSpace.SetActive ( true );
                if ( Input.GetKeyDown ( KeyCode.Space ) )
                    {
                    pressSpace.SetActive ( false );
                    }
                return;
                }
            }

        /// <summary>
        /// Sends a debt collector personally telling you to pay up, with increased costs.
        /// </summary>
        /// <param name="bill"></param>
        private void DagVaarding ( Bill bill )
            {
            var fine = 150;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            GlobalAudio.instance.SoundWarning ( );
            if ( pressSpace != null )
                {
                pressSpace.SetActive ( true );
                if ( Input.GetKeyDown ( KeyCode.Space ) )
                    {
                    pressSpace.SetActive ( false );
                    }
                return;
                }
            }

        /// <summary>
        /// The judge will get involved and a debt collector will bring the verdict.
        /// </summary>
        /// <param name="bill"></param>
        private void Vonnis ( Bill bill )
            {
            var fine = 250;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            GlobalAudio.instance.SoundWarning ( );
            if ( pressSpace != null )
                {
                pressSpace.SetActive ( true );
                if ( Input.GetKeyDown ( KeyCode.Space ) )
                    {
                    pressSpace.SetActive ( false );
                    }
                return;
                }
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

        /// <summary>
        /// Comparison between the current time and the set due dates in the bill class.
        /// </summary>
        private void DueDateCheck ( )
            {
            foreach ( var bill in Bills )
                {
                if ( TimeManager.currentTime == bill.DueDate && TimeManager.currentTime < bill.Aanmaning )
                    {
                    billOverDueDate ( bill );
                    }
                else if ( TimeManager.currentTime == bill.Aanmaning && TimeManager.currentTime < bill.Somatie )
                    {
                    AanManing ( bill );
                    }
                else if ( TimeManager.currentTime == bill.Somatie && TimeManager.currentTime < bill.Dagvaarding )
                    {
                    Somatie ( bill );
                    }
                else if ( TimeManager.currentTime == bill.Dagvaarding && TimeManager.currentTime < bill.Vonnis )
                    {
                    DagVaarding ( bill );
                    }
                else if ( TimeManager.currentTime == bill.Vonnis && TimeManager.currentTime < bill.Beslag )
                    {
                    Vonnis ( bill );
                    }
                else if ( TimeManager.currentTime == bill.Beslag )
                    {
                    Beslag ( bill );
                    }
                else
                    {
                    Normal ( bill );
                    }
                }
            }

        /// <summary>
        /// This handles the movement and envelope position to gameobjects
        /// </summary>
        private void EnvelopeHandler ( )
            {
            float step = speed * Time.deltaTime;
            foreach ( GameObject envelope in envelopes )
                {
                if ( spawnZone.transform.childCount == 0 )
                    {
                    if ( envelope )
                        {
                        envelope.transform.position = Vector3.MoveTowards ( envelope.transform.position, spawnZone.transform.position, step );
                        float distance = Vector3.Distance ( envelope.transform.position, spawnZone.transform.position );
                        if ( distance <= 1f )
                            {
                            envelope.transform.SetParent ( spawnZone.transform, false );
                            }
                        return;
                        }
                    }
                }
            }
        /// <summary>
        /// Keeps check of the bills in the scene that are pending to be paid.
        /// </summary>
        private void BillCounter ( )
            {
            _textBillCounter.text = "Rekeningen in wachtrij";
            _textBillCounterNumber.text = string.Format ( "{0}", Inbox.transform.childCount );
            _textBillCounter.GetComponentInChildren<SpriteRenderer> ( ).enabled = true;
            if ( Inbox.transform.childCount == 0 )
                {
                _textBillCounterNumber.text = " ";
                _textBillCounter.GetComponentInChildren<SpriteRenderer> ( ).enabled = false;
                _textBillCounter.text = " ";
                }
            }
        }
    }