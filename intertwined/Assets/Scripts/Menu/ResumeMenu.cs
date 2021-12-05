using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class ResumeMenu : MonoBehaviour
    {

        public void QuitMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}