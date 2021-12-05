using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    [RequireComponent(typeof(PlayerInput))]
    public class GameInputs : MonoBehaviour
    {
        [Header("Boy Input Values")]
        public Vector2 boyMove;
        public bool boyInteract;
        
        [Header("Dog Input Values")]
        public Vector2 dogMove;
        public bool dogInteract;
        
        [Header("Movement Settings")]
        public bool analogMovement;

        public int CameraRotation => GetNetRotationEffect();
        [Header("Camera Rotation Inputs")]
        public bool clockwiseRotation;
        public bool antiClockwiseRotation;

        public bool paused;
        public bool mainmenu;
        
        public void OnBoyMove(InputValue value)
        {
            BoyMoveInput(value.Get<Vector2>());
        }

        public void OnBoyInteract(InputValue value)
        {
            // BoyInteractInput(value.isPressed);
            boyInteract = value.Get<Single>() > 0.5f;
        }

        private void BoyMoveInput(Vector2 newMoveDirection)
        {
            boyMove = newMoveDirection;
        }

        // private void BoyInteractInput(bool newInteractState)
        // {
        //     boyInteract = !boyInteract;
        // }
        
        public void OnDogMove(InputValue value)
        {
            DogMoveInput(value.Get<Vector2>());
        }

        public void OnDogInteract(InputValue value)
        {
            // DogInteractInput(value.isPressed);
            dogInteract = value.Get<Single>() > 0.5f;
        }
        
        private void DogMoveInput(Vector2 newMoveDirection)
        {
            dogMove = newMoveDirection;
        }

        // private void DogInteractInput(bool newInteractState)
        // {
        //     dogInteract = !dogInteract;
        // }
        
        public void OnRotateCamClockwise(InputValue value)
        {
            clockwiseRotation = value.Get<Single>() > 0.5f;
        }
        
        public void OnRotateCamAntiClockwise(InputValue value)
        {
            antiClockwiseRotation = value.Get<Single>() > 0.5f;
        }

        public void OnPause(InputValue value)
        {
            paused = value.Get<Single>() > 0.5f;
        }

        public void OnMainMenu(InputValue value)
        {
            mainmenu = value.Get<Single>() > 0.5f;
        }

        private int GetNetRotationEffect()
        {
            if (clockwiseRotation == antiClockwiseRotation) return 0;
            if (clockwiseRotation) return 1;
            
            // else if (antiClockwiseRotation) 
            return -1;
        }

        public void OnStart(InputValue value)
        {
            Debug.Log($"Start Pressed={value}");
        }

        public bool AnyCharInput()
        {
            return boyInteract || dogInteract || boyMove != Vector2.zero || dogMove != Vector2.zero;
        }
    }
}