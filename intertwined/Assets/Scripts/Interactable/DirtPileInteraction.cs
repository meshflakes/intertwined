using UnityEngine;

namespace Interactable
{
    public class DirtPileInteraction : Interactable
    {
        public GameObject plank;

        public AudioSource diggingAudio;

        private bool _diggingStarted;

        protected void Update()
        {
            if (_diggingStarted && !diggingAudio.isPlaying)
                CompleteDigging();
        }

        public override bool Interact(Character.Character interacter)
        {
            if (_diggingStarted) return false;
            
            // TODO: lock dog in place for animation & audio
            diggingAudio.Play();
            _diggingStarted = true;
            return true;
        }

        private void CompleteDigging()
        {
            var plankChildTransform = transform.Find("Plank");
            Instantiate(plank, plankChildTransform.position, plankChildTransform.rotation);
            RemoveInteractableFromCharacters(); 
            Destroy(gameObject);
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}