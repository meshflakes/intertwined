using System;
using System.Collections.Generic;
using Interactable;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [Tooltip("Move speed of the character in m/s")]
        public float moveSpeed = 2.0f;

        [Tooltip("How fast the character turns to face movement direction")] [Range(0.0f, 0.3f)]
        public float rotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float speedChangeRate = 10.0f;

        [Space(10)] [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float gravity = -15.0f;

        [Space(10)] [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float fallTimeout = 0.15f;

        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool grounded = true;

        [Tooltip("Useful for rough ground")] public float groundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float groundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask groundLayers;

        public Interactor CharInteractor;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        private Animator _animator;
        private bool _hasAnimator;
        
        private CharacterController _controller;

        [Tooltip("Climbing speed of the char")]
        public float climbSpeed = 2.0f;
        private bool Climbing => _climbingObj != null;
        private ClimbableObj _climbingObj;
        private Vector3 _normalizedClimbDirection;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();

            AssignAnimationIDs();

            _fallTimeoutDelta = fallTimeout;

            CharInteractor = new Interactor();
            CharInteractor.PlayerChar = this;
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            Gravity();
            GroundedCheck();
            
            CharInteractor.UpdateHeldInteractablePosition();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            var position = transform.position;
            var spherePosition = new Vector3(position.x, position.y - groundedOffset, position.z);
            grounded =
                Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, grounded);
            }
        }

        private void Gravity()
        {
            if (Climbing)
            {
                _fallTimeoutDelta = fallTimeout;
                _verticalVelocity = Math.Max(_verticalVelocity, 0.0f);
            }
            else if (grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = fallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }
            }
            else
            {
                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }
        }
        
        public void Move(Vector2 move, bool analogMovement, GameObject mainCamera)
        {
            move = LimitMoveBounds(move);

            var inputMagnitude = move.magnitude;
            
            if (Climbing)
            {
                Debug.Log("Climbing=True");

                Climb(move, mainCamera);
                return;
            }
            
            var targetSpeed = moveSpeed;
            
            // if there is no input, set the target speed to 0
            if (move == Vector2.zero) targetSpeed = 0.0f;

            CalculateAndUpdateSpeed(inputMagnitude, analogMovement, targetSpeed);
            
            var targetDirection = CalculateAndUpdateRotation(move, mainCamera);

            MovePlayer(targetDirection);

            MoveAnimation(inputMagnitude, targetSpeed);
        }

        private void MovePlayer(Vector3 targetDirection)
        {
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
            
        }

        private void CalculateAndUpdateSpeed(float inputMagnitude, bool analogMovement, float targetSpeed)
        {
            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon
            var velocity = _controller.velocity;
            var currentHorizontalSpeed = new Vector3(velocity.x, 0.0f, velocity.z).magnitude;

            const float speedOffset = 0.1f;
            inputMagnitude = analogMovement ? inputMagnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * speedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }
        }

        private Vector3 CalculateAndUpdateRotation(Vector2 move, GameObject mainCamera)
        {
            // normalise input direction
            Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

            if (move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) *
                    Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                
                if (!Climbing) // if climbing rotation is fixed to climb direction
                {
                    var rotation = Mathf.SmoothDampAngle(
                        transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, rotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
            }
            
            return Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        }

        private void MoveAnimation(float inputMagnitude, float targetSpeed)
        {
            if (!_hasAnimator) return;
            
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed,
                Time.deltaTime * speedChangeRate);
            
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }

        private void Climb(Vector2 moveInput, GameObject mainCamera)
        {
            var climbSpeedModifier = DirectedClimbSpeedModifier(moveInput);

            // TODO: insert correct location in GetClimbingDirection
            var moveAmt = climbSpeed * climbSpeedModifier * Time.deltaTime * (_climbingObj.GetClimbingDirection(Vector3.zero) * Vector3.up);

            Debug.Log($"grounded={grounded}, climbSpeedModifier={climbSpeedModifier}");
            if (!grounded || climbSpeedModifier >= 0)
            {
                _controller.Move(moveAmt);
            }
            else
            {
                StopClimbing();
            }
        }

        /**
         * takes in the movement input, and returns modifier on climb speed:
         *   positive for climbing up, negative for climbing down
         */
        private float DirectedClimbSpeedModifier(Vector2 moveInput)
        {
            var transformPosition = transform.position;
            var position2d = new Vector3(transformPosition.x, 0f, transformPosition.z);

            // percieve climbable position to be further away from the char than it is, so the forward movement
            // according to the player is interpreted as moving up the ladder
            var climbablePosition = _climbingObj.transform.position + Vector3.forward;
            // var climbablePosition = _climbingObj.transform.position + (transform.rotation * Vector3.forward);
            var climbablePosition2d = new Vector3(climbablePosition.x, 0f, climbablePosition.z);

            var climbableDirection = climbablePosition2d - position2d;
            var projectedMovement = Vector3.Project(new Vector3(moveInput.x, 0f, moveInput.y), climbableDirection);

            var movingInOppositeDirection =
                (climbablePosition.x - transformPosition.x) < 0 == projectedMovement.x < 0;
            
            return movingInOppositeDirection ? moveInput.magnitude : -moveInput.magnitude;
        }

        private bool ToStopClimbing()
        {
            // TODO: figure out when to stop climbing
            return false;
        }

        public void StartClimbing(ClimbableObj obj, Vector3 startingPosition, Quaternion climbingDirection)
        {
            _climbingObj = obj;
            Debug.Log($"climbing obj set as {_climbingObj}");
            _normalizedClimbDirection = climbingDirection.eulerAngles.normalized;
            var currRotationEuler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(climbingDirection.eulerAngles.x, currRotationEuler.y, currRotationEuler.z);

            Debug.Log($"Climbing={Climbing}");
            // TODO: set starting position
        }

        public void StopClimbing()
        {
            _climbingObj = null;
            var currRotationEuler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, currRotationEuler.y, 0);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            Gizmos.color = grounded ? transparentGreen : transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            var position = transform.position;
            Gizmos.DrawSphere(new Vector3(position.x, position.y - groundedOffset, position.z),
                groundedRadius);
        }
        
        private Vector2 LimitMoveBounds(Vector2 move)
        {
            //If players are about to be out of bounds, do not let them move in that direction
            Vector3 vp = UnityEngine.Camera.main.WorldToViewportPoint(transform.position);
            if (vp.x < 0.05 && move.x < 0)
            {
                move = Vector2.zero;
            }
            else if (vp.x > 0.95 && move.x > 0)
            {
                move = Vector2.zero;
            }
            if (vp.y < 0.05 && move.y < 0)
            {
                move = Vector2.zero;
            }
            else if (vp.y > 0.95 && move.y > 0)
            {
                move = Vector2.zero;
            }

            return move;
        }
    }
}