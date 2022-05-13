using System;
using LapSystem;
using Manager;
using UnityEngine;

namespace CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float translateSpeed = 10;
        [SerializeField] private float rotationSpeed = 5;
        [SerializeField] private Vector3 camera1 = new Vector3(0, 2, -5);
        [SerializeField] private Vector3 camera2 = new Vector3(0, 5, -10);
        [SerializeField] private Vector3 camera3 = new Vector3(0, 2, 4);

        private bool changeToCamera1;
        private bool changeToCamera2;
        private bool changeToCamera3;
        private bool booster;
        
        private int checkCamera;//Check where is Camera mode now

        private Vector3 offset;
        private Vector3 cameraMenu = new Vector3(-83.737f, 1.216f, -4.03f);

        private void Awake()
        {
            Debug.Assert(translateSpeed > 0 ,"translateSpeed Can't be equal or under the zero");
            Debug.Assert(rotationSpeed > 0, "rotationSpeed Can't be equal or under the zero");
            Debug.Assert(camera1 != Vector3.zero,"camera1 offset Can't be equal or under the zero");
            Debug.Assert(camera2 != Vector3.zero,"camera2 offset Can't be equal or under the zero");
            Debug.Assert(camera3 != Vector3.zero,"camera3 offset Can't be equal or under the zero");
        }

        private void Start()
        {
            UiManager.Instance.OnMainManu += SetMenuCam;
            changeToCamera1 = true;
            checkCamera = 1;
        }

        private void Update()
        {
            //To debug null ref in loop
            if (UiManager.Instance.IsGameStart && FinishLapCheckPoint.Instance.IsGameEnded == false)
            {
                GetInput();
                ChangeCamera();
                BoosterCamera();
            }
        }
        
        private void LateUpdate()
        {
            //To debug null ref in loop
            if (UiManager.Instance.IsGameStart && FinishLapCheckPoint.Instance.IsGameEnded == false)
            {
                HandleTranslation();
                HandleRotation();
            }
        }
        
        private void SetMenuCam()
        {
            if (!(Camera.main is null))
            {
                //Set Camera to mainMenu
                Camera.main.transform.position = cameraMenu;
                Camera.main.transform.rotation = Quaternion.Euler(7.516f, 38.926f, 0.9210001f);
            }
        }
        
        private void HandleRotation()
        {
            //Rotation Camera
            var direction = GameManager.Instance.PlayerSpawned.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        private void HandleTranslation()
        {
            //Following Camera
            var targetPosition = GameManager.Instance.PlayerSpawned.transform.TransformPoint(offset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
        }

        private void GetInput()
        {
            changeToCamera1 = Input.GetKeyDown(KeyCode.C);
            changeToCamera2 = Input.GetKeyDown(KeyCode.V);
            changeToCamera3 = Input.GetKeyDown(KeyCode.X);
            booster = Input.GetKey(KeyCode.LeftShift);
        }


        private void ChangeCamera()
        {
            //Change Camera mode
            if (changeToCamera1)
            {
                checkCamera = 1;
                offset = camera1;
                translateSpeed = 10;
                rotationSpeed = 5;
            }
            else if (changeToCamera2)
            {
                checkCamera = 2;
                offset = camera2;
                translateSpeed = 10;
                rotationSpeed = 5;
            }
            else if (changeToCamera3)
            {
                checkCamera = 3;
                offset = camera3;
                translateSpeed = 25;
                rotationSpeed = 10;
            }
        }

        private void BoosterCamera()
        {
            switch (booster)
            {
                //Boost Camera each Camera mode
                case true when checkCamera == 1:
                    offset = new Vector3(0, 4, -7);
                    break;
                case true when checkCamera == 2:
                    offset = new Vector3(0, 7, -12);
                    break;
                //Back to current Camera mode
                case false when checkCamera == 1:
                    offset = camera1;
                    break;
                case false when checkCamera == 2:
                    offset = camera2;
                    break;
            }
        }
    }
}
//Please Give A to 1620701795 senPai :)