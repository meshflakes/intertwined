using System;
using System.Collections.Generic;
using Camera;
using Interactable.Util;
using UnityEngine;

namespace Interactable.ConstructionSite
{
    
    public class CraneCameraManager : MonoBehaviour
    {
        [Header("Camera Sequence")]
        [Tooltip("Time taken for camera to move from original position to end position and vice versa")]
        public float animationDuration = 2;
        
        private AreaCameraManager _cameraSequence;
        [NonSerialized] public readonly List<int> NumInLocations = new List<int> {0, 0, 0};

        protected void Start()
        {
            var targetTransform = transform.Find("Camera");
            _cameraSequence = new AreaCameraManager(targetTransform, animationDuration);
        }

        protected void Update()
        {
            if (NumInLocations[(int) Location.FullCraneArea] != 2)
            {
                // nobody in rn, make sure camera is not being modified
                if (_cameraSequence.GetCameraActive())
                {
                    _cameraSequence.EndCameraSequence();
                }
                
            } else if (NumInLocations[(int) Location.FinalBuilding] == 2)
            {
                // reached end, revert to regular camera

                if (_cameraSequence.GetCameraActive()) _cameraSequence.EndCameraSequence();
                else throw new Exception("Something went wrong here");
            }
            else
            {
                // set to special camera
                Debug.Log("Trying to start sequence");
                if (!_cameraSequence.GetCameraActive()) _cameraSequence.StartNewCameraSequence();
                
            }
        }
    }
}