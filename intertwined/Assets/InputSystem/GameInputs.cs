using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class GameInputs : MonoBehaviour
    {
        [Header("Boy Input Values")]
        public Vector2 boyMove;
        public bool boyJump;
        public bool boyInteract;
        
        [Header("Dog Input Values")]
        public Vector2 dogMove;
        public bool dogJump;
        public bool dogInteract;

        public void OnBoyMove(InputValue value)
        {
            BoyMoveInput(value.Get<Vector2>());
        }

        public void OnBoyJump(InputValue value)
        {
            BoyJumpInput(value.isPressed);
        }

        public void OnBoyInteract(InputValue value)
        {
            BoyInteractInput(value.isPressed);
        }

        public void BoyMoveInput(Vector2 newMoveDirection)
        {
            boyMove = newMoveDirection;
        }
		
        public void BoyJumpInput(bool newJumpState)
        {
            boyJump = newJumpState;
        }

        public void BoyInteractInput(bool newInteractState)
        {
            boyInteract = newInteractState;
        }
        
        public void OnDogMove(InputValue value)
        {
            DogMoveInput(value.Get<Vector2>());
        }

        public void OnDogJump(InputValue value)
        {
            DogJumpInput(value.isPressed);
        }

        public void OnDogInteract(InputValue value)
        {
            DogInteractInput(value.isPressed);
        }

        public void DogMoveInput(Vector2 newMoveDirection)
        {
            dogMove = newMoveDirection;
        }
		
        public void DogJumpInput(bool newJumpState)
        {
            dogJump = newJumpState;
        }

        public void DogInteractInput(bool newInteractState)
        {
            dogInteract = newInteractState;
        }
    }
}