using System.Collections.Generic;
using Controls;
using UnityEngine;

namespace Camera
{
    public class PlayerFollowCamera : CameraController
    {
        private readonly Transform _p1;
        private readonly Transform _p2;

        private readonly float _smoothness;
        private readonly Transform _cameraTransform;
        
        List<GameObject> hiddenItems = new List<GameObject>();
        private GameInputs _input;
        
        public PlayerFollowCamera(Transform p1, Transform p2, float smoothness, Transform cameraTransform)
        {
            _p1 = p1;
            _p2 = p2;
            _smoothness = smoothness;
            _cameraTransform = cameraTransform;
            
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInputs>();
        }

        public override void UpdateCamera(Vector3 targetPosition, Quaternion targetRotation)
        {
            var p1Position = _p1.position;
            var p2Position = _p2.position;

            CheckVisualObstacle(p1Position, p2Position);
            
            _cameraTransform.rotation = targetRotation;
            
            _cameraTransform.position = Vector3.Slerp(_cameraTransform.position, targetPosition, _smoothness * Time.deltaTime);
        }
        
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