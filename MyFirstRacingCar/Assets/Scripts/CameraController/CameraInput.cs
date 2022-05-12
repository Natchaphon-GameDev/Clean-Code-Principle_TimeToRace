using System;
using LapSystem;
using Manager;
using UnityEngine;

namespace CameraController
{
    public class CameraInput : MonoBehaviour
    {
        [SerializeField] private KeyCode fontCameraKey = KeyCode.C;
        [SerializeField] private KeyCode topCameraKey = KeyCode.V;
        [SerializeField] private KeyCode backCameraKey = KeyCode.X;
        [SerializeField] private KeyCode nitroBoostKey = KeyCode.LeftShift;

        private CameraPerspective perspective;

        private void Awake()
        {
            SetupReference();
        }

        private void SetupReference()
        {
            perspective = GetComponent<CameraPerspective>();
        }

        private void Update()
        {
            if (UiManager.Instance.IsGameStart && FinishLapCheckPoint.Instance.IsGameEnded == false)
            {
                SetInput();
            }
        }

        protected void SetInput()
        {
            ;
            
            if (Input.GetKeyDown(fontCameraKey))
            {
                perspective.SwitchPerspective(CameraMode.FontCamera);
            }
            else if (Input.GetKeyDown(topCameraKey))
            {
                perspective.SwitchPerspective(CameraMode.TopCamera);
            }
            else if (Input.GetKeyDown(backCameraKey))
            {
                perspective.SwitchPerspective(CameraMode.BackCamera);
            }

            if (Input.GetKey(nitroBoostKey))
            {
                perspective.SwitchPerspective(CameraMode.NitroCamera);
            }
            
        }
    }
}