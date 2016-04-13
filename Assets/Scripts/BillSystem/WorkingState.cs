﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.BillSystem
    {
    public class WorkingState : MonoBehaviour
        {
        private Text workStateText;
        private Slider workSlider;
        [SerializeField]
        private BillManager billManager;
        private Slider workingProgressSlider;
        [SerializeField]
        private Text energy;
        [SerializeField]
        private int workEnergy;
        [SerializeField]
        private float workTime;
        [SerializeField]
        private float workSpeed;
        [SerializeField]
        public WorkState currentState;
        public enum WorkState { NotWorking, Average, Hard, OverDrive, Drained }
        public Canvas canvas;

        private void Start ( )
            {
            UI ( );
            SetWorkState ( WorkState.NotWorking );
            workEnergy = 300;
            Invoke ( "Energy", 1 );
            workingProgressSlider.interactable = false;
            }

        private void Update ( )
            {
            workingProgressSlider.value = Mathf.MoveTowards ( workingProgressSlider.value, workTime, workSpeed );
            energy.text = "Energy: " + workEnergy;
            }

        private void SetWorkState ( int workIntensity )
            {
            WorkState newWorkState;

            switch ( workIntensity )
                {
                case 0:
                    newWorkState = WorkState.Drained;
                    workEnergy = 0;
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
                    Debug.Log ( "ERROR: The workIntensity is set to a number that is not an alternative!" );
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

        IEnumerator NotWorkingState ( )
            {
            while ( currentState == WorkState.NotWorking )
                {
                workTime = 0.0f;
                workStateText.text = "working pace:" + currentState;
                yield return new WaitForSeconds ( 3 );
                workEnergy += 6;
                }
            workTime = 0f;
            yield return null;
            }

        IEnumerator AverageState ( )
            {
            while ( currentState == WorkState.Average )
                {
                workStateText.text = "working pace:" + currentState;
                yield return new WaitForSeconds ( 2 );
                workSpeed = 0.015f;
                workTime = 100.0f;
                yield return new WaitForSeconds ( 4 );
                workEnergy += 6;
                Money.instance.currentMoney += AddMoney ( 30 );
                workTime = 0.0f;
                }
            workTime = 0f;
            yield return null;
            }

        IEnumerator HardState ( )
            {
            while ( currentState == WorkState.Hard )
                {
                workStateText.text = "working pace:" + currentState;
                yield return new WaitForSeconds ( 2 );
                workSpeed = 0.05f;
                workTime = 100.0f;
                yield return new WaitForSeconds ( 2 );
                workEnergy -= 6;
                Money.instance.currentMoney += AddMoney ( 45 );
                workTime = 0.0f;
                }
            workTime = 0f;
            yield return null;
            }

        IEnumerator OverDriveState ( )
            {
            while ( currentState == WorkState.OverDrive )
                {
                workStateText.text = "working pace:" + currentState;
                yield return new WaitForSeconds ( 1 );
                workSpeed = 0.2f;
                workTime = 100.0f;
                yield return new WaitForSeconds ( 1 );
                workEnergy -= 12;
                Money.instance.currentMoney += AddMoney ( 45 );
                workTime = 0.0f;
                }
            workTime = 0f;
            yield return null;
            }

        IEnumerator DrainedState ( )
            {
            while ( currentState == WorkState.Drained )
                {
                workStateText.text = "working pace:" + currentState;
                workTime = 0.0f;
                workSlider.interactable = false;
                yield return null;
                workTime = 0.0f;
                }
            workTime = 0f;
            workSlider.interactable = true;
            yield return null;
            }

        private void Energy ( )
            {
            if ( workEnergy <= 0 )
                {

                SetWorkState ( WorkState.Drained );
                CancelInvoke ( "Energy" );
                workEnergy = 0;
                Debug.Log ( "Game Over!" );
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
            workStateText = workSlider.GetComponentInChildren<Text> ( );
            workSlider.onValueChanged.AddListener ( delegate { SetWorkState ( ( int ) workSlider.value ); } );
            }
        }
    }