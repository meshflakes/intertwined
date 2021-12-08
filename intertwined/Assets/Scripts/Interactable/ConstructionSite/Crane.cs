using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactable.ConstructionSite
{
    public class Crane : MonoBehaviour
    {
        [NonSerialized] public bool Powered = false;
        [NonSerialized] public int NumInTrigger = 0;
        public int degreesRotatedPerSecond;
        public float maxRotation;
        private float _currentRotation = 27f;
        [FormerlySerializedAs("barBlockingColliders")] public GameObject frontBlockingColliders;
        public GameObject backBlockingColliders;
        public Transform craneTransform;

        private GameObject _buildingCollider;

        protected void Start()
        {
            _buildingCollider = GameObject.Find("beam entrance blocker");
        }

        protected void Update()
        {
            UpdateRotation();
            UpdateSteelBeamColliders();
        }

        private void UpdateRotation()
        {
            if (!Powered) return;
                        
            float degreesToRotate;
            
            if (NumInTrigger > 0)
            {
                if (IsEqual(_currentRotation, maxRotation)) return;
                
                degreesToRotate = Math.Min(Time.deltaTime * degreesRotatedPerSecond, maxRotation-_currentRotation);
                craneTransform.Rotate(0, degreesToRotate, 0);
                _currentRotation += degreesToRotate;
                return;
            }
            else if (IsEqual(_currentRotation, 0f)) return;
            
            degreesToRotate = Math.Min(Time.deltaTime * degreesRotatedPerSecond, _currentRotation);
            craneTransform.Rotate(0, -degreesToRotate, 0);
            _currentRotation -= degreesToRotate;
        }

        private void UpdateSteelBeamColliders()
        {
            frontBlockingColliders.SetActive(!IsEqual(_currentRotation, 0));
            backBlockingColliders.SetActive(!IsEqual(_currentRotation, maxRotation));
            _buildingCollider.SetActive(!IsEqual(_currentRotation, maxRotation));
        }

        private static bool IsEqual(float x, float y)
        {
            const double epsilon = 1e-5;

            return Math.Abs(x - y) < epsilon;
        }
    }
}