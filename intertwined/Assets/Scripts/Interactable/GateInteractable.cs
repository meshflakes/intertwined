﻿using System;
using Interactable.Util;
using UnityEngine;

namespace Interactable
{
    public class GateInteractable : DoorInteraction
    {
        [Header("Camera Sequence")]
        [Tooltip("Time taken for camera to move from original position to end position and vice versa")]
        public float animationDuration = 1;
        [Tooltip("Time spent viewing the end position")]
        public float minimumTimeBeforeReturn = 3;

        private CameraSequenceManager _cameraSequence;
        private GameObject _lock;
        
        protected new void Start()
        {
            base.Start();
            
            var targetTransform = transform.Find("Camera");
            _cameraSequence = new CameraSequenceManager(targetTransform, animationDuration, minimumTimeBeforeReturn);
            
            _lock = GameObject.Find("Lock");
        }

        public override bool Interact(Character.Character interacter)
        {
            if (unlocked) return base.Interact(interacter);
            
            // TODO: remove player control? 
            _cameraSequence.StartNewCameraSequence();
            return true;
        }

        public override bool Interact(Character.Character interacter, Interactable interactable)
        {
            if (!unlocked && base.Interact(interacter, interactable) && unlocked)
            {
                Destroy(_lock);
                return true;
            }

            return Interact(interacter);
        }
    }
}