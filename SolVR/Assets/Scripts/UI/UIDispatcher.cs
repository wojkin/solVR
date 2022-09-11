using Managers;
using ScriptableObjects.Environments;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// Class for running game manager functionalities from UI. 
    /// </summary>
    public class UIDispatcher : MonoBehaviour
    {
        #region Custom Methods

        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame()
        {
            GameManager.ExitGame();
        }

        /// <summary>
        /// Loads a scene based on the provided <see cref="Environment"/>.
        /// </summary>
        /// <param name="scriptableObject"><see cref="Environment"/> with specified scene parameter.</param>
        public void LoadScene(Environment scriptableObject)
        {
            CustomSceneManager.Instance.QueueLoadScene(scriptableObject.scene, LoadSceneMode.Single);
        }

        /// <summary>
        /// Reloads the currently loaded scene.
        /// </summary>
        public void ReloadScene()
        {
            CustomSceneManager.Instance.QueueReloadScene();
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

        #endregion
    }
}