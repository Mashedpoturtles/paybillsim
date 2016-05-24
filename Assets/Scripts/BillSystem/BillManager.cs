using Assets.BillSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the behaviour of instantiating bills and their behaviour once instantiated.
/// </summary>
public class BillManager : MonoBehaviour
    {
    [SerializeField]
    private GameObject spawnZone;
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
    private Text _textBillCounter;

    private void OnEnable ( )
        {
        Initialize ( );
        }
    private void Start ( )
        {
        if ( instance == null )
            {
            instance = this;
            }
        else if ( instance != this )
            {
            Destroy ( gameObject );
            return;
            }
        }

    /// <summary>
    /// Setting everything that needs to be prepared in start.
    /// </summary>
    private void Initialize ( )
        {
        Bills = new List<Bill> ( );
        envelopes = new List<GameObject> ( );
        envelope = ( Resources.Load<GameObject> ( "Envelope" ) );
        Application.runInBackground = true;
        GameManager.Instance.OnDayChange += onDayChanged;
        GameManager.Instance.OnDayChange += DueDateCheck;
        }

    void Update ( )
        {
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
            Debt.instance.currentDebt -= bill.Cost;
            GlobalAudio.instance.SoundPaidBill ( );
            Destroy ( bill.Object );
            Bills.Remove ( bill );
            GameManager.Instance.UnPause ( );
            }
        if ( Money.instance.currentMoney >= bill.RecievedCost || Money.instance.currentMoney <= bill.RecievedCost )
            {
            Money.instance.currentMoney += bill.RecievedCost;
            GlobalAudio.instance.SoundPaidBill ( );
            Destroy ( bill.Object );
            Bills.Remove ( bill );
            GameManager.Instance.UnPause ( );
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
    private Bill createBill ( BillType type )
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
                    ui.SetUI ( newBill );
                    }
                }
            return newBill;
            }
        return null;
        }

    public void SplitBillsInTerms ( Bill bill, int amount )
        {

        int newBillCost = bill.Cost / amount;
        Debug.Log ( "newbillcost" + newBillCost );
        Debug.Log ( "billcost" + bill.Cost );
        for ( int i = 0 ; i < amount ; i++ )
            {
            Bill tempBill = createBill ( bill.Type );
            tempBill.Cost = newBillCost;

            BillUI ui = tempBill.Object.GetComponentInChildren<BillUI> ( );
            ui.ReplaceInfo ( tempBill );
            }

        if ( bill.Object.transform.parent == spawnZone )
            {
            Destroy ( bill.Object.transform.parent.parent );
            envelopes.Remove ( bill.Object.transform.parent.gameObject );
            }


        Debt.instance.currentDebt -= bill.Cost;
        Destroy ( bill.Object );
        Bills.Remove ( bill );
        }

    /// <summary>
    /// This method is used to set the day of the month on which a bill is ment to instantiate.
    /// </summary>
    private void onDayChanged ( )
        {
        if ( GameManager.currentTime.Day == 21 )
            {
            createBill ( BillType.Internet );
            }

        if ( GameManager.currentTime.Day == 24 )
            {
            createBill ( BillType.Electriciteit );
            }

        if ( GameManager.currentTime.Day == 3 )
            {
            createBill ( BillType.GasEnLicht );
            }

        if ( GameManager.currentTime.Day == 13 )
            {
            createBill ( BillType.ZorgVerzekering );
            }

        if ( GameManager.currentTime.Day == 18 )
            {
            createBill ( BillType.Telefoon );
            }

        if ( GameManager.currentTime.Day == Random.Range ( 1, 30 ) )
            {
            createBill ( BillType.Event );
            }
        }

    private void RandomEventNegative ( Bill bill )
        {
        if ( bill.Type == BillType.Event )
            {
            bill.Cost += Random.Range ( 100, 500 );
            bill.RecievedCost = 0;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.ReplaceInfoToEventNegative ( bill );
            }
        }
    private void RandomEventPositive ( Bill bill )
        {
        if ( bill.Type == BillType.Event )
            {
            bill.DueDate = GameManager.currentTime.AddDays ( 0 );
            bill.Cost = 0;
            bill.RecievedCost += Random.Range ( 100, 500 );
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.ReplaceInfoToEventPositive ( bill );
            }
        }

    private void Normal ( Bill bill )
        {
        if ( bill.Type == BillType.Electriciteit )
            {
            bill.Cost = 50;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.SetUI ( bill );
            }
        else if ( bill.Type == BillType.Internet )
            {
            bill.Cost = 75;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.SetUI ( bill );
            }
        else if ( bill.Type == BillType.GasEnLicht )
            {
            bill.Cost = 200;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.SetUI ( bill );
            }
        else if ( bill.Type == BillType.Telefoon )
            {
            bill.Cost = 60;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.SetUI ( bill );
            }
        else if ( bill.Type == BillType.ZorgVerzekering )
            {
            bill.Cost = 175;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.SetUI ( bill );
            }
        }

    private void billOverDueDate ( Bill bill )
        {

        }

    /// <summary>
    /// Sends a reminder to pay the bill past its due date.
    /// </summary>
    /// <param name="bill"></param>
    private void AanManing ( Bill bill )
        {

        }

    /// <summary>
    /// Sends one last reminder to pay the bill past the duedate but with increased cost.
    /// </summary>
    /// <param name="bill"></param>
    private void Somatie ( Bill bill )
        {
        if ( bill.Type != BillType.Event )
            {
            var fine = 100;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.ReplaceInfo ( bill );
            ui.AddWarning ( bill );
            }
        }

    /// <summary>
    /// Sends a debt collector personally telling you to pay up, with increased costs.
    /// </summary>
    /// <param name="bill"></param>
    private void DagVaarding ( Bill bill )
        {
        if ( bill.Type != BillType.Event )
            {
            var fine = 150;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            }
        }

    /// <summary>
    /// The judge will get involved and a debt collector will bring the verdict.
    /// </summary>
    /// <param name="bill"></param>
    private void Vonnis ( Bill bill )
        {
        if ( bill.Type != BillType.Event )
            {
            var fine = 250;
            bill.Cost += fine;
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui = bill.Object.GetComponent<BillUI> ( );
            ui.ReplaceInfo ( bill );
            }
        }

    /// <summary>
    /// Seizure of funds and possessions.
    /// </summary>
    /// <param name="bill"></param>
    private void Beslag ( Bill bill )
        {
        if ( bill.Type != BillType.Event )
            {
            Money.instance.currentMoney = 0;
            gameInfo.text = "Verloren.";
            LoadLevelsOnClick.instance.LoadMenuScene ( );

            GlobalAudio.instance.SoundGameOver ( );
            }
        }

    /// <summary>
    /// Comparison between the current time and the set due dates in the bill class.
    /// </summary>
    private void DueDateCheck ( )
        {
        foreach ( Bill bill in Bills )
            {
            if ( GameManager.currentTime == bill.DueDate && GameManager.currentTime < bill.Aanmaning )
                {
                billOverDueDate ( bill );
                }
            else if ( GameManager.currentTime == bill.Aanmaning && GameManager.currentTime < bill.Somatie )
                {
                AanManing ( bill );
                }
            else if ( GameManager.currentTime == bill.Somatie && GameManager.currentTime < bill.Dagvaarding )
                {
                Somatie ( bill );
                }
            else if ( GameManager.currentTime == bill.Dagvaarding && GameManager.currentTime < bill.Vonnis )
                {
                DagVaarding ( bill );
                }
            else if ( GameManager.currentTime == bill.Vonnis && GameManager.currentTime < bill.Beslag )
                {
                Vonnis ( bill );
                }
            else if ( GameManager.currentTime == bill.Beslag )
                {
                Beslag ( bill );
                }
            else if ( GameManager.currentTime == bill.IssueDate )
                {
                Normal ( bill );
                Debt.instance.currentDebt += bill.Cost;
                if ( bill.Type == BillType.Event )
                    {
                    float percentPositive = 0.5f;
                    float percentNegative = 0.6f;
                    var randomVal = Random.value;
                    if ( randomVal >= percentPositive && randomVal < percentNegative )
                        {
                        RandomEventPositive ( bill );
                        }
                    else if ( randomVal > percentNegative )
                        {
                        RandomEventNegative ( bill );
                        }
                    else
                        {
                        RandomEventPositive ( bill );
                        }
                    }
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
        _textBillCounter.GetComponentInChildren<Image> ( ).enabled = true;
        if ( Inbox.transform.childCount == 0 )
            {
            _textBillCounterNumber.text = " ";
            _textBillCounter.GetComponentInChildren<Image> ( ).enabled = false;
            _textBillCounter.text = " ";
            }
        }
    }
