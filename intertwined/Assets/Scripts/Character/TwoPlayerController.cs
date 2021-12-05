using Controls;
using UnityEngine;

namespace Character
{
    public class TwoPlayerController : MonoBehaviour
    {
        // player objects
        private Character _boy;
        private Character _dog;

        // general objects
        private GameObject _mainCamera;
        private GameInputs _input;

        public AnxietyCalc anxietyCalc;

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
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInputs>();
            _boy = GameObject.FindGameObjectWithTag("Boy").GetComponentInParent<Character>();
            _dog = GameObject.FindGameObjectWithTag("Dog").GetComponentInParent<Character>();
        }

        private void Update()
        {
            Move();
            Interact();
            Pet();
        }

        private void Move()
        {
            _boy.Move(_input.boyMove, _input.analogMovement, _mainCamera);
            _dog.Move(_input.dogMove, _input.analogMovement, _mainCamera);
        }

        private void Interact()
        {
            _boy.CharInteractor.Interact(_input.boyInteract);
            _dog.CharInteractor.Interact(_input.dogInteract);
        }

        private void Pet()
        {
            if (_boy.CharInteractor.HasInteractables() |
                _dog.CharInteractor.HasInteractables() |
                !anxietyCalc.CanPet()) return;

            if (_input.boyInteract & _input.dogInteract)
            {
                _boy.GetComponentInChildren<Animator> ().SetTrigger("Petting");
                anxietyCalc.LowerAnxiety();
            }
        }
    }
}