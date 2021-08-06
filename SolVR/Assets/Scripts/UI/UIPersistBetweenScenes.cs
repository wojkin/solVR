using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// Class which makes sure that the gameObject persist between loadings of different scenes.
    /// </summary>
    public class UIPersistBetweenScenes : MonoBehaviour
    {

        [Tooltip("UIElement attached to the same gameObject as the UI canvas.")] [SerializeField]
        private UIElement uiElement;
    
        private Canvas _worldSpaceCanvas; // canvas of the gameObject UI
    
        /// <summary>
        /// Initialize fields.
        /// </summary>
        public void Awake()
        {
            _worldSpaceCanvas = uiElement.GetComponent<Canvas>();
        }
    
        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            // subscribe to CustomSceneManager events for loading scenes
            CustomSceneManager.Instance.AfterLoad += AfterLoadHandler;
            CustomSceneManager.Instance.BeforeUnload += BeforeUnloadHandler;
        }
    
        /// <summary>
        /// Unsubscribes from all previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            // unsubscribe from previously subscribed events
            if (CustomSceneManager.Instance != null)
            {
                CustomSceneManager.Instance.AfterLoad -= AfterLoadHandler;
                CustomSceneManager.Instance.BeforeUnload -= BeforeUnloadHandler;
            }
        }
    
        /// <summary>
        /// This function will be called before a scene is unloaded. The UI element is hidden and then
        /// moved to DDOL scene. This makes the gameObject persist between scene loads.
        /// </summary>
        /// <param name="sceneName">Name of the scene which will be unloaded.</param>
        private void BeforeUnloadHandler(string sceneName)
        {
            uiElement.Hide();
            DontDestroyOnLoad(gameObject); // move the gameObject to DDOL
        }
    
        /// <summary>
        /// This function will be called after a scene is loaded. The gameObject is moved from
        /// DDOL to the currently loaded scene, so it can be accessed by other scripts.
        /// </summary>
        /// <param name="sceneName">Name of the scene, which was loaded.</param>
        private void AfterLoadHandler(string sceneName)
        {
            // move the gameObject to the current scene
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
            _worldSpaceCanvas.worldCamera = Camera.main; // set the UI camera to the main camera in the loaded scene
        }
    }
}