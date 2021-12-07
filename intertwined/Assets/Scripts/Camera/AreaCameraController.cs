using System;
using Controls;
using UnityEngine;

namespace Camera
{
    /**
    * Slightly scuffed implementation, should extract common stuff from CameraSequenceController
     * to another class and have both of these inherit from it instead
    */
    public class AreaCameraController : CameraSequenceController
    {

        [NonSerialized] public bool AreaCamActive = false;
        
        public AreaCameraController(Vector3 endingPosition, Quaternion endingRotation, Transform cameraTransform,
            float animationDuration)
            : base(endingPosition, endingRotation, cameraTransform, animationDuration, float.NaN)
        {
            // minimum time before return doesn't apply to this camera controller
        }
        
        protected override bool CheckIfMoveBackToStart()
        {
            return !AreaCamActive;
        }

    }
}