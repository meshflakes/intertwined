using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    
    public class TwoPlayerController : MonoBehaviour
    {
	    //Player Objects
	    [SerializeField]
	    private GameObject player1;
	    [SerializeField]
	    private GameObject player2;
	    
        [Header("Player 1")]
        [Tooltip("The character's controller")]
        public CharacterController Player1CharacterController;
        [Tooltip("Move speed of the character in m/s")]
        public float Player1MoveSpeed = 2.0f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float Player1RotationSmoothTime = 0.12f;
        [Tooltip("Acceleration and deceleration")]
        public float Player1SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float Player1JumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Player1Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float Player1JumpTimeout = 0.50f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float Player1FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Player1Grounded = true;
        [Tooltip("Useful for rough ground")]
        public float Player1GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float Player1GroundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask Player1GroundLayers;
        
        // player
        private float _speedPlayer1;
        private float _animationBlendPlayer1;
        private float _targetRotationPlayer1 = 0.0f;
        private float _rotationVelocityPlayer1;
        private float _verticalVelocityPlayer1;
        private float _terminalVelocityPlayer1 = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDeltaPlayer1;
        private float _fallTimeoutDeltaPlayer1;

        // animation IDs
        private int _animIDSpeedPlayer1;
        private int _animIDGroundedPlayer1;
        private int _animIDJumpPlayer1;
        private int _animIDFreeFallPlayer1;
        private int _animIDMotionSpeedPlayer1;

        private Animator _animatorPlayer1;
        private CharacterController _controllerPlayer1;
        private BoyInputs _inputPlayer1;
        private GameObject _mainCameraPlayer1;

        private bool _hasAnimatorPlayer1;
        
        
        [Header("Player 2")]
        [Tooltip("The character's controller")]
        public CharacterController Player2CharacterController;
        [Tooltip("Move speed of the character in m/s")]
        public float Player2MoveSpeed = 2.0f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float Player2RotationSmoothTime = 0.12f;
        [Tooltip("Acceleration and deceleration")]
        public float Player2SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float Player2JumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Player2Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float Player2JumpTimeout = 0.50f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float Player2FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Player2Grounded = true;
        [Tooltip("Useful for rough ground")]
        public float Player2GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float Player2GroundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask Player2GroundLayers;
        
        // player
        private float _speedPlayer2;
        private float _animationBlendPlayer2;
        private float _targetRotationPlayer2 = 0.0f;
        private float _rotationVelocityPlayer2;
        private float _verticalVelocityPlayer2;
        private float _terminalVelocityPlayer2 = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDeltaPlayer2;
        private float _fallTimeoutDeltaPlayer2;

        // animation IDs
        private int _animIDSpeedPlayer2;
        private int _animIDGroundedPlayer2;
        private int _animIDJumpPlayer2;
        private int _animIDFreeFallPlayer2;
        private int _animIDMotionSpeedPlayer2;

        private Animator _animatorPlayer2;
        private CharacterController _controllerPlayer2;
        private DogInputs _inputPlayer2;

        private bool _hasAnimatorPlayer2;
        
        
        private GameObject _mainCamera;
        
        
        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            _hasAnimatorPlayer1 = TryGetComponent(out _animatorPlayer1);
            _hasAnimatorPlayer2 = TryGetComponent(out _animatorPlayer2);
            _controllerPlayer1 = Player1CharacterController;
            _controllerPlayer2 = Player2CharacterController;
            _inputPlayer1 = GetComponent<BoyInputs>();
            _inputPlayer2 = GetComponent<DogInputs>();
            

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDeltaPlayer1 = Player1JumpTimeout;
            _jumpTimeoutDeltaPlayer2 = Player2JumpTimeout;
            _fallTimeoutDeltaPlayer1 = Player1FallTimeout;
            _fallTimeoutDeltaPlayer2 = Player2FallTimeout;
        }

        private void Update()
        {
            _hasAnimatorPlayer1 = TryGetComponent(out _animatorPlayer1);
            _hasAnimatorPlayer2 = TryGetComponent(out _animatorPlayer2);
			
            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeedPlayer1 = Animator.StringToHash("Speed");
            _animIDGroundedPlayer1 = Animator.StringToHash("Grounded");
            _animIDJumpPlayer1 = Animator.StringToHash("Jump");
            _animIDFreeFallPlayer1 = Animator.StringToHash("FreeFall");
            _animIDMotionSpeedPlayer1 = Animator.StringToHash("MotionSpeed");
            
            _animIDSpeedPlayer2 = Animator.StringToHash("Speed");
            _animIDGroundedPlayer2 = Animator.StringToHash("Grounded");
            _animIDJumpPlayer2 = Animator.StringToHash("Jump");
            _animIDFreeFallPlayer2 = Animator.StringToHash("FreeFall");
            _animIDMotionSpeedPlayer2 = Animator.StringToHash("MotionSpeed");
        }
        
         private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePositionPlayer1 = 
	            new Vector3(player1.transform.position.x, player1.transform.position.y - Player1GroundedOffset, player1.transform.position.z);
            Player1Grounded = Physics.CheckSphere(spherePositionPlayer1, Player1GroundedRadius, Player1GroundLayers, QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimatorPlayer1)
            {
                _animatorPlayer1.SetBool(_animIDGroundedPlayer1, Player1Grounded);
            }
            
            // set sphere position, with offset
            Vector3 spherePositionPlayer2 = 
	            new Vector3(player2.transform.position.x, player2.transform.position.y - Player2GroundedOffset, player2.transform.position.z);
            Player2Grounded = Physics.CheckSphere(spherePositionPlayer2, Player2GroundedRadius, Player2GroundLayers, QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimatorPlayer2)
            {
	            _animatorPlayer2.SetBool(_animIDGroundedPlayer2, Player2Grounded);
            }
        }
        
        private void Move()
		{
			// Player 1
			
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeedPlayer1 = Player1MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_inputPlayer1.move == Vector2.zero) targetSpeedPlayer1 = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeedPlayer1 = new Vector3(_controllerPlayer1.velocity.x, 0.0f, _controllerPlayer1.velocity.z).magnitude;

			float speedOffsetPlayer1 = 0.1f;
			float inputMagnitudePlayer1 = _inputPlayer1.move.magnitude;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeedPlayer1 < targetSpeedPlayer1 - speedOffsetPlayer1 || currentHorizontalSpeedPlayer1 > targetSpeedPlayer1 + speedOffsetPlayer1)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speedPlayer1 = Mathf.Lerp(currentHorizontalSpeedPlayer1, targetSpeedPlayer1 * inputMagnitudePlayer1, Time.deltaTime * Player1SpeedChangeRate);

				// round speed to 3 decimal places
				_speedPlayer1 = Mathf.Round(_speedPlayer1 * 1000f) / 1000f;
			}
			else
			{
				_speedPlayer1 = targetSpeedPlayer1;
			}
			_animationBlendPlayer1 = Mathf.Lerp(_animationBlendPlayer1, targetSpeedPlayer1, Time.deltaTime * Player1SpeedChangeRate);

			// normalise input direction
			Vector3 inputDirectionPlayer1 = new Vector3(_inputPlayer1.move.x, 0.0f, _inputPlayer1.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_inputPlayer1.move != Vector2.zero)
			{
				_targetRotationPlayer1 = Mathf.Atan2(inputDirectionPlayer1.x, inputDirectionPlayer1.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
				float rotation = 
					Mathf.SmoothDampAngle(player1.transform.eulerAngles.y, _targetRotationPlayer1, ref _rotationVelocityPlayer1, Player1RotationSmoothTime);

				// rotate to face input direction relative to camera position
				player1.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}


			Vector3 targetDirectionPlayer1 = Quaternion.Euler(0.0f, _targetRotationPlayer1, 0.0f) * Vector3.forward;

			// move the player
			_controllerPlayer1.Move(targetDirectionPlayer1.normalized * (_speedPlayer1 * Time.deltaTime) + new Vector3(0.0f, _verticalVelocityPlayer1, 0.0f) * Time.deltaTime);

			// update animator if using character
			if (_hasAnimatorPlayer1)
			{
				_animatorPlayer1.SetFloat(_animIDSpeedPlayer1, _animationBlendPlayer1);
				_animatorPlayer1.SetFloat(_animIDMotionSpeedPlayer1, inputMagnitudePlayer1);
			}
			
			
			
			
			// Player 2
			
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeedPlayer2 = Player2MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_inputPlayer2.move == Vector2.zero) targetSpeedPlayer2 = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeedPlayer2 = new Vector3(_controllerPlayer2.velocity.x, 0.0f, _controllerPlayer2.velocity.z).magnitude;

			float speedOffsetPlayer2 = 0.1f;
			float inputMagnitudePlayer2 = _inputPlayer2.move.magnitude;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeedPlayer2 < targetSpeedPlayer2 - speedOffsetPlayer2 || currentHorizontalSpeedPlayer2 > targetSpeedPlayer2 + speedOffsetPlayer2)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speedPlayer2 = Mathf.Lerp(currentHorizontalSpeedPlayer2, targetSpeedPlayer2 * inputMagnitudePlayer2, Time.deltaTime * Player2SpeedChangeRate);

				// round speed to 3 decimal places
				_speedPlayer2 = Mathf.Round(_speedPlayer2 * 1000f) / 1000f;
			}
			else
			{
				_speedPlayer2 = targetSpeedPlayer2;
			}
			_animationBlendPlayer2 = Mathf.Lerp(_animationBlendPlayer2, targetSpeedPlayer2, Time.deltaTime * Player2SpeedChangeRate);

			// normalise input direction
			Vector3 inputDirectionPlayer2 = new Vector3(_inputPlayer2.move.x, 0.0f, _inputPlayer2.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_inputPlayer2.move != Vector2.zero)
			{
				_targetRotationPlayer2 = Mathf.Atan2(inputDirectionPlayer2.x, inputDirectionPlayer2.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
				float rotation = 
					Mathf.SmoothDampAngle(player2.transform.eulerAngles.y, _targetRotationPlayer2, ref _rotationVelocityPlayer2, Player2RotationSmoothTime);

				// rotate to face input direction relative to camera position
				player2.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}


			Vector3 targetDirectionPlayer2 = Quaternion.Euler(0.0f, _targetRotationPlayer2, 0.0f) * Vector3.forward;

			// move the player
			_controllerPlayer2.Move(targetDirectionPlayer2.normalized * (_speedPlayer2 * Time.deltaTime) + new Vector3(0.0f, _verticalVelocityPlayer2, 0.0f) * Time.deltaTime);

			// update animator if using character
			if (_hasAnimatorPlayer2)
			{
				_animatorPlayer2.SetFloat(_animIDSpeedPlayer2, _animationBlendPlayer2);
				_animatorPlayer2.SetFloat(_animIDMotionSpeedPlayer2, inputMagnitudePlayer2);
			}
		}

		private void JumpAndGravity()
		{
			// Player 1
			
			if (Player1Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDeltaPlayer1 = Player1FallTimeout;

				// update animator if using character
				if (_hasAnimatorPlayer1)
				{
					_animatorPlayer1.SetBool(_animIDJumpPlayer1, false);
					_animatorPlayer1.SetBool(_animIDFreeFallPlayer1, false);
				}

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocityPlayer1 < 0.0f)
				{
					_verticalVelocityPlayer1 = -2f;
				}

				// Jump
				if (_inputPlayer1.jump && _jumpTimeoutDeltaPlayer1 <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocityPlayer2 = Mathf.Sqrt(Player1JumpHeight * -2f * Player1Gravity);

					// update animator if using character
					if (_hasAnimatorPlayer1)
					{
						_animatorPlayer1.SetBool(_animIDJumpPlayer1, true);
					}
				}

				// jump timeout
				if (_jumpTimeoutDeltaPlayer1 >= 0.0f)
				{
					_jumpTimeoutDeltaPlayer1 -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDeltaPlayer1 = Player1JumpTimeout;

				// fall timeout
				if (_fallTimeoutDeltaPlayer1 >= 0.0f)
				{
					_fallTimeoutDeltaPlayer1 -= Time.deltaTime;
				}
				else
				{
					// update animator if using character
					if (_hasAnimatorPlayer1)
					{
						_animatorPlayer1.SetBool(_animIDFreeFallPlayer1, true);
					}
				}

				// if we are not grounded, do not jump
				_inputPlayer1.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocityPlayer1 < _terminalVelocityPlayer1)
			{
				_verticalVelocityPlayer1 += Player1Gravity * Time.deltaTime;
			}
			
			
			
			// Player 2
			
			if (Player2Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDeltaPlayer2 = Player2FallTimeout;

				// update animator if using character
				if (_hasAnimatorPlayer2)
				{
					_animatorPlayer2.SetBool(_animIDJumpPlayer2, false);
					_animatorPlayer2.SetBool(_animIDFreeFallPlayer2, false);
				}

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocityPlayer2 < 0.0f)
				{
					_verticalVelocityPlayer2 = -2f;
				}

				// Jump
				if (_inputPlayer2.jump && _jumpTimeoutDeltaPlayer2 <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocityPlayer2 = Mathf.Sqrt(Player2JumpHeight * -2f * Player2Gravity);

					// update animator if using character
					if (_hasAnimatorPlayer2)
					{
						_animatorPlayer2.SetBool(_animIDJumpPlayer2, true);
					}
				}

				// jump timeout
				if (_jumpTimeoutDeltaPlayer2 >= 0.0f)
				{
					_jumpTimeoutDeltaPlayer2 -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDeltaPlayer2 = Player2JumpTimeout;

				// fall timeout
				if (_fallTimeoutDeltaPlayer2 >= 0.0f)
				{
					_fallTimeoutDeltaPlayer2 -= Time.deltaTime;
				}
				else
				{
					// update animator if using character
					if (_hasAnimatorPlayer2)
					{
						_animatorPlayer2.SetBool(_animIDFreeFallPlayer2, true);
					}
				}

				// if we are not grounded, do not jump
				_inputPlayer2.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocityPlayer2 < _terminalVelocityPlayer2)
			{
				_verticalVelocityPlayer2 += Player2Gravity * Time.deltaTime;
			}
		}
		
		// private void OnDrawGizmosSelected()
		// {
		// 	Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		// 	Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
		//
		// 	if (Player1Grounded) Gizmos.color = transparentGreen;
		// 	else Gizmos.color = transparentRed;
		// 	
		// 	// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
		// 	Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		// }
    }
}