using System;
using System.Collections.Generic;
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
        
        List<GameObject> hiddenItems = new List<GameObject>();
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

        public override void UpdateCamera(Vector3 position, Quaternion rotation)
        {
            // PlayerControlledRotation();
            
            var p1Position = _p1.position;
            var p2Position = _p2.position;

            CheckVisualObstacle(p1Position, p2Position);
            
            // _cameraTransform.LookAt((p1Position + p2Position) / 2f);

            _cameraTransform.rotation = rotation;
            
            // var midpoint = (p1Position + p2Position) / 2f;
            // var distance = (p1Position - p2Position).magnitude;
            // distance = Math.Min(distance, _maxDist);
            //
            // var zoomModifier = distance > _zoomStartDist ? distance * _zoom : 1;
            //
            // _targetCameraPos = midpoint + _cameraOffset * zoomModifier; 
            _cameraTransform.position = Vector3.Slerp(_cameraTransform.position, position, _smoothness * Time.deltaTime);
        }

        // private void PlayerControlledRotation()
        // {
        //     RotateCam(_input.CameraRotation * _cameraRotation * Time.deltaTime);
        // }
        //
        // private void RotateCam(float angle)
        // {
        //     _cameraOffset = Quaternion.AngleAxis(angle, Vector3.up) * _cameraOffset;
        // }

        private void CheckVisualObstacle(Vector3 p1Position, Vector3 p2Position)
        {
            int layerMask = 1 << 3;

            float rayOffSet = 1;
            RaycastHit hit;
            if (Physics.Raycast(_cameraTransform.position,  (p1Position + Vector3.up * rayOffSet) - _cameraTransform.position, 
                out hit, (p1Position - _cameraTransform.position).magnitude, layerMask))
            {
                Debug.DrawRay(_cameraTransform.position, ((p1Position + Vector3.up * rayOffSet) - _cameraTransform.position), Color.yellow);
                // Debug.Log("Did Hit" + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
                hiddenItems.Add(hit.collider.gameObject);
                
            } else if (Physics.Raycast(_cameraTransform.position,  p2Position - _cameraTransform.position, 
                out hit, (p2Position - _cameraTransform.position).magnitude, layerMask))
            {
                Debug.DrawRay(_cameraTransform.position, ((p2Position + Vector3.up * rayOffSet) - _cameraTransform.position), Color.yellow);
                // Debug.Log("Did Hit" + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<Renderer> ().enabled = false;
                hiddenItems.Add(hit.collider.gameObject);
                
            }
            else
            {
                Debug.DrawRay(_cameraTransform.position, ((p1Position + Vector3.up * rayOffSet) - _cameraTransform.position), Color.white);
                Debug.DrawRay(_cameraTransform.position, ((p2Position) - _cameraTransform.position), Color.white);
                // Debug.Log("Did not Hit");
                foreach (var item in hiddenItems)
                {
                    // Debug.Log("Bringing back: " + item.name);
                    item.GetComponent<Renderer>().enabled = true;
                }
                hiddenItems.Clear();
                
            }
        }
    }
}