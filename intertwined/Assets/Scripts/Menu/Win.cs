using Character;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class Win : Interactable.Interactable
    {
        private bool _dogMadeIt = false;
        private bool _boyMadeIt = false;

        private bool LevelComplete => _boyMadeIt && _dogMadeIt;
        
        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (!enteredTrigger) return;

            if (interacter.charType == CharType.Boy)
                _boyMadeIt = true;
            else if (interacter.charType == CharType.Dog)
                _dogMadeIt = true;
            
            if (LevelComplete) GoToMainMenu();
        }
        
        private void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
