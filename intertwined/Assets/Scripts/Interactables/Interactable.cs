using DefaultNamespace;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    private void OnTriggerEnter(Collider other)
    {
        // Add itself to the character's interactables list
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Interacter>()
                .AddToInteractablesList(gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Remove itself to the character's interactables list
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Interacter>()
                .RemoveFromInteractablesList(gameObject);
        }
    }
}