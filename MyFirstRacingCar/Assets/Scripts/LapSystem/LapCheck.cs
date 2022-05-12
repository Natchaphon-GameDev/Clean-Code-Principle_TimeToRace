using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace LapSystem
{
    public class LapCheck : MonoBehaviour
    {
        public int Round { get; private set; }
        public int CheckpointLapIndex { get; private set; }
        public static LapCheck Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            //Set Lap and Round
            Round = 1;
            CheckpointLapIndex = 0;
        }

        private void Start()
        {
            UiManager.Instance.OnRestarted += RestartRound;
        }

        public void SetCheckPointIndex(int value)
        {
            CheckpointLapIndex = value;
        }
        
        public void NextRound()
        {
            Round++; 
        }

        private void RestartRound()
        {
            Round = 1; //reset round
        }
    }
}
//Please Give A to 1620701795 senPai :)