using System;
using LapSystem;
using Manager;
using UnityEngine;

namespace CameraController
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Camera Speed Setter")] 
        [SerializeField] private float translateSpeed = 10f;
        [SerializeField] private float rotationSpeed = 5f;

        [SerializeField] private float currentTranslateSpeed;
        [SerializeField] private float currentRotateSpeed;
        
        private CameraPerspective perspective;

        private void Awake()
        {
            SetupReference();
            DebugSetting();
        }

        private void DebugSetting()
        {
            Debug.Assert(translateSpeed > 0, "translateSpeed Can't be equal or under the zero");
            Debug.Assert(rotationSpeed > 0, "rotationSpeed Can't be equal or under the zero");
        }

        private void SetupReference()
        {
            perspective = GetComponent<CameraPerspective>();
        }

        private void Start()
        {
            SetupStartValue();
        }

        private void SetupStartValue()
        {
            currentTranslateSpeed = translateSpeed;
            currentRotateSpeed = rotationSpeed;
        }

        private void LateUpdate()
        {
            if (UiManager.Instance.IsGameStart && FinishLapCheckPoint.Instance.IsGameEnded == false)
            {
                HandleTranslation();
                HandleRotation();
                // AddSpeedToBackCamera();
            }
        }

        private void HandleRotation()
        {
            var direction = GameManager.Instance.PlayerSpawned.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, currentRotateSpeed * Time.deltaTime);
        }

        private void HandleTranslation()
        {
            var targetPosition = GameManager.Instance.PlayerSpawned.transform.TransformPoint(perspective.offset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, currentTranslateSpeed * Time.deltaTime);
        }

        //TODO: This Method is looping Do it Once
        // private void AddSpeedToBackCamera()
        // {
        //     if (perspective.GetCameraMode() == CameraMode.BackCamera)
        //     {
        //         currentTranslateSpeed += 15f;
        //         currentRotateSpeed += 5f;
        //     }
        //     else
        //     {
        //         currentTranslateSpeed = translateSpeed;
        //         currentRotateSpeed = rotationSpeed;
        //     }
        // }
    }
}