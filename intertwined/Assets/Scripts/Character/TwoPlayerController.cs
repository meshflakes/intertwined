using InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [RequireComponent(typeof(PlayerInput))]
    public class TwoPlayerController : MonoBehaviour
    {
        // player objects
        [SerializeField] private Character boy;
        [SerializeField] private Character dog;

        // general objects
        private GameObject _mainCamera;
        private GameInputs _input;


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
            _input = GetComponent<GameInputs>();
        }

        private void Update()
        {
            Move();
            Interact();
        }

        private void Move()
        {
            boy.Move(_input.boyMove, _mainCamera);
            dog.Move(_input.dogMove, _mainCamera);
        }

        private void Interact()
        {
            boy.Interact(_input.boyInteract);
            dog.Interact((_input.dogInteract));
        }
    }
}