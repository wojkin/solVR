using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Handles main game functionalities.
    /// </summary>
    public static class GameManager
    {
        #region Variables

        /// <summary>a delegate for a pausing change (pause or resume)</summary>
        public delegate void PauseChange();

        /// <summary>an event invoked after the game is paused</summary>
        public static event PauseChange OnPause;

        /// <summary>an event invoked after the game is resumed</summary>
        public static event PauseChange OnResume;

        /// <summary>flag showing if game is paused</summary>
        public static bool gameIsPaused;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        static GameManager()
        {
            //  adding listener that resumes the game after loading a new scene
            CustomSceneManager.Instance.AfterLoad += (_) =>
            {
                if (gameIsPaused) // resumes game if it's paused
                    ResumeGame();
            };
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public static void ExitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Pausing the game.
        /// </summary>
        public static void PauseGame()
        {
            Time.timeScale = 0f;
            gameIsPaused = true;
            OnPause?.Invoke();
        }

        /// <summary>
        /// Resuming the game.
        /// </summary>
        public static void ResumeGame()
        {
            Time.timeScale = 1;
            gameIsPaused = false;
            OnResume?.Invoke();
        }

        #endregion
    }
}