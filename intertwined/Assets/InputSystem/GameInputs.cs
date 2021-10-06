using UnityEngine;
using UnityEngine.InputSystem;

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
            boyInteract = newInteractState;
        }
        
        public void OnDogMove(InputValue value)
        {
            DogMoveInput(value.Get<Vector2>());
        }

        public void OnDogInteract(InputValue value)
        {
            DogInteractInput(value.isPressed);
        }

        private void DogMoveInput(Vector2 newMoveDirection)
        {
            dogMove = newMoveDirection;
        }

        private void DogInteractInput(bool newInteractState)
        {
            dogInteract = newInteractState;
        }
    }
}