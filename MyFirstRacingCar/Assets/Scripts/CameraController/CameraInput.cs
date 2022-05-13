using UnityEngine;

namespace CameraController
{
    public class CameraInput : MonoBehaviour
    {
        [SerializeField] private KeyCode fontCameraKey = KeyCode.C;
        [SerializeField] private KeyCode topCameraKey = KeyCode.V;
        [SerializeField] private KeyCode backCameraKey = KeyCode.X;
        [SerializeField] private KeyCode nitroBoostKey = KeyCode.LeftShift;

        private CameraPerspective _perspective;

        private void Awake()
        {
            SetupReference();
        }

        private void SetupReference()
        {
            _perspective = GetComponent<CameraPerspective>();
        }

        private void Update()
        {
            SetInput();
        }

        private void SetInput()
        {
            if (Input.GetKeyDown(fontCameraKey))
            {
                _perspective.SwitchPerspective(CameraMode.FontCamera);
            }
            else if (Input.GetKeyDown(topCameraKey))
            {
                _perspective.SwitchPerspective(CameraMode.TopCamera);
            }
            else if (Input.GetKeyDown(backCameraKey))
            {
                _perspective.SwitchPerspective(CameraMode.BackCamera);
            }

            if (Input.GetKey(nitroBoostKey))
            {
                _perspective.SwitchPerspective(CameraMode.NitroCamera);
            }
        }
    }
}