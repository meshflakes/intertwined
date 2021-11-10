using System;
using Interactable;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteraction : Interactable.Interactable
{
    private Transform _transform;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Quaternion _transitionRotationPerUpdate;
    private Vector3 _pivotPosition;
    private bool Swinging => _remainingRotation != 0;
    private float _remainingRotation = 0f;
    private Vector3 _rotationAxis = Vector3.zero;
    private int _rotationAxisModifier;        
        
    [Header("Door")]
    [Tooltip("How many degrees the door opens on rotation")]
    [Range(0, 359)]
    public int rotation = 90;
    [Tooltip("How fast the door rotates")] 
    public int degreesRotatedPerSecond = 20;

    [Space(10)]
    [Tooltip("Starting 'unlocked' status of the door")]
    public bool unlocked = true;
    [Tooltip("Id of key required to open the door, 0 if no key is required")]
    public int lockId;
    
    [Space(10)]
    [Tooltip("Starting state of the door")]
    public bool open = false;
    [Tooltip("Toggle direction of opening the door")]
    public bool openingTowardsNorth = true;

    
    
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _pivotPosition = _transform.Find("DoorPivot").position;
        _rotationAxisModifier = openingTowardsNorth ? 1 : -1;
        // var rotation = _transform.rotation;
        // _transform.po
        //
        // _originalRotation = new Quaternion(
        //     rotation.x,
        //     rotation.y,
        //     rotation.z,
        //     rotation.w);
    }

    private void Update()
    {
        if (!Swinging) return;
        
        var degreesToRotate = Math.Min(Time.deltaTime * degreesRotatedPerSecond, _remainingRotation);
        _transform.RotateAround(_pivotPosition, _rotationAxis, degreesToRotate);
        _remainingRotation -= degreesToRotate;
    }

    public override bool Interact(Character.Character interacter)
    {
        if (!unlocked || Swinging) return false;
        
        if (!open)
        {
            Debug.Log("opening");
            open = true;
            _remainingRotation = rotation;
            _rotationAxis = Vector3.up * _rotationAxisModifier;
        }
        else
        {
            Debug.Log("closing");
            open = false;
            _remainingRotation = rotation;
            _rotationAxis = Vector3.down * _rotationAxisModifier;
        }

        return true;
    }
    
    public override bool Interact(Character.Character interacter, Interactable.Interactable interactable)
    {
        if (lockId == 0 || unlocked) return Interact(interacter);

        // check if Interactable is a key AND the key can unlock this door
        if (interactable is KeyType key && key.CanUnlock(lockId))
        {
            unlocked = true;
            return Interact(interacter);
        }
        else return false;
    }

    public override bool UsedWith(Interactable.Interactable other)
    {
        return true;
    }
}