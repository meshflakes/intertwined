using System;
using Controls;
using UnityEngine;

namespace Camera
{
    public class DefaultCameraCalculator
    {
        private readonly Transform _p1;
        private readonly Transform _p2;

        private readonly float _zoom;
        private readonly float _zoomStartDist;
        private readonly float _maxDist;
        private Vector3 _cameraOffset;
        private readonly float _cameraRotationSpeed;
        
        private readonly GameInputs _input;
        
        public DefaultCameraCalculator(Transform p1, Transform p2, float zoom, float zoomStartDist, 
                float maxDist, Vector3 cameraOffset, float cameraRotationSpeed)
        {
            _p1 = p1;
            _p2 = p2;
            _zoom = zoom;
            _zoomStartDist = zoomStartDist;
            _maxDist = maxDist;
            _cameraOffset = cameraOffset;
            _cameraRotationSpeed = cameraRotationSpeed;
            
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInputs>();
        }

        public void UpdateDefaultCameraPositionAndRotation(Vector3 currentCameraPos, out Vector3 targetPosition, out Quaternion targetRotation)
        {
            PlayerControlledRotation();
            
            var p1Position = _p1.position;
            var p2Position = _p2.position;
            
            var midpoint = (p1Position + p2Position) / 2f;
            targetRotation = Quaternion.LookRotation(midpoint - currentCameraPos);

            var distance = (p1Position - p2Position).magnitude;
            distance = Math.Min(distance, _maxDist);

            var zoomModifier = distance > _zoomStartDist ? distance * _zoom : 1;
        
            targetPosition = midpoint + _cameraOffset * zoomModifier; 
        }

        private void PlayerControlledRotation()
        {
            RotateCam(_input.CameraRotation * _cameraRotationSpeed * Time.deltaTime);
        }
        
        private void RotateCam(float angle)
        {
            _cameraOffset = Quaternion.AngleAxis(angle, Vector3.up) * _cameraOffset;
        }
    }
}