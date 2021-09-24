using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// A base class for all classes which need to have only a single instance and be globally accessible. Unlike a
    /// singleton it doesn't create a new instance when no other instance exists.
    /// </summary>
    public class SingleGlobalInstance<T> : MonoBehaviour where T : Component
    {
        #region Variables

        private static T _instance;

        public static T Instance => _instance;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Checks if any other instance of this class exists. If it doesn't it sets itself as the globally accessible
        /// instance, otherwise it destroys itself.
        /// </summary>
        public virtual void Awake()
        {
            // check if any other instance exists
            if (_instance == null)
            {
                _instance = this as T; // set itself as the global instance
                DontDestroyOnLoad(gameObject); // move itself to the DDOL scene
            }
            else
            {
                Destroy(gameObject); // destroy itself
            }
        }

        #endregion
    }
}