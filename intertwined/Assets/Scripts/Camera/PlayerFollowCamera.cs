using System;
using UnityEngine;

namespace Camera
{
    
    public class PlayerFollowCamera : CameraController
    {
        private Transform _p1;
        private Transform _p2;

        private float _zoom;
        private float _zoomStartDist;
        private float _maxDist;
        private float _smoothness;
        private Vector3 _cameraOffset;
        private Transform _cameraTransform;

        private Vector3 _targetCameraPos;
        
        public PlayerFollowCamera(Transform p1, Transform p2, float zoom, float zoomStartDist, 
                float maxDist, float smoothness, Vector3 cameraOffset, Transform cameraTransform)
        {
            _p1 = p1;
            _p2 = p2;
            _zoom = zoom;
            _zoomStartDist = zoomStartDist;
            _maxDist = maxDist;
            _smoothness = smoothness;
            _cameraOffset = cameraOffset;
            _cameraTransform = cameraTransform;
        }

        public override void UpdateCamera()
        {
            PlayerControlledRotation();
            PlayerControlledRotation();
            
            var p1Position = _p1.position;
            var p2Position = _p2.position;
            
            _cameraTransform.LookAt((p1Position + p2Position) / 2f);

            var midpoint = (p1Position + p2Position) / 2f;
            var distance = (p1Position - p2Position).magnitude;
            distance = Math.Min(distance, _maxDist);

            var zoomModifier = distance > _zoomStartDist ? distance * _zoom : 1;
        
            _targetCameraPos = midpoint + _cameraOffset * zoomModifier; 
            
            // TODO: change smoothness to have some dependence on delta time (for consistent smoothing across frame rates)
            _cameraTransform.position = Vector3.Slerp(_cameraTransform.position, _targetCameraPos, _smoothness);
        }

        private void PlayerControlledRotation()
        {
            // TODO: change to get input from input system instead of manual key check
            if (Input.GetMouseButton(0))
            {
                RotateCam(0.2f);
            } else if (Input.GetMouseButton(1))
            {
                RotateCam(-0.2f);
            }
        }
        
        private void RotateCam(float angle)
        {
            _cameraOffset = Quaternion.AngleAxis(angle, Vector3.up) * _cameraOffset;
        }
    }
}