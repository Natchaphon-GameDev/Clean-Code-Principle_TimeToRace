using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace LapSystem
{
    public class LapCheckPoint : MonoBehaviour
    {
        [SerializeField] private int index;

        private void Awake()
        {
            Debug.Assert(index > 0 ,"index Can't be equal or under the zero");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<LapCheck>())
            {
                var playerCar = other.GetComponent<LapCheck>();

                //index - 1 because player can't be revert the lap
                if (playerCar.CheckpointLapIndex == index - 1)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.ButtonClicked);
                    UiManager.Instance.ShowEachLap(); //Show each lap time 
                    playerCar.SetCheckPointIndex(index); //set new lap index
                    // Debug.Log(playerCar.CheckpointLapIndex);
                }
            }
        }
    }
}
//Please Give A to 1620701795 senPai :)