using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {

        private GameObject _pauseMenuUI;

        public InputActionAsset inputActionAsset;
        public static bool IsPaused = false;

        private void Start()
        {
            _pauseMenuUI = transform.Find("PauseMenuUI").gameObject;
            
            inputActionAsset.Enable();

            var pause = inputActionAsset.FindAction("Pause");

            pause.started += PauseClicked;
        }

        private void PauseClicked(InputAction.CallbackContext context)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            
        }

        public void Resume()
        {
            _pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void Pause()
        {
            _pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            IsPaused = true;
        }
    }
}