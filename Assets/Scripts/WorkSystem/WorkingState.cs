using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.BillSystem
{
    public class WorkingState : MonoBehaviour
    {
        private Text workStateText;
        private Button buttonRefill;
        private Slider workSlider;


        [SerializeField]
        private int workEnergy;
        [SerializeField]
        public WorkState currentState;
        public enum WorkState { NotWorking, Average, Hard, OverDrive, Drained }

        private void Start()
        {
            UI();
            SetWorkState(WorkState.NotWorking);
            workEnergy = 300;
            Invoke("Energy", 1);
        }
        private void Update()
        {
            Debug.Log(workSlider.value);
        }

        private void SetWorkState(int workIntensity)
        {
            WorkState newWorkState;

            switch (workIntensity)
            {
                case 0:
                    newWorkState = WorkState.Drained;
                    break;

                case 1:
                    newWorkState = WorkState.NotWorking;
                    break;

                case 2:
                    newWorkState = WorkState.Average;
                    break;

                case 3:
                    newWorkState = WorkState.Hard;
                    break;

                case 4:
                    newWorkState = WorkState.OverDrive;
                    break;

                default:
                    newWorkState = WorkState.NotWorking;
                    Debug.Log("ERROR: The workIntensity is set to a number that is not an alternative!");
                    break;
            }

            currentState = newWorkState;
            StartCoroutine(newWorkState.ToString() + "State");

            Debug.Log(workIntensity.ToString());
        }

        private void SetWorkState(WorkState newWorkState)
        {
            currentState = newWorkState;
            StartCoroutine(newWorkState.ToString() + "State");
        }

        private int AddMoney(int money)
        {
            return money;
        }

        IEnumerator NotWorkingState()
        {
            while (currentState == WorkState.NotWorking)
            {
                workStateText.text = "working pace:" + currentState;
                yield return null;
            }
            yield return null;
        }

        IEnumerator AverageState()
        {
            while (currentState == WorkState.Average)
            {
                yield return new WaitForSeconds(3);
                workStateText.text = "working pace:" + currentState;
                workEnergy -= 3;
                Money.instance.currentMoney += AddMoney(10);

            }
            yield return null;
        }

        IEnumerator HardState()
        {
            while (currentState == WorkState.Hard)
            {
                yield return new WaitForSeconds(3);
                workStateText.text = "working pace:" + currentState;
                workEnergy -= 6;
                Money.instance.currentMoney += AddMoney(20);
            }
            yield return null;
        }

        IEnumerator OverDriveState()
        {
            while (currentState == WorkState.OverDrive)
            {
                yield return new WaitForSeconds(3);
                workStateText.text = "working pace:" + currentState;
                workEnergy -= 12;
                Money.instance.currentMoney += AddMoney(30);
            }
            yield return null;
        }

        IEnumerator DrainedState()
        {
            while (currentState == WorkState.Drained)
            {
                workStateText.text = "working pace:" + currentState;
                workSlider.interactable = false;
                yield return null;
            }
            workSlider.interactable = true;
            yield return null;
        }

        private void Energy()
        {
            if (workEnergy <= 0)
            {
                SetWorkState(WorkState.Drained);
                ButtonUI();
                CancelInvoke("Energy");
            }
            else if (workEnergy >= 0)
            {
                Invoke("Energy", 1);
            }
        }

        private void OnClickReplenishEnergy()
        {
            if (currentState == WorkState.Drained)
            {
                currentState = WorkState.NotWorking;
                workEnergy += 250;
                Destroy(buttonRefill.gameObject);
            }
            else
            {
                Debug.Log("You dont need to refill energy");
            }
        }

        private void UI()
        {
            workSlider = GameObject.FindWithTag("WorkSlider").GetComponent<Slider>();
            workStateText = workSlider.GetComponentInChildren<Text>();
            workSlider.onValueChanged.AddListener(delegate { SetWorkState((int)workSlider.value); });
        }
        private void ButtonUI()
        {
            buttonRefill = Instantiate(Resources.Load("EnergyRefill")) as Button;
            buttonRefill = GameObject.FindWithTag("buttonRefillEnergy").GetComponent<Button>();
            Canvas canvas = BillManager.canvas;
            canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
            buttonRefill.transform.SetParent(canvas.transform, false);
            buttonRefill.onClick.AddListener(() => OnClickReplenishEnergy());
        }
    }
}