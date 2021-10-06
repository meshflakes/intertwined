using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private Transform _transform;
    private List<GameObject> _inRangeInteractables;    

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _inRangeInteractables = new List<GameObject>();
    }
    
    public void AddToInteractablesList(GameObject obj)
    {
        _inRangeInteractables.Add(obj);
    }

    public void RemoveFromInteractablesList(GameObject obj)
    {
        _inRangeInteractables.RemoveAll(o => o == obj);
    }
    
    public void Interact(bool interact)
    {
        // May need interact button timeout (0.2s or so)
        if (Input.GetKeyDown("f") && _inRangeInteractables.Count > 0)
        {
            Debug.Log("starting interaction");
            var interactTarget = GetTargetInteractable();
            
            // TODO: add interaction logic here
            interactTarget.GetComponent<Interactable>().Interact();
        }
    }

    private GameObject GetTargetInteractable()
    {
        // TODO: add more sophisticated logic to select target interactable
        return _inRangeInteractables[0];
    }
}
