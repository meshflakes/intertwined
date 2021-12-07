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
            if (_cameraController != null) throw new Exception("camera is alr active");
            
            _cameraController = new AreaCameraController(_targetPosition, _targetRotation, _cameraTransform,
                _animationDuration);

            _cameraManager.SetCameraController(_cameraController);
            _cameraController.AreaCamActive = true;
        }

        public bool GetCameraActive()
        {
            if (_cameraController == null) return false;

            return _cameraController.AreaCamActive;
        }

        public void EndCameraSequence()
        {
            _cameraController.AreaCamActive = false;
            _cameraController = null;
        }
    }
}