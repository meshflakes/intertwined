using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace InputSystem
{
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

        public void OnBoyMove(InputValue value)
        {
            BoyMoveInput(value.Get<Vector2>());
        }

        public void OnBoyInteract(InputValue value)
        {
            BoyInteractInput(value.isPressed);
        }

        private void BoyMoveInput(Vector2 newMoveDirection)
        {
            boyMove = newMoveDirection;
        }

        private void BoyInteractInput(bool newInteractState)
        {
            boyInteract = !boyInteract;
        }
        
        public void OnDogMove(InputValue value)
        {
            DogMoveInput(value.Get<Vector2>());
        }

        public void OnDogInteract(InputValue value)
        {
            // UnityEngine.Debug.Log($"OnDogInteract calling function: {new StackFrame(1, true).GetMethod().Name}");
            DogInteractInput(value.isPressed);
        }

        public void OnRotateCamClockwise(InputValue value)
        {
            Debug.Log($"OnRotateCamClockwise called with value={value.isPressed}, by {new StackFrame(1, true).GetMethod().Name}");
        }
        
        public void OnRotateCamAntiClockwise(InputValue value)
        {
            Debug.Log($"OnRotateCamClockwise called with value={value.isPressed}, by {new StackFrame(1, true).GetMethod().Name}");
        }

        private void DogMoveInput(Vector2 newMoveDirection)
        {
            dogMove = newMoveDirection;
        }

        private void DogInteractInput(bool newInteractState)
        {

            dogInteract = !dogInteract;
        }
    }
}