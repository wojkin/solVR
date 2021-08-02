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
            CustomSceneManager.Instance.QueueLoadScene(scriptableObject.scene);
        }
    }
}