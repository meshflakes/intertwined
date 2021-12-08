using UnityEngine;

namespace Interactable
{
    public class DirtPileInteraction : Interactable
    {
        public GameObject plank;

        public AudioSource diggingAudio;

        private bool _diggingStarted;
        private Animator _anim;

        public bool noObject;

        protected void Update()
        {
            if (_diggingStarted && !diggingAudio.isPlaying)
            {
                _anim.SetInteger("Interacting", 0);
                CompleteDigging();   
            }
        }

        public override bool Interact(Character.Character interacter)
        {
            if (_diggingStarted)
            {
                interacter.GetComponentInChildren<Animator>().SetInteger("Interacting", 0);
                return false;
            }
            
            // TODO: lock dog in place for animation & audio
            diggingAudio.Play();
            _diggingStarted = true;
            _anim = interacter.GetComponentInChildren<Animator>();
            _anim.SetInteger("Interacting", 1);
            return true;
        }

        private void CompleteDigging()
        {
            var plankChildTransform = transform.Find("Plank");
            if(!noObject) Instantiate(plank, plankChildTransform.position, plankChildTransform.rotation);
            RemoveInteractableFromCharacters(); 
            Destroy(gameObject);
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}