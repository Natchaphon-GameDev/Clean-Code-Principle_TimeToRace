using System;
using System.Collections;
using System.Collections.Generic;
using LapSystem;
using UnityEngine;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        //MaxValue because I want to keep minimum score
        private int minOfMode1 = int.MaxValue;
        private int secOfMode1 = 60;
        private float millisecOfMode1 = 10;

        private int minOfMode2 = int.MaxValue;
        private int secOfMode2 = 60;
        private float millisecOfMode2 = 10;

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
        }

        public string BestScoreOfMode1()
        {
            //Keep minimum score system
            if (LapTime.Instance.MIN <= minOfMode1)
            {
                if (LapTime.Instance.Sec <= secOfMode1 && LapTime.Instance.MIN <= minOfMode1)
                {
                    if (LapTime.Instance.MilliSec <= millisecOfMode1 && LapTime.Instance.Sec <= secOfMode1)
                    {
                        SoundManager.Instance.Play(SoundManager.Sound.NewBestScore);
                        minOfMode1 = LapTime.Instance.MIN;
                        secOfMode1 = LapTime.Instance.Sec;
                        millisecOfMode1 = LapTime.Instance.MilliSec;
                    }
                    else if (LapTime.Instance.MilliSec > millisecOfMode1 && LapTime.Instance.Sec < secOfMode1)
                    {
                        SoundManager.Instance.Play(SoundManager.Sound.NewBestScore);
                        minOfMode1 = LapTime.Instance.MIN;
                        secOfMode1 = LapTime.Instance.Sec;
                        millisecOfMode1 = LapTime.Instance.MilliSec;
                    }
                }
            }
            //Return to string because I want to keep my score private
            return $"Your Best of 1 Laps Time Score : {minOfMode1}:{secOfMode1}:{millisecOfMode1:F0}";
        }

        public string BestScoreOfMode2()
        {
            if (LapTime.Instance.MIN <= minOfMode2)
            {
                if (LapTime.Instance.Sec <= secOfMode2 && LapTime.Instance.MIN <= minOfMode2)
                {
                    if (LapTime.Instance.MilliSec <= millisecOfMode2 && LapTime.Instance.Sec <= secOfMode2)
                    {
                        SoundManager.Instance.Play(SoundManager.Sound.NewBestScore);
                        minOfMode2 = LapTime.Instance.MIN;
                        secOfMode2 = LapTime.Instance.Sec;
                        millisecOfMode2 = LapTime.Instance.MilliSec;
                    }
                    else if (LapTime.Instance.MilliSec > millisecOfMode2 && LapTime.Instance.Sec < secOfMode1)
                    {
                        SoundManager.Instance.Play(SoundManager.Sound.NewBestScore);
                        minOfMode2 = LapTime.Instance.MIN;
                        secOfMode2 = LapTime.Instance.Sec;
                        millisecOfMode2 = LapTime.Instance.MilliSec;
                    }
                }
            }
            return $"Your Best of 3 Laps Time Score : {minOfMode2}:{secOfMode2}:{millisecOfMode2:F0}";
        }
    }
}