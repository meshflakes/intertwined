using UnityEngine;

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
    
        private Vector3 _cameraOffset;
        private CameraController _playerFollowCamera;
        
        // TODO: use / remove
        // public Collider camTrigger;
        // public float followTimeDelta = 0.8f;
    
        private void Start()
        {
            var cameraTransform = transform;
            
            _cameraOffset = cameraTransform.position - ((p1.position + p2.position) / 2f);
            _playerFollowCamera =
                new PlayerFollowCamera(p1, p2, zoom, zoomStartDist, maxDist, smoothness, _cameraOffset, cameraTransform);
        }

        private void Update()
        {
            _playerFollowCamera.UpdateCamera();
        }

        public void SetCameraController()
        {
            // TODO: implement
        }
    }
}
