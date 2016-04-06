using Assets.BillSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BillSystem
    {
    public class BillManager : MonoBehaviour
        {
        public static Canvas canvas;
        public static bool canAfford;
        public static CanvasGroup canvasGroup;
        public static Dictionary<int, Bill> Billholder = new Dictionary<int, Bill> ( );
        public static Dictionary<int, GameObject> InfoHolder = new Dictionary<int, GameObject> ( );
        [SerializeField]
        private RectTransform SpawnZone;

        private void CreateBill ( )
            {
            foreach ( KeyValuePair<int, Bill> pair in Billholder )
                {
                if ( !InfoHolder.ContainsKey ( pair.Key ) )
                    {
                    string billInformation;
                    billInformation = string.Format ( "Bill type: {0} \\n Issue date: {1} \\n  Due date: {2} \\n Amount to pay: {3} \\n",
                                        Enum.GetName ( typeof ( BillType ), pair.Value.Type ),
                                        pair.Value.IssueDate.ToString ( "d" ),
                                        pair.Value.DueDate.ToString ( "d" ),
                                        pair.Value.Amount ).Replace ( "\\n", "\n" );

                    CreateUILogic ( billInformation, pair.Key );
                    }
                }
            }

        private void CreateUILogic ( string billInformationText, int billID )
            {
            InfoHolder.Add ( billID, ( GameObject ) Instantiate ( Resources.Load ( "billInfo" ) ) );
            InfoHolder [ billID ].transform.FindChild ( "billInformation" ).GetComponent<Text> ( ).text = billInformationText;

            Button buttonPay = InfoHolder [ billID ].GetComponentInChildren<Button> ( );
            buttonPay.onClick.AddListener ( ( ) => PayBill ( buttonPay.name ) );

            buttonPay.name = billID.ToString ( );

            InfoHolder [ billID ].transform.SetParent ( SpawnZone.transform, false );
            }

        private void Start ( )
            {
            canvas = GameObject.FindWithTag ( "Canvas" ).GetComponent<Canvas> ( );
            Application.runInBackground = true;
            TimeManager.OnDayChange += IssueBill;
            }

        private void IssueBill ( )
            {
            switch ( TimeManager.currentTime.DayOfWeek )
                {
                case DayOfWeek.Tuesday:
                    Billholder.Add ( GetKey ( ), new Bill ( BillType.Internet ) );
                    CreateBill ( );
                    break;
                case DayOfWeek.Thursday:
                    Billholder.Add ( GetKey ( ), new Bill ( BillType.Electricity ) );
                    CreateBill ( );
                    break;
                }
            }

        private int GetKey ( )
            {
            for ( int i = 0 ; i < Billholder.Count + 1 ; i++ )
                {
                if ( !Billholder.ContainsKey ( i ) )
                    return i;
                }
            return 0;
            }

        private void CheckDueDate ( string billId )
            {
            var bill = Billholder [ Convert.ToInt32 ( billId ) ];
            if ( TimeManager.currentTime > bill.DueDate )

                switch ( bill.dueLevel )
                    {
                    case 1:
                        Debug.Log ( "case 1" );
                        break;
                    }
            }

        private void PayBill ( string billId )
            {
            var bill = Billholder [ Convert.ToInt32 ( billId ) ];
            if ( Money.instance.currentMoney >= bill.Amount )
                {
                canAfford = true;
                Money.instance.currentMoney -= bill.Amount;
                }
            else
                {
                canAfford = false;
                }

            if ( canAfford )
                {
                Billholder.Remove ( Convert.ToInt32 ( billId ) );
                Destroy ( InfoHolder [ Convert.ToInt32 ( billId ) ] );
                InfoHolder.Remove ( Convert.ToInt32 ( billId ) );
                }
            else
                {
                Debug.Log ( "You cant afford to pay this bill!" );
                }
            }
        }
    }

