using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Handles main game functionalities.
    /// </summary>
    public static class GameManager
    {
        public static bool gameIsPaused = false; // flag showing if game is paused

        public delegate void PauseChange(); // a delegate for a pausing change (pause or resume)
        
        public static event PauseChange AfterPause; // an event invoked after the game is paused

        public static event PauseChange AfterResume; // an event invoked after the game is resumed

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
            AfterPause?.Invoke();
        }

        /// <summary>
        /// Resuming the game.
        /// </summary>
        public static void ResumeGame()
        {
            Time.timeScale = 1;
            gameIsPaused = false;
            AfterResume?.Invoke();
        }
    }
}