using System;
using System.Collections.Generic;
using LapSystem;
using Manager;
using UnityEngine;

namespace CameraController
{
    public class CameraPerspective : MonoBehaviour
    {
        [Header("Scriptable Perspective Setter")] 
        [SerializeField] private Perspective menuCamera = default;
        [SerializeField] private Perspective fontCamera = default;
        [SerializeField] private Perspective topCamera = default;
        [SerializeField] private Perspective backCamera = default;

      public Vector3 offset;

        private CameraMode currentCameraMode;

        public CameraMode GetCameraMode()
        {
            return currentCameraMode;
        }

        private void Awake()
        {
            DebugSetting();
        }

        private void DebugSetting()
        {
            Debug.Assert(menuCamera != null, "menuCamera can't be null");
            Debug.Assert(fontCamera != null, "fontCamera can't be null");
            Debug.Assert(topCamera != null, "topCamera can't be null");
            Debug.Assert(backCamera != null, "backCamera can't be null");
        }


        private void Start()
        {
            SwitchPerspective(CameraMode.MenuCamera);
        }

        private void SetPerspective(Perspective perspective)
        {
            transform.position = perspective.GetPosition();
            transform.rotation = Quaternion.Euler(perspective.GetRotation());
            currentCameraMode = perspective.GetCameraMode();
        }

        public void SwitchPerspective(CameraMode cameraMode)
        {
            switch (cameraMode)
            {
                case CameraMode.MenuCamera:
                    SetPerspective(menuCamera);
                    break;
                case CameraMode.FontCamera:
                    SetPerspective(fontCamera);
                    offset = fontCamera.GetPosition();
                    break;
                case CameraMode.TopCamera:
                    SetPerspective(topCamera);
                    offset = topCamera.GetPosition();
                    break;
                case CameraMode.BackCamera:
                    SetPerspective(backCamera);
                    offset = backCamera.GetPosition();
                    break;
                case CameraMode.NitroCamera:
                    // AddNitroOffset();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //TODO: This Method is looping Do it Once
        // private void AddNitroOffset()
        // {
        //     offset += new Vector3(0, 2, -2);
        // }
    }
}