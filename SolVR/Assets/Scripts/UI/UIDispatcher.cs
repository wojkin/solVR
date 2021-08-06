using Managers;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class for running game manager functionalities from UI. 
    /// </summary>
    public class UIDispatcher : MonoBehaviour
    {
        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame()
        {
            GameManager.ExitGame();
        }
        
        /// <summary>
        /// Loads a scene based on scene parameter in provided scriptableObject.
        /// </summary>
        /// <param name="scriptableObject">Scriptable object of Environment type
        /// with specified scene parameter as reference to addressable scene.</param>
        public void LoadScene(Environment scriptableObject)
        {
            CustomSceneManager.Instance.QueueLoadScene(scriptableObject.scene);
        }

        /// <summary>
        /// Manages pausing and resuming the game.
        /// </summary>
        public void TogglePauseGame()
        {
            if (GameManager.gameIsPaused)
                GameManager.ResumeGame();
            else
                GameManager.PauseGame();
        }
    }
}