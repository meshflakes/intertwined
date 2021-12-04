using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        private EventSystem _eventSystem;
        private GameObject _playButton;
        private GameObject comic;

        private void Start()
        {
            _eventSystem = EventSystem.current;
            _playButton = transform.Find("PlayButton").gameObject;
            _eventSystem.SetSelectedGameObject(_playButton);
        }

        public void PlayGame()
        {
            var parent = gameObject.transform.parent;
            //This loads the comic first
            comic = parent.Find("Comic").gameObject;
            comic.SetActive(true);
            //Show the comic for a set time before starting the park level
            Invoke("StartParkLevel", 5);
            
        }

        private void StartParkLevel()
        {
            SceneManager.LoadScene("Scenes/ParkLevel");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
        
        
    }
}