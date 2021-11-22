using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Controls;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {

        public GameObject _pauseMenuUI;

        public static bool IsPaused = false;
        private GameInputs _input;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInputs>();
 
        }

        private void Update()
        {
            if (_input.paused)
            {
                //Toggle
                Pause();
            }
            else
            {
                Resume();
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