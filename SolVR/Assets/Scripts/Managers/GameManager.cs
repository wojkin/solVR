using Patterns;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Managers
{
    /// <summary>
    /// Handles main game functionalities.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static bool gameIsPaused = false; // flag showing if game is paused

        /// <summary>
        /// Quits the application.
        /// </summary>
        public void ExitGame() 
        {
            Application.Quit();
        }
        
        /// <summary>
        /// Loads a scene based on scene parameter in provided scriptableObject.
        /// </summary>
        /// <param name="scriptableObject">Scriptable object of Environment type
        /// with specified scene parameter as reference to addressable scene.</param>
        public void LoadScene(Environment scriptableObject)
        {
            Addressables.LoadSceneAsync(scriptableObject.scene);
        }
        
        /// <summary>
        /// Manages pausing and resuming the game.
        /// </summary>
        public void TogglePauseGame()
        {
            if(gameIsPaused)
                ResumeGame();
            else
                PauseGame();
            gameIsPaused = !gameIsPaused;
        }
        
        /// <summary>
        /// Pausing the game.
        /// </summary>
        private void PauseGame ()
        {
            Time.timeScale = 0f;
        }

        /// <summary>
        /// Resuming the game.
        /// </summary>
        private void ResumeGame ()
        {
            Time.timeScale = 1;
        }
    }
}
