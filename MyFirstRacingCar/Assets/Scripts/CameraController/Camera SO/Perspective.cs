using UnityEngine;

namespace CameraController
{
    [CreateAssetMenu(menuName = "Camera/Perspective")]
    public class Perspective : ScriptableObject
    {
        [Header("Mode")] [SerializeField] private CameraMode mode;
        [Header("Position")] [SerializeField] private Vector3 position;
        [Header("Rotation")] [SerializeField] private Vector3 rotation;

        public Vector3 GetPosition()
        {
            return position;
        }
        
        public Vector3 GetRotation()
        {
            return rotation;
        }

        public CameraMode GetCameraMode()
        {
            return mode;
        }
    }
}