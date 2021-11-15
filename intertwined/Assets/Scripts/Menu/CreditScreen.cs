using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class CreditScreen : MonoBehaviour
    {

        public void ReturnToMenu(InputAction.CallbackContext context)
        {
            SceneManager.LoadScene(0);
        }
    }
}