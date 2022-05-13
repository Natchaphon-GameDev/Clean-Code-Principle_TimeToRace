using System;
using UnityEngine;

namespace CameraController
{
    public class CameraPerspective : MonoBehaviour
    {
        [Header("Scriptable Perspective Setter")] 
        [SerializeField] private Perspective menuCamera;
        [SerializeField] private Perspective fontCamera;
        [SerializeField] private Perspective topCamera;
        [SerializeField] private Perspective backCamera;

        [NonSerialized]
        public Vector3 Offset;

        private Vector3 _nitroOffset;
        private CameraMode _currentCameraMode;

        public CameraMode GetCameraMode()
        {
            return _currentCameraMode;
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
            _currentCameraMode = perspective.GetCameraMode();
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
                    Offset = fontCamera.GetPosition();
                    break;
                case CameraMode.TopCamera:
                    SetPerspective(topCamera);
                    Offset = topCamera.GetPosition();
                    AddNitroOffset();
                    break;
                case CameraMode.BackCamera:
                    SetPerspective(backCamera);
                    Offset = backCamera.GetPosition();
                    AddNitroOffset();
                    break;
                case CameraMode.NitroCamera:
                    SetNitroOffset();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

         private void SetNitroOffset()
         {
             Offset = _nitroOffset;
         }
         
         private void AddNitroOffset()
         {
             _nitroOffset = Offset + new Vector3(0, 2, -2);
         }
    }
}