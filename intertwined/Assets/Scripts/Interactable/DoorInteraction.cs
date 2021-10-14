using Interactable;
using UnityEngine;

public class DoorInteraction : Interactable.Interactable
{
    private bool _opened = false;
    private Transform _transform;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Quaternion _transitionRotationPerUpdate;
    private Vector3 _pivotPosition;
    
    [Header("Door")]
    [Tooltip("How many degrees the door opens on rotation")]
    [Range(0, 359)]
    public int Rotation = 90;
    [Tooltip("How fast the door rotates")] 
    public int DegreesRotatedPerSecond = 20;

    [Space(10)]
    [Tooltip("Starting 'unlocked' status of the door")]
    public bool Unlocked = true;
    [Tooltip("Id of key required to open the door, 0 if no key is required")]
    public int KeyId = 0;
    
    [Space(10)]
    [Tooltip("Starting state of the door")]
    public bool Open = false;
    [Tooltip("Toggle direction of opening the door")]
    public bool OpeningTowardsNorth = true;
    
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _pivotPosition = _transform.Find("DoorPivot").position;
        // var rotation = _transform.rotation;
        // _transform.po
        //
        // _originalRotation = new Quaternion(
        //     rotation.x,
        //     rotation.y,
        //     rotation.z,
        //     rotation.w);
    }

    public override bool Interact(Character.Character interacter)
    {
        if (!Unlocked) return false;
        
        if (!_opened)
        {
            Debug.Log("opening");
            _opened = true;
            _transform.RotateAround(_pivotPosition, Vector3.up, Rotation);
        }
        else
        {
            Debug.Log("closing");
            _opened = false;
            _transform.RotateAround(_pivotPosition, Vector3.down, Rotation);
        }

        return true;
    }
    
    public override bool Interact(Character.Character interacter, Interactable.Interactable interactable)
    {
        Debug.Log("Interacting with Door");
        if (KeyId == 0) return Interact(interacter);

        // TODO: figure out how to check
        // if (typeof(KeyType).IsInstanceOfType(interactable)) return false;
        
        Debug.Log("is key type");
        if (!((KeyType) interactable).CanUnlock(KeyId)) return false;
        Debug.Log("can unlock");

        Unlocked = true;
        return Interact(interacter);
    }


    public override bool UsedWith(Interactable.Interactable other)
    {
        return true;
    }
}