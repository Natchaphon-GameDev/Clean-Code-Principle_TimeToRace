using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace LapSystem
{
    public class FinishLapCheckPoint : MonoBehaviour
    {
        [SerializeField] private int finishLapCheckPoint;
        public event Action OnGameEnded;
        public event Action OnLapTimerStart;

        public int IsStartLap { get; private set; } //Check to start lap timer
        public bool IsGameEnded { get; private set; } //Stop update loop in camera
        
        public int NumberOfFinishRounds { get; private set; }
        
        public static FinishLapCheckPoint Instance { get; private set; }

        private void Awake()
        {
            //FinishLap index need to equal with last lap check index so this game have 6 Laps
            Debug.Assert(finishLapCheckPoint == 6,"FinishLap index need to equal with last lap check index");
            
            IsGameEnded = false; //Set the game not over yet
            IsStartLap = 0; //Stop lap timer 

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UiManager.Instance.OnStarted += NewGame;
            UiManager.Instance.OnRestarted += RestartLapTime;
            UiManager.Instance.OnMainManu += GameEnded;
            UiManager.Instance.OnMainManu += RestartLapTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Check Is car has a LapCheck Script?
            if (other.GetComponent<LapCheck>())
            {
                var playerCar = other.GetComponent<LapCheck>();

                //Start lap timer
                if (IsStartLap == 0)
                {
                    IsStartLap = 1;
                    OnLapTimerStart?.Invoke();
                }
                
                //Check that player complete the round
                if (playerCar.CheckpointLapIndex == finishLapCheckPoint)
                {
                    playerCar.SetCheckPointIndex(0); //Reset Lap
                    playerCar.NextRound(); //Change to the next round
                    UiManager.Instance.RoundCount(); //Update UI RoundCount
                }
                
                //Set number of finish rounds and +1 because game start at round 1
                if (LapCheck.Instance.Round == NumberOfFinishRounds + 1)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.FinishRace);
                    IsGameEnded = true; //stop camera update loop
                    IsStartLap = 0; 
                    OnGameEnded?.Invoke();
                }
            }
        }

        private void NewGame()
        {
            IsGameEnded = false; //Set Camera back to update
        }

        private void GameEnded()
        {
            IsGameEnded = true; //stop camera update loop
        }

        private void RestartLapTime()
        {
            IsStartLap = 0; //stop lap timer
        }

        public void SetLap(int lap)
        {
            NumberOfFinishRounds = lap; //Set mode
        }
    }
}
//Please Give A to 1620701795 senPai :)

