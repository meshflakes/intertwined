using Camera;
using UnityEngine;

namespace Interactable.Util
{
    /**
     * creates CameraSequenceController and sets it in the CameraController when required
     */
    public class CameraSequenceManager
    {
        private readonly Transform _cameraTransform;
        private readonly CameraManager _cameraManager;
        
        private readonly Vector3 _targetPosition;
        private readonly Quaternion _targetRotation;
        
        private readonly float _animationDuration;
        private readonly float _minimumTimeBeforeReturn;
        
        public CameraSequenceManager(Transform targetTransform, float animationDuration, float minimumTimeBeforeReturn)
        {
            _targetPosition = targetTransform.position;
            _targetRotation = targetTransform.rotation;
            _animationDuration = animationDuration;
            _minimumTimeBeforeReturn = minimumTimeBeforeReturn;
            
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _cameraTransform = mainCamera.transform;
            _cameraManager = mainCamera.GetComponent<CameraManager>();
        }

        public void StartNewCameraSequence()
        {
            var cameraSequence = new CameraSequenceController(_targetPosition, _targetRotation, _cameraTransform,
                _animationDuration, _minimumTimeBeforeReturn);

            _cameraManager.SetCameraController(cameraSequence);
        }
    }
}