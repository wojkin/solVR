using Managers;
using UnityEngine;

namespace Dev
{
    ///<summary>
    /// Class for managing the loading of the _preload scene.
    /// </summary>
    public class DevPreloadScene : MonoBehaviour
    {
        #region Built-in Methods

        /// <summary>
        /// Checks if an object named "__app" is in the scene. If it's not, the _preload scene is loaded. If the "__app"
        /// object is already in the scene, it means that the _preload scene was loaded before.
        /// </summary>
        private void Awake()
        {
            if (GameObject.Find("__app") == null) CustomSceneManager.Instance.QueueLoadScene("_preload");
        }

        #endregion
    }
}