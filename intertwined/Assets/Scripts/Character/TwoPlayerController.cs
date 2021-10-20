﻿using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(PlayerInput))]
    public class TwoPlayerController : MonoBehaviour
    {
        // player objects
        private Character _boy;
        private Character _dog;

        // general objects
        private GameObject _mainCamera;
        private GameInputs _input;

        public AnxietyCalc AnxietyCalc;


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
            _boy = GameObject.FindGameObjectWithTag("Boy").GetComponent<Character>();
            _dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Character>();
        }

        private void Update()
        {
            Move();
            Interact();
        }

        private void Move()
        {
            _boy.Move(_input.boyMove, _mainCamera);
            _dog.Move(_input.dogMove, _mainCamera);
        }

        private void Interact()
        {
            if (_input.boyInteract && _input.dogInteract)
            {
                AnxietyCalc.LowerAnxiety();
            }
            _boy.CharInteractor.Interact(_input.boyInteract);
            _dog.CharInteractor.Interact(_input.dogInteract);
        }
    }
}