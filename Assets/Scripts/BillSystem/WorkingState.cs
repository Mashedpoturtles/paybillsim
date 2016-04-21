using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.BillSystem
    {
    public class WorkingState : MonoBehaviour
        {
        [SerializeField]
        private Text  _WarningTextEnergy;
        [SerializeField]
        private Text _WarningTextNotworking;
        private Slider workSlider;
        [SerializeField]
        private BillManager billManager;
        [SerializeField]
        private Slider workingProgressSlider;
        [SerializeField]
        private Text energy;
        [SerializeField]
        private int workEnergy;
        [SerializeField]
        private float workSpeed;
        [SerializeField]
        public WorkState currentState;
        public enum WorkState { Niet, Gewoon, Hard, Workaholic, niet }
        public Canvas canvas;

        private void Start ( )
            {
            UI ( );
            SetWorkState ( WorkState.Niet );
            workEnergy = 300;
            Invoke ( "Energy", 1 );
            workingProgressSlider.interactable = false;
            workingProgressSlider.maxValue = 300;
            workingProgressSlider.minValue = 0;
            }

        private void Update ( )
            {
            workingProgressSlider.value = workEnergy;
            energy.text = "Energy: " + workEnergy;
            WarningLowEnergy ( );
            WarningNotWorking ( );          
            }
        private void WarningLowEnergy ( )
            {
            if(workEnergy > 300)
                {
                workEnergy = 300;
                }
            if ( workEnergy <= 200 )
                {
                _WarningTextEnergy.text = "Zorg dat je niet te hard werkt!";
                }
            if ( workEnergy <= 100 )
                {
                _WarningTextEnergy.text = "Werk wat langzamer.";
                }
            }
        private void WarningNotWorking()
            {
            if( workSlider.value == 1 )
                {
                _WarningTextNotworking.text = "Je bent niet aan het werk, gebruik de slider om geld te verdienen.";
                }
            else
                {
                _WarningTextNotworking.text = "";
                }
            }
        private void SetWorkState ( int workIntensity )
            {
            WorkState newWorkState;

            switch ( workIntensity )
                {
                case 0:
                    newWorkState = WorkState.niet;
                    workEnergy = 0;
                    break;

                case 1:
                    newWorkState = WorkState.Niet;
                    break;

                case 2:
                    newWorkState = WorkState.Gewoon;
                    break;

                case 3:
                    newWorkState = WorkState.Hard;
                    break;

                case 4:
                    newWorkState = WorkState.Workaholic;
                    break;

                default:
                    newWorkState = WorkState.Niet;
                    break;
                }
            currentState = newWorkState;
            StartCoroutine ( newWorkState.ToString ( ) + "State" );
            }

        private void SetWorkState ( WorkState newWorkState )
            {
            currentState = newWorkState;
            StartCoroutine ( newWorkState.ToString ( ) + "State" );
            }

        private int AddMoney ( int money )
            {
            return money;
            }

        IEnumerator NietState ( )
            {
            while ( currentState == WorkState.Niet )
                {
                yield return new WaitForSeconds ( 3 );
                if(workEnergy < 300)
                    {
                    workEnergy += 4;
                    }
                }
            yield return null;
            }

        IEnumerator GewoonState ( )
            {
            while ( currentState == WorkState.Gewoon )
                {
                yield return new WaitForSeconds ( 6 );
                if ( workEnergy < 300 )
                    {
                    workEnergy += 4;
                    }
                Money.instance.currentMoney += AddMoney ( 30 );
                }
            yield return null;
            }

        IEnumerator HardState ( )
            {
            while ( currentState == WorkState.Hard )
                {
                yield return new WaitForSeconds ( 4 );
                if ( workEnergy > 0 )
                    {
                    workEnergy -= 6;
                    }
                Money.instance.currentMoney += AddMoney ( 35 );
                }
            yield return null;
            }

        IEnumerator WorkaholicState ( )
            {
            while ( currentState == WorkState.Workaholic )
                {
                yield return new WaitForSeconds ( 2 );
                if ( workEnergy > 0 )
                    {
                    workEnergy -= 12;
                    }
                Money.instance.currentMoney += AddMoney ( 35 );
                }
            yield return null;
            }

        IEnumerator nietState ( )
            {
            while ( currentState == WorkState.niet )
                {
                workSlider.interactable = false;
                yield return null;
                }
            workSlider.interactable = true;
            yield return null;
            }

        private void Energy ( )
            {
            if ( workEnergy <= 0 )
                {

                SetWorkState ( WorkState.niet );
                CancelInvoke ( "Energy" );
                workEnergy = 0;
                _WarningTextEnergy.text = "Je hebt geen energie meer om te werken.";
                return;
                }
            else if ( workEnergy >= 1 )
                {
                Invoke ( "Energy", 1 );
                return;
                }         
            }

        private void UI ( )
            {
            workSlider = GameObject.FindWithTag ( "WorkSlider" ).GetComponent<Slider> ( );
            workingProgressSlider = GameObject.FindWithTag ( "WorkingProgressBar" ).GetComponent<Slider> ( );
            workSlider.onValueChanged.AddListener ( delegate { SetWorkState ( ( int ) workSlider.value ); } );
            }
        }
    }