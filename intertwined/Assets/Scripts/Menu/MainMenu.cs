using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject comic;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
        
        
    }
}