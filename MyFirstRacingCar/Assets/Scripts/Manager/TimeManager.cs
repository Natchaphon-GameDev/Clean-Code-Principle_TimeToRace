using System;
using System.Collections;
using System.Collections.Generic;
using LapSystem;
using UnityEngine;

namespace Manager
{
    public class TimeManager : MonoBehaviour
    {
        private bool toggle;
        [Range(0,3)][SerializeField] private float timeScale;
        [Range(0,1)][SerializeField] private float defaultTimeScale;
        // Start is called before the first frame update

        public static TimeManager Instance { get; private set; }
        
        private void Awake()
        {
            Debug.Assert(timeScale == 0 ,"timeScale is not zero");
            Debug.Assert(defaultTimeScale != 0 ,"defaultTimeScale is zero");
            
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
            UiManager.Instance.OnRestarted += UnPauseGame;
        }

        // Update is called once per frame
        private void Update()
        {
            //Check Is Player In game?
            if (UiManager.Instance.IsGameStart)
            {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    SoundManager.Instance.Play(SoundManager.Sound.ButtonClicked);
                    
                    //Toggle button
                    toggle = !toggle;
                    //Pause && Unpause BGM
                    SoundManager.Instance.Pause(SoundManager.Sound.MotorLoop,toggle);
                    SoundManager.Instance.Pause(SoundManager.Sound.InGameBGM,toggle);
                    SoundManager.Instance.Pause(SoundManager.Sound.Break,toggle);
                    SoundManager.Instance.Pause(SoundManager.Sound.CarDrift,toggle);
                    Time.timeScale = toggle? timeScale : defaultTimeScale;
                    
                    if (toggle)
                    {
                        UiManager.Instance.ShowInGameMenu();
                    }
                    else
                    {
                        UiManager.Instance.HideInGameMenu();
                    }
                }
            }
        }

        public void UnPauseGame()
        {
            //Toggle if other input call to unPause
            toggle = !toggle;
            SoundManager.Instance.Pause(SoundManager.Sound.InGameBGM,toggle);
            SoundManager.Instance.Pause(SoundManager.Sound.MotorLoop,toggle);
            SoundManager.Instance.Pause(SoundManager.Sound.Break,toggle);
            SoundManager.Instance.Pause(SoundManager.Sound.CarDrift,toggle);
            Time.timeScale = defaultTimeScale;
        }
    }
}
//Please Give A to 1620701795 senPai :)

