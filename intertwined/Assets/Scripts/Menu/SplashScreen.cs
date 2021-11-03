using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Menu
{
    public class SplashScreen : MonoBehaviour
    {

        private EventSystem _eventSystem;
        private GameObject _mainMenu;
        private GameObject _playButton;

        private void Start()
        {
            var parent = gameObject.transform.parent;
            
            _eventSystem = EventSystem.current;
            _mainMenu = parent.Find("MainMenu").gameObject;
            _playButton = _mainMenu.transform.Find("PlayButton").gameObject;
        }

        public void Begin(InputAction.CallbackContext context)
        {
            gameObject.SetActive(false);
            _mainMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(_playButton);
        }
    }
}
