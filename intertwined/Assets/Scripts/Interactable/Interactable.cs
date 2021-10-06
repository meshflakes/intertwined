using UnityEngine;

namespace Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact(Character.Character interacter);
    }
}