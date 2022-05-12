using System;
using System.Collections;
using System.Collections.Generic;
using CarController;
using LapSystem;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float playerFrontWheelMotorForce;
        [SerializeField] private float playerRearWheelMotorForce;
        [SerializeField] private float playerBreakForce;
        [SerializeField] private float playerTurboEngine;
        [SerializeField] private float trackDrag;
        [SerializeField] private float groundDrag;
        [SerializeField] private float airDrag;
        [SerializeField] private float downForce;
        
        [SerializeField] private PlayerCarController player;
        
        [NonSerialized] public PlayerCarController PlayerSpawned;

        public static GameManager Instance { get; private set; } //Singleton Pattern

        private void Awake()
        {
            Debug.Assert(playerFrontWheelMotorForce > 0 ,"playerFrontWheelMotorForce Can't be equal or under the zero");
            Debug.Assert(playerRearWheelMotorForce > 0 ,"playerRearWheelMotorForce Can't be equal or under the zero");
            Debug.Assert(playerBreakForce > 0 ,"playerBreakForce Can't be equal or under the zero");
            Debug.Assert(playerTurboEngine > 0 ,"playerTurboEngine Can't be equal or under the zero");
            Debug.Assert(trackDrag != 0 ,"Now trackDrag is zero");
            Debug.Assert(groundDrag != 0 ,"Now groundDrag is zero");
            Debug.Assert(airDrag != 0 ,"Now airDrag is zero");
            Debug.Assert(downForce > 0 ,"downForce Can't be equal or under the zero");
            Debug.Assert(player != null, "player can't be null");

            //Singleton Pattern
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
            UiManager.Instance.OnStarted += PlayerSpawn;
            UiManager.Instance.OnRestarted += InGameRestart;
            FinishLapCheckPoint.Instance.OnGameEnded += WaitForDestory;
        }

        private void PlayerSpawn()
        {
            //Spawn PlayerCar at the prefab position
            PlayerSpawned = Instantiate(player);
            PlayerSpawned.Init(playerFrontWheelMotorForce, playerRearWheelMotorForce, playerBreakForce,
                playerTurboEngine,trackDrag, groundDrag, airDrag, downForce);
            SoundManager.Instance.Play(SoundManager.Sound.MotorLoop);
        }

        private void WaitForDestory()
        {
            //Delay for destory player car for only game ended
            Invoke("PlayerDestroyed", 0.5f);
        }

        public void PlayerDestroyed()
        {
            Destroy(PlayerSpawned.gameObject);
        }

        private void InGameRestart()
        {
            PlayerDestroyed();
            //Delay because if spawn immediately the camera can't keep up 
            Invoke("PlayerSpawn", 0.0001f);
        }
    }
}
//Please Give A to 1620701795 senPai :)