namespace Interactable
{
    public class KeyInteractable : GrabbableInteractable, KeyType
    {
        public bool CanUnlock(int keyId)
        {
            // TODO: add proper logic for unlock
            return keyId != 0;
        }

        public override bool UsedWith(Interactable other)
        {
            throw new System.NotImplementedException();
        }

        public override bool Interact(Character.Character interacter)
        {
            throw new System.NotImplementedException();
        }
    }
}