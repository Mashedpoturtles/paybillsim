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

        public void CreateBill ( )
            {
            foreach ( KeyValuePair<int, Bill> pair in Billholder )
                {
                if ( !InfoHolder.ContainsKey ( pair.Key ) )
                    {
                    if ( pair.Value.escalation != EscalationType.Ok )
                        return;

                    pair.Value.IsShown = true;

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

        public void CreateUILogic ( string billInformationText, int billID )
            {
            InfoHolder.Add ( billID, ( GameObject ) Instantiate ( Resources.Load ( "billInfo" ) ) );
            InfoHolder [ billID ].transform.FindChild ( "billInformation" ).GetComponent<Text> ( ).text = billInformationText;



            Button buttonPay = InfoHolder [ billID ].GetComponentInChildren<Button> ( );
            buttonPay.onClick.AddListener ( ( ) => PayBill ( buttonPay.name ) );

            buttonPay.name = billID.ToString ( );

            InfoHolder [ billID ].transform.SetParent ( SpawnZone.transform, false );
            }



        public void Start ( )
            {
            canvas = GameObject.FindWithTag ( "Canvas" ).GetComponent<Canvas> ( );
            Application.runInBackground = true;

            TimeManager.OnDayChange += IssueBill;

            }

        public void BillPayedInWarning ( int billId )
            {


            Billholder.Remove ( billId );
            Destroy ( InfoHolder [ billId ] );
            InfoHolder.Remove ( billId );
            }

        public void IssueBill ( )
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

        public int GetKey ( )
            {
            for ( int i = 0 ; i < Billholder.Count + 1 ; i++ )
                {
                if ( !Billholder.ContainsKey ( i ) )
                    return i;
                }
            return 0;
            }

        public void PayBill ( string billId )
            {
            if ( Money.instance.currentMoney >= Billholder [ int.Parse ( billId ) ].Amount )
                {
                canAfford = true;
                Money.instance.currentMoney -= Billholder [ int.Parse ( billId ) ].Amount;
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