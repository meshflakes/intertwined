using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class MenuButton : MonoBehaviour
    {

        private EventSystem _eventSystem;
        private Button _button;
        private GameObject _parentMenu;
        
        public GameObject targetMenu;
        public GameObject firstSelected;

        private void Start()
        {
            _eventSystem = EventSystem.current;
            _button = GetComponent<Button>();
            _parentMenu = transform.parent.gameObject;
            
            _button.onClick.AddListener(LoadMenu);
        }

        private void LoadMenu()
        {
            _parentMenu.SetActive(false);
            targetMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(firstSelected);
        }
        
    }
}