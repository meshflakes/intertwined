using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class BoyInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public bool jump;
        public bool interact;

        [Header("Movement Settings")]
        public bool analogMovement;

        public void OnBoyMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnBoyJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnBoyInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }
		
        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }
    }
}