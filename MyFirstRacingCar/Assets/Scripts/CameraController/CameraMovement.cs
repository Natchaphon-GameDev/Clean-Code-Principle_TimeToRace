using Manager;
using UnityEngine;

namespace CameraController
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Camera Speed Setter")] 
        [SerializeField] private float translateSpeed = 10f;
        [SerializeField] private float rotationSpeed = 5f;

        [SerializeField] private float addTranslateSpeed = 15f;
        [SerializeField] private float addRotateSpeed = 5f;
        [SerializeField] private float currentTranslateSpeed;
        [SerializeField] private float currentRotateSpeed;
        
        private bool _isAddSpeedDone;
        private CameraPerspective _perspective;

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
            _perspective = GetComponent<CameraPerspective>();
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
            HandleTranslation();
            HandleRotation();
            AddSpeedToBackCamera();
        }

        private void HandleRotation()
        {
            var direction = GameManager.Instance.PlayerSpawned.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, currentRotateSpeed * Time.deltaTime);
        }

        private void HandleTranslation()
        {
            var targetPosition = GameManager.Instance.PlayerSpawned.transform.TransformPoint(_perspective.Offset);
            transform.position =
                Vector3.Lerp(transform.position, targetPosition, currentTranslateSpeed * Time.deltaTime);
        }

        private void AddSpeedToBackCamera()
        {
            if (_perspective.GetCameraMode() == CameraMode.BackCamera)
            {
                if (_isAddSpeedDone)
                {
                    return;
                }

                currentTranslateSpeed += addTranslateSpeed;
                currentRotateSpeed += addRotateSpeed;
                _isAddSpeedDone = true;
            }
            else
            {
                currentTranslateSpeed = translateSpeed;
                currentRotateSpeed = rotationSpeed;
                _isAddSpeedDone = false;
            }
        }
    }
}