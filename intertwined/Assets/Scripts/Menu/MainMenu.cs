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
        private GameObject intro1;
        private GameObject intro2;
        private GameObject intro3;
        private void Start()
        {
            _eventSystem = EventSystem.current;
            _playButton = transform.Find("PlayButton").gameObject;
            _eventSystem.SetSelectedGameObject(_playButton);
        }

        public void PlayGame()
        {
            //This loads the comic first
            //comic = parent.Find("Comic").gameObject;
            //comic.SetActive(true);
            ComicSequenceAndStartLevel();

        }

        private void ComicSequenceAndStartLevel()
        {
            var parent = gameObject.transform.parent;
            intro1 = parent.Find("Intro1").gameObject;
            intro2 = parent.Find("Intro2").gameObject;
            intro3 = parent.Find("Intro3").gameObject;
            intro1.SetActive(true); 
            Invoke("PanelTwo", 3);
        }
        private void PanelTwo()
        {
            intro1.SetActive(false);
            intro2.SetActive(true);
            Invoke("PanelThree", 3);
        }

        private void PanelThree()
        {
            intro2.SetActive(false);
            intro3.SetActive(true);
            Invoke("StartParkLevel", 3);
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