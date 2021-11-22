using System;
using Controls;
using UnityEngine;

namespace Camera
{
    
    public class PlayerFollowCamera : CameraController
    {
        private readonly Transform _p1;
        private readonly Transform _p2;

        private readonly float _zoom;
        private readonly float _zoomStartDist;
        private readonly float _maxDist;
        private readonly float _smoothness;
        private readonly Transform _cameraTransform;
        private Vector3 _cameraOffset;
        private readonly float _cameraRotation;

        private Vector3 _targetCameraPos;
        private GameInputs _input;
        
        public PlayerFollowCamera(Transform p1, Transform p2, float zoom, float zoomStartDist, 
                float maxDist, float smoothness, Vector3 cameraOffset, Transform cameraTransform, float cameraRotation)
        {
            _p1 = p1;
            _p2 = p2;
            _zoom = zoom;
            _zoomStartDist = zoomStartDist;
            _maxDist = maxDist;
            _smoothness = smoothness;
            _cameraOffset = cameraOffset;
            _cameraTransform = cameraTransform;
            _cameraRotation = cameraRotation;
            
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInputs>();
        }

        public override void UpdateCamera()
        {
            PlayerControlledRotation();
            
            var p1Position = _p1.position;
            var p2Position = _p2.position;
            
            _cameraTransform.LookAt((p1Position + p2Position) / 2f);

            var midpoint = (p1Position + p2Position) / 2f;
            var distance = (p1Position - p2Position).magnitude;
            distance = Math.Min(distance, _maxDist);

            var zoomModifier = distance > _zoomStartDist ? distance * _zoom : 1;
        
            _targetCameraPos = midpoint + _cameraOffset * zoomModifier; 
            
            _cameraTransform.position = Vector3.Slerp(_cameraTransform.position, _targetCameraPos, _smoothness * Time.deltaTime);
        }

        private void PlayerControlledRotation()
        {
            RotateCam(_input.CameraRotation * _cameraRotation * Time.deltaTime);
        }
        
        private void RotateCam(float angle)
        {
            _cameraOffset = Quaternion.AngleAxis(angle, Vector3.up) * _cameraOffset;
        }
    }
}