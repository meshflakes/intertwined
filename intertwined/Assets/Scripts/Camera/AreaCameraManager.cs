using System;
using UnityEngine;

namespace Camera
{
    public class AreaCameraManager
    {
        private readonly Transform _cameraTransform;
        private readonly CameraManager _cameraManager;
        
        private readonly Vector3 _targetPosition;
        private readonly Quaternion _targetRotation;
        
        private readonly float _animationDuration;
        private AreaCameraController _cameraController;

        private readonly float _epsilonTime = 0.1f;
        private float _movementOkTime = 0f;
        
        public AreaCameraManager(Transform targetTransform, float animationDuration)
        {
            _targetPosition = targetTransform.position;
            _targetRotation = targetTransform.rotation;
            _animationDuration = animationDuration;
            
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _cameraTransform = mainCamera.transform;
            _cameraManager = mainCamera.GetComponent<CameraManager>();
        }

        public void StartNewCameraSequence()
        {
            if (_cameraController != null)
            {
                Debug.LogError("camera is currently active");
                return;
            }
            
            _cameraController = new AreaCameraController(_targetPosition, _targetRotation, _cameraTransform,
                _animationDuration);

            _cameraManager.SetCameraController(_cameraController);
            _cameraController.AreaCamActive = true;
            _movementOkTime = Time.time + _animationDuration + _epsilonTime;
        }

        public bool GetCameraActive()
        {
            if (_cameraController == null) return false;

            return _cameraController.AreaCamActive;
            // return _cameraController.AreaCamActive && Time.time > _movementOkTime;
        }

        public void EndCameraSequence()
        {
            _cameraController.AreaCamActive = false;
            _cameraController = null;
            _movementOkTime = Time.time + _animationDuration + _epsilonTime;
        }
    }
}