using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;

namespace LapSystem
{
    public class LapTime : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI minText;
        [SerializeField] private TextMeshProUGUI secText;
        [SerializeField] private TextMeshProUGUI milliSecText;
        
        public int MIN { get; private set; }
        public int Sec { get; private set; }
        public float MilliSec { get; private set; }
        public static LapTime Instance { get; private set; }
        
        private void Awake()
        {
            Debug.Assert(minText != null ,"minText can't be null");
            Debug.Assert(secText != null ,"secText can't be null");
            Debug.Assert(milliSecText != null ,"milliSecText can't be null");
            
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
            FinishLapCheckPoint.Instance.OnGameEnded += WaitForShowScore;
            UiManager.Instance.OnRestarted += TimeReset;
            UiManager.Instance.OnMainManu += TimeReset;
        }

        private void Update()
        {
            //Wait for Player cross the Start line
            if (FinishLapCheckPoint.Instance.IsStartLap == 1)
            {
                //Timer system
                MilliSec += (Time.deltaTime * 10);
                milliSecText.text = $"{MilliSec:F0}";
                if (MilliSec >= 9)
                {
                    MilliSec = 0;
                    Sec++;
                }
                
                if (Sec < 10)
                {
                    secText.text = $"0{Sec}:";
                }
                else if (Sec >= 10 && Sec <= 59)
                {
                    secText.text = $"{Sec}:";
                }
                else if (Sec == 60)
                {
                    Sec = 0;
                    MIN++;
                }

                if (MIN < 10)
                {
                    minText.text = $"0{MIN}:";
                }
                else if (MIN >= 10 && MIN <= 59)
                {
                    minText.text = $"{MIN}:";
                }
            }
        }

        private void WaitForShowScore()
        {
            //wait for UIManager get the timer and to score
            Invoke("TimeReset",1);
        }

        private void TimeReset()
        {
            //Reset timer
            MIN = 0;
            Sec = 0;
            MilliSec = 0;
        }
    }
}
//Please Give A to 1620701795 senPai :)
