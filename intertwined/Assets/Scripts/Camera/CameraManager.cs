using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        public Transform p1;
        public Transform p2;

        public float zoom = 0.165f;
        public float zoomStartDist = 6;
        public float maxDist = 12;
        public float smoothness = 0.5f;
        [FormerlySerializedAs("cameraRotation")] public float cameraRotationSpeed = 30;
    
        private Vector3 _cameraOffset;
        private CameraController _playerFollowCamera;
        private CameraController _sequenceCamera;
        private CameraTypes _currentCamera = CameraTypes.PlayerFollow;
        private DefaultCameraCalculator _defaultCameraCalculator;
    
        private void Start()
        {
            var cameraTransform = transform;
            _playerFollowCamera = new PlayerFollowCamera(p1, p2, smoothness, cameraTransform);
            
            _cameraOffset = cameraTransform.position - ((p1.position + p2.position) / 2f);
            _defaultCameraCalculator = new DefaultCameraCalculator(p1, p2, zoom, zoomStartDist, maxDist, _cameraOffset,
                cameraRotationSpeed);
        }

        private void Update()
        {
            _defaultCameraCalculator.UpdateDefaultCameraPositionAndRotation(transform.position, 
                out var targetPosition, out var targetRotation);
            
            switch (_currentCamera)
            {
                case CameraTypes.PlayerFollow:
                    _playerFollowCamera.UpdateCamera(targetPosition, targetRotation);
                    break;
                case CameraTypes.CameraSequence:
                    if (_sequenceCamera.YieldingCameraControl)
                    {
                        _sequenceCamera = null;
                        _currentCamera = CameraTypes.PlayerFollow;
                    }
                    else _sequenceCamera.UpdateCamera(targetPosition, targetRotation);

                    break;
                
                default:
                    throw new NotImplementedException("Default type for camera update not implemented");
            }
        }

        public void SetCameraController(CameraController sequenceCamera)
        {
            if (_currentCamera != CameraTypes.PlayerFollow) 
                throw new Exception("Cannot start camera sequence when not on default camera");
            
            _sequenceCamera = sequenceCamera;
            _currentCamera = CameraTypes.CameraSequence;
        }
    }
}
