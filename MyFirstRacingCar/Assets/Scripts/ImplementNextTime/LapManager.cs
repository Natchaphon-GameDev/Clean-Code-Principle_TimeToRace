using System;
using System.Collections;
using System.Collections.Generic;
using CarController;
using LapSystem;
using UnityEngine;

namespace ImplenemtNextTime //TODO: Implement Next Time
{
    public class LapManager : MonoBehaviour
    {
        public enum Lap
        {
            StartLap,
            Lap1,
            Lap2,
            Lap3,
            Lap4,
            Lap5,
            FinishLap
        }

        [Serializable]
        public struct LapCount
        {
            public Lap LapName;
            public GameObject LapCollider;
            public int order;
        }
        
        public static LapManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        

        [SerializeField] private List<LapCount> lapTriggers;

        public int GetLapOrder(int lap)
        {
            foreach (var lapTrigger in lapTriggers)
            {
                if (lapTrigger.order == lap)
                {
                    return lap;
                }
            }
            return default(int);
        }
        
        public int SetLapOrder(int order)
        {
            foreach (var lapTrigger in lapTriggers)
            {
            }
            return default(int);
        }

        // private void Start()
        // {
        //     foreach (var lapTrigger in lapTriggers)
        //     {
        //         lapTrigger.LapCollider.gameObject.SetActive(false);
        //     }
        //
        //     GetLapOrder(Lap.StartLap).LapCollider.gameObject.SetActive(true);
        // }

        // public void LapStart()
        // {
        //     switch (i)
        //         {
        //             case 1:
        //                 GetLapPosition(Lap.Lap1).LapCollider.gameObject.SetActive(true);
        //                 i = 2;
        //                 return;
        //             case 2:
        //                 GetLapPosition(Lap.Lap2).LapCollider.gameObject.SetActive(true);
        //                 i = 3;
        //                 return;
        //             case 3:
        //                 GetLapPosition(Lap.Lap3).LapCollider.gameObject.SetActive(true);
        //                 i = 4;
        //                 return;
        //             case 4:
        //                 GetLapPosition(Lap.Lap4).LapCollider.gameObject.SetActive(true);
        //                 i = 5;
        //                 return;
        //             case 5:
        //                 GetLapPosition(Lap.Lap5).LapCollider.gameObject.SetActive(true);
        //                 i = 6;
        //                 return;
        //             case 6:
        //                 GetLapPosition(Lap.FinishLap).LapCollider.gameObject.SetActive(true);
        //                 i = 7;
        //                 return;
        //             // default:
        //             //     GetLapPosition(Lap.StartLap).LapCollider.gameObject.SetActive(true);
        //             //     return;
        //         }
        // }
    }
}