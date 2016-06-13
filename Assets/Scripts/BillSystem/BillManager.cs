using Assets.BillSystem;
using System.Collections.Generic;
using System.Linq;
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
    public static List<Bill> instalmentQueue { get; set; }
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
        Bills = new List<Bill> ( );
        instalmentQueue = new List<Bill> ( );
        envelopes = new List<GameObject> ( );
        envelope = ( Resources.Load<GameObject> ( "Envelope" ) );
        Application.runInBackground = true;
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
        GameManager.Instance.OnDayChange += onDayChanged;
        GameManager.Instance.OnDayChange += DueDateCheck;
        }

    void Update ( )
        {
        EnvelopeHandler ( );
        BillCounter ( );
        DebugPanel.Log ( "realtime look at the instalments in queue", instalmentQueue.Count );
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
            Text buttonText = GameObject.FindWithTag ( "pausebutton" ).GetComponentInChildren<Text> ( );
            if ( buttonText.text != "Start!" )
                {
                if ( GameManager.Instance.IsPaused == true )
                    {
                    GameManager.Instance.UnPause ( );
                    }
                }
            }
        if ( Money.instance.currentMoney >= bill.RecievedCost || Money.instance.currentMoney <= bill.RecievedCost )
            {
            Money.instance.currentMoney += bill.RecievedCost;
            GlobalAudio.instance.SoundPaidBill ( );
            Destroy ( bill.Object );
            Bills.Remove ( bill );
            Text buttonText = GameObject.FindWithTag ( "pausebutton" ).GetComponentInChildren<Text> ( );
            if ( buttonText.text != "Start!" )
                {
                if ( GameManager.Instance.IsPaused == true )
                    {
                    GameManager.Instance.UnPause ( );
                    }
                }
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
                BillUI ui = newBill.Object.GetComponentInChildren<BillUI> ( );
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
    /// <summary>
    /// Overload of the createBill method
    /// </summary>
    /// <param name="bill"></param>
    /// <returns></returns>
    private Bill createBill ( Bill bill )
        {
        if ( spawnZone )
            {
            DebugPanel.Log ( " this got passed into the create instalment ", bill );
            Bill newBill = bill;
            DebugPanel.Log ( " Bill newbill is made and got assigned bill and now looks like ", newBill );
            Bills.Add ( newBill );
            DebugPanel.Log ( " the newbill got added to the blls list and we are at a count of ", Bills.Count );
            newBill.IssueDate = GameManager.currentTime;
            newBill.DueDate = GameManager.currentTime.AddDays ( 31 );

            GameObject envelope = Instantiate ( Resources.Load<GameObject> ( "Envelope" ) );
            envelopes.Add ( envelope );

            envelope.transform.SetParent ( Inbox.transform, false );
            GlobalAudio.instance.SoundBillPending ( );
            GameObject billObject = Instantiate ( Resources.Load<GameObject> ( "billprefab" ) );
            newBill.Object = billObject;
            DebugPanel.Log ( " newbil.object looks like this after being assigned billObject ", newBill.Object );
            BillUI ui = billObject.GetComponentInChildren<BillUI> ( );
            DebugPanel.Log ( "newbill cost on creation ", newBill.Cost );
            newBill.Object.transform.SetParent ( envelope.transform.FindChild ( "SpawnZone" ).transform, false );
            ui.SetUI ( newBill );
            DebugPanel.Log ( " hello ui script what can you tell us? ", ui );
            DebugPanel.Log ( "newbill cost after ui set ", newBill.Cost );
            return newBill;
            }
        return null;
        }

    public void SplitBillsInTerms ( Bill bill, int amount )
        {
        int newBillCost = bill.Cost / amount;

        for ( int i = 0 ; i < amount ; i++ )
            {
            DebugPanel.Log ( "bill is split into : ", amount );
            Bill tempBill = new Bill ( bill.Type );
            DebugPanel.Log ( "suposed new bill cost after splitting", newBillCost );
            DebugPanel.Log ( "tempbill cost before the split newbillcost is assigned ", tempBill.Cost );
            tempBill.Cost = newBillCost;
            DebugPanel.Log ( "tempbill cost after the split newbillcost is assigned ", tempBill.Cost );
            DebugPanel.Log ( "current debt before newbillcost is added to it ", Debt.instance.currentDebt );
            Debt.instance.currentDebt += newBillCost;
            DebugPanel.Log ( "current debt after newbillcost is added to it ", Debt.instance.currentDebt );
            instalmentQueue.Add ( tempBill );
            DebugPanel.Log ( "tempbil got added ", tempBill );
            }

        if ( bill.Object.transform.parent == spawnZone )
            {
            Destroy ( bill.Object.transform.parent.parent );
            envelopes.Remove ( bill.Object.transform.parent.gameObject );
            }

        Debt.instance.currentDebt -= bill.Cost;
        DebugPanel.Log ( "current debt after the old bill cost is detracted from it ", Debt.instance.currentDebt );
        Destroy ( bill.Object );
        Bills.Remove ( bill );
        }

    /// <summary>
    /// This method is used to set the day of the month on which a bill is ment to instantiate.
    /// </summary>
    private void onDayChanged ( )
        {
        if ( GameManager.currentTime.Day == 28 && instalmentQueue.Count > 0 ) // look at this.
            {

            createBill ( instalmentQueue [ 0 ] );
            DebugPanel.Log ( " looking at the instalmentque's first object ", instalmentQueue [ 0 ] );
            DebugPanel.Log ( "the cost of the instalment now that its been created", instalmentQueue [ 0 ].Cost );
            DebugPanel.Log ( " the stamp count before the instalment got created", instalmentQueue.Count );
            instalmentQueue.RemoveAt ( 0 );
            DebugPanel.Log ( " the stamp count after the instalment got created", instalmentQueue.Count );
            }

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
            bill.RecievedCost = 0;
            bill.Cost += Random.Range ( 100, 500 );
            BillUI ui = bill.Object.GetComponentInChildren<BillUI> ( );
            ui.ReplaceInfoToEventNegative ( bill );
            Debt.instance.currentDebt += bill.Cost;
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
            bill.Cost = 150;
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
            bill.Cost = 275;
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
