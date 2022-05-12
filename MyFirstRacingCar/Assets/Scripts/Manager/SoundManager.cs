using System;
using System.Collections;
using System.Collections.Generic;
using CarController;
using LapSystem;
using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<SoundClip> soundClips;
        
        [SerializeField] private float minPitchForMotor = 0.05f; //Min pitch sound motor
        
        private float pitchFromCar; //Velocity form car
        
        public static SoundManager Instance { get; private set; }

        public void Awake()
        {
            Debug.Assert(soundClips != null && soundClips.Count != 0, "Sound clips need to setup");
            Debug.Assert(minPitchForMotor > 0, "minPitchForMotor Can't be equal or under the zero");

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this); //Do not Destroy this when reset hierarchy
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            //Play BGM
            Play(Sound.MenuBGM);

            UiManager.Instance.OnStarted += SwitchBGMSound;
            UiManager.Instance.OnMainManu += SwitchBGMSound;
            UiManager.Instance.OnMainManu += StopFinishLapSound;
            UiManager.Instance.OnButtonClicked += ButtonClickedSound;
            FinishLapCheckPoint.Instance.OnLapTimerStart += StartRace;
            FinishLapCheckPoint.Instance.OnGameEnded += StopLoopCarSound;
            UiManager.Instance.OnMainManu += StopLoopCarSound;
        }
        
        private void StopLoopCarSound()
        {
            //Debug sound loop on game ended
            Stop(Sound.Break);
            Stop(Sound.MotorLoop);
        }

        private void StopFinishLapSound()
        {
            //Debug Loop sound play in menu 
            Stop(Sound.NewBestScore);
            Stop(Sound.FinishRace);
        }

        private void SwitchBGMSound()
        {
            //Switch sound BGM 
            if (GetSoundClip(Sound.MenuBGM).AudioSource.isPlaying)
            {
                Stop(Sound.MenuBGM);
                Play(Sound.InGameBGM);
            }
            else if (GetSoundClip(Sound.InGameBGM).AudioSource.isPlaying)
            {
                Stop(Sound.InGameBGM);
                Play(Sound.MenuBGM);
            }
        }
        
        private void ButtonClickedSound()
        {
            //SoundClick
            Play(Sound.ButtonClicked);
        }
        
        private void StartRace()
        {
            //Sound Gun start
            Play(Sound.StartRace);
        }
        
        public enum Sound
        {
            //SoundClip
            MenuBGM,
            InGameBGM,
            MotorLoop,
            ButtonClicked,
            Break,
            CarDrift,
            FinishRace,
            StartRace,
            Turbo,
            NewBestScore
        }

        [Serializable]
        public class SoundClip
        {
            //Init of every SoundClip
            public Sound Sound;
            public AudioClip AudioClip;
            [Range(0, 2)] public float SoundVolume;
            public bool Loop = false;
            [HideInInspector] public AudioSource AudioSource;
        }

        public void SlowMotionSound(Sound sound)
        {
            //Slow sound when car was in the air
            GetSoundClip(sound).AudioSource.pitch = 0.6f;
        }
        
        public void NormalSpeedSound(Sound sound)
        {
            //Back to normal speed
            GetSoundClip(sound).AudioSource.pitch = 1;
        }

        public void Play(Sound sound)
        {
            //Play Sound system
            var soundClip = GetSoundClip(sound);
            if (soundClip.AudioSource == null)
            {
                soundClip.AudioSource = gameObject.AddComponent<AudioSource>();
            }
            soundClip.AudioSource.clip = soundClip.AudioClip;
            soundClip.AudioSource.volume = soundClip.SoundVolume;
            soundClip.AudioSource.loop = soundClip.Loop;
            soundClip.AudioSource.Play();
        }

        public void ControlMotorSound(Sound sound)
        {
            //Easy way to Control Engine sound realistic
            var soundClip = GetSoundClip(sound);
            soundClip.AudioSource.pitch = minPitchForMotor;
            pitchFromCar = PlayerCarController.Instance.CurrentCarSpeed;
            
            if (pitchFromCar < minPitchForMotor)
            {
                soundClip.AudioSource.pitch = minPitchForMotor;
            }
            else
            {
                soundClip.AudioSource.pitch = pitchFromCar;
            }
        }
        
        public void Stop(Sound sound)
        {
            //Stop Sound system
            var soundClip = GetSoundClip(sound);
            if (soundClip.AudioSource == null)
            {
                soundClip.AudioSource = gameObject.AddComponent<AudioSource>();
            }
            soundClip.AudioSource.volume = soundClip.SoundVolume;
            soundClip.AudioSource.loop = soundClip.Loop;
            soundClip.AudioSource.Stop();
        }

        public void Pause(Sound sound,bool toggle)
        {
            //Stop and resume Sound system
            var soundClip = GetSoundClip(sound);
            soundClip.AudioSource.clip = soundClip.AudioClip;
            soundClip.AudioSource.volume = soundClip.SoundVolume;
            soundClip.AudioSource.loop = soundClip.Loop;
            if (toggle)
            {
                soundClip.AudioSource.Pause();
            }
            else
            {
                soundClip.AudioSource.UnPause();
            }
        }
        
        private SoundClip GetSoundClip(Sound sound)
        {
            //find sound in list
            foreach (var soundClip in soundClips)
            {
                if (soundClip.Sound == sound)
                {
                    return soundClip;
                }
            }
            return null;
        }
    }
}

