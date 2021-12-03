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
        
        // TODO: use / remove
        // public Collider camTrigger;
        // public float followTimeDelta = 0.8f;
    
        private void Start()
        {
            var cameraTransform = transform;
            
            _cameraOffset = cameraTransform.position - ((p1.position + p2.position) / 2f);
            _playerFollowCamera =
                new PlayerFollowCamera(p1, p2, zoom, zoomStartDist, maxDist, smoothness, _cameraOffset, cameraTransform,
                    cameraRotationSpeed);

            _defaultCameraCalculator = new DefaultCameraCalculator(p1, p2, zoom, zoomStartDist, maxDist, _cameraOffset,
                cameraRotationSpeed);
        }

        private void Update()
        {
            _defaultCameraCalculator.UpdateDefaultCameraPositionAndRotation(transform.position, 
                out var position, out var rotation);
            
            switch (_currentCamera)
            {
                case CameraTypes.PlayerFollow:
                    _playerFollowCamera.UpdateCamera(position, rotation);
                    break;
                case CameraTypes.CameraSequence:
                    if (_sequenceCamera.YieldingCameraControl)
                    {
                        _sequenceCamera = null;
                        _currentCamera = CameraTypes.PlayerFollow;
                    }
                    else _sequenceCamera.UpdateCamera(position, rotation);

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
