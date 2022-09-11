using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeveloperTools
{
    ///<summary>
    /// Class for managing the loading of the _preload scene.
    /// </summary>
    public class DevPreloadScene : MonoBehaviour
    {
        #region Built-in Methods

        /// <summary>
        /// Checks if the _preload scene should be loaded.
        /// </summary>
        private void Awake()
        {
            // checks if an object named "__app" is in the scene, if it's not, the _preload scene is loaded
            if (GameObject.Find("__app") == null) SceneManager.LoadScene("_preload", LoadSceneMode.Single);
        }

        #endregion
    }
}