using Interactable.Util;

namespace Interactable
{
    public class SignInteractable : Interactable
    {
        public float animationDuration = 1;
        public float minimumTimeBeforeReturn = 3;

        private CameraSequenceManager _cameraSequence;
        protected void Start()
        {
            var targetTransform = transform.Find("Camera");
            _cameraSequence = new CameraSequenceManager(targetTransform, animationDuration, minimumTimeBeforeReturn);
        }

        public override bool Interact(Character.Character interacter)
        {
            // TODO: remove player control? 
            _cameraSequence.StartNewCameraSequence();
            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}