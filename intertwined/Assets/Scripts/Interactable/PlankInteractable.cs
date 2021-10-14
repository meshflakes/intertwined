using UnityEngine;

namespace Interactable
{
    public class PlankInteractable : GrabbableInteractable
    {
        public override bool Interact(Character.Character interacter)
        {
            if (Held())
            {
                Debug.Log("Releasing Object");
                Release();
            }
            else
            {
                Debug.Log("Grabbing object");
                Grab(interacter.gameObject);
            }

            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}