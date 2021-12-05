using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Controls;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {

        public GameObject _pauseMenuUI;

        public static bool IsPaused = false;
        private GameInputs _input;

        private float timeSincePause = 0;
        private float pauseCooldown = 1;
        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInputs>();
            Pause();
        }

        private void Update()
        {
            timeSincePause += Time.unscaledDeltaTime;
            if (_input.paused && timeSincePause>pauseCooldown)
            {
                if (IsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
                timeSincePause = 0;
            }

            if (_input.mainmenu)
            {
                ReturnToMenu();
            }
        }

        public void ReturnToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
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