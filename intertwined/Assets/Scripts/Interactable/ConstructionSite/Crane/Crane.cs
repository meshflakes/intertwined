using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable.ConstructionSite.Crane
{
    public class Crane : MonoBehaviour, IEntityMover
    {
        [NonSerialized] public bool Powered = false;
        [NonSerialized] public int NumInTrigger = 0;
        public int degreesRotatedPerSecond;
        public float maxRotation;
        private float _currentRotation = 27f;
        public GameObject frontBlockingColliders;
        public GameObject backBlockingColliders;
        public Transform craneTransform;

        public GameObject buildingCollider;

        private readonly HashSet<(Transform transform, float movementScale)> _movementEntities = new HashSet<(Transform, float)>();

        protected void Update()
        {
            UpdateRotation();
            UpdateSteelBeamColliders();
        }

        private void UpdateRotation()
        {
            if (!Powered) return;
                        
            float degreesToRotate;
            int rotationSign;
            
            if (NumInTrigger > 0)
            {
                if (IsEqual(_currentRotation, maxRotation)) return;

                rotationSign = 1;
                degreesToRotate = Math.Min(Time.deltaTime * degreesRotatedPerSecond, maxRotation-_currentRotation);
            }
            else
            {
                if (IsEqual(_currentRotation, 0f)) return;
                rotationSign = -1;
                degreesToRotate = Math.Min(Time.deltaTime * degreesRotatedPerSecond, _currentRotation);
            }
            
            Rotate(rotationSign * degreesToRotate);
        }

        private void Rotate(float signedRotationAngle)
        {
            var rotationCenter = craneTransform.position;
            craneTransform.RotateAround(rotationCenter, Vector3.up, signedRotationAngle);

            foreach (var entity in _movementEntities)
            {
                // Not sure why the scaling is required, but it's necessary - and seems to change frequently
                entity.transform.RotateAround(rotationCenter, Vector3.up, signedRotationAngle * entity.movementScale * 1.5f);
            }
            _currentRotation += signedRotationAngle;
        }

        private void UpdateSteelBeamColliders()
        {
            frontBlockingColliders.SetActive(!IsEqual(_currentRotation, 0));
            backBlockingColliders.SetActive(!IsEqual(_currentRotation, maxRotation));
            buildingCollider.SetActive(!IsEqual(_currentRotation, maxRotation));
        }

        private static bool IsEqual(float x, float y)
        {
            const double epsilon = 1e-5;

            return Math.Abs(x - y) < epsilon;
        }

        public void AddEntity((Transform transform, float movementScale) entity)
        {
            _movementEntities.Add(entity);
        }

        public void RemoveEntity((Transform transform, float movementScale) entity)
        {
            _movementEntities.Remove(entity);
        }
    }
}