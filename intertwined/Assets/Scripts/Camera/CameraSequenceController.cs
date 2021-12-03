using UnityEngine;

namespace Camera
{

    
    /**
     * Short lived camera controller that is used to focus on a certain object for a certain duration,
     * then can be returns control to the player when a button is pressed
     */
    public class CameraSequenceController : CameraController
    {
        private readonly Vector3 _startingPosition;
        private readonly Quaternion _startingRotation;
        private readonly Vector3 _endingPosition;
        private readonly Quaternion _endingRotation;

        private readonly Transform _cameraTransform;

        private float _animationTime;
        private readonly float _animationDuration;
        private readonly float _minimumTimeBeforeReturn;

        private float _animationEndTime;
        private CameraMovementPhase _cameraMovementPhase = CameraMovementPhase.MoveToEnd;

        public CameraSequenceController(Vector3 startingPosition, Quaternion startingRotation, Vector3 endingPosition, 
                Quaternion endingRotation, Transform cameraTransform, float animationDuration, float minimumTimeBeforeReturn)
        {
             _startingPosition = startingPosition;
             _startingRotation = startingRotation;
             _endingPosition = endingPosition;
             _endingRotation = endingRotation;
             _cameraTransform = cameraTransform;
             _animationDuration = animationDuration;
             _minimumTimeBeforeReturn = minimumTimeBeforeReturn;
        }
        
        public CameraSequenceController(Vector3 endingPosition, Quaternion endingRotation, Transform cameraTransform,
            float animationDuration, float minimumTimeBeforeReturn)
            : this(cameraTransform.position, cameraTransform.rotation, endingPosition,
                endingRotation, cameraTransform, animationDuration, minimumTimeBeforeReturn) {}
        
        public override void UpdateCamera(Vector3 position, Quaternion rotation)
        {
            var phaseCompleted = false;
            switch (_cameraMovementPhase)
            {
                case CameraMovementPhase.MoveToEnd:
                    phaseCompleted = MoveCamera(_endingPosition, _endingRotation,
                        _startingPosition, _startingRotation);
                    break;
                
                case CameraMovementPhase.WaitAtEnd:
                    phaseCompleted = CheckIfMoveBackToStart();
                    break;
                
                case CameraMovementPhase.MoveBackToStart:
                    phaseCompleted = MoveCamera(position, rotation,
                        _endingPosition, _endingRotation);
                    // phaseCompleted = MoveCamera(_startingPosition, _startingRotation,
                    //     _endingPosition, _endingRotation);
                    break;
                
                case CameraMovementPhase.YieldCameraControl:
                    YieldingCameraControl = true;
                    break;
                    
                default:
                    YieldingCameraControl = true;
                    break;
            }

            if (phaseCompleted) _cameraMovementPhase++;
        }

        private bool CheckIfMoveBackToStart()
        {
            // TODO: check for player input 
            return Time.time > _animationEndTime + _minimumTimeBeforeReturn;
        }

        /**
         * Move camera position and rotation to target through Spherical linear interpolation
         *
         * return (bool): movement to target completed
         */
        private bool MoveCamera(Vector3 targetPosition, Quaternion targetRotation, Vector3 originalPosition, Quaternion originalRotation)
        {
            _animationTime += Time.deltaTime;
            
            _cameraTransform.position = Vector3.Slerp(originalPosition, targetPosition, _animationTime / _animationDuration);
            _cameraTransform.rotation = Quaternion.Slerp(originalRotation, targetRotation, _animationTime / _animationDuration);

            if (_animationTime <= _animationDuration) return false;
        
            _animationTime = 0;
            _animationEndTime = Time.time;
            return true;
        }
    }
    
    internal enum CameraMovementPhase
    {
        MoveToEnd,
        WaitAtEnd,
        MoveBackToStart,
        YieldCameraControl
    }
    
}