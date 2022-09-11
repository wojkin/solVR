using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// A base class for all classes which need to have only a single instance and be globally accessible.
    /// </summary>
    /// <remarks>Unlike a singleton it doesn't create a new instance when no other instance exists.</remarks>
    public class SingleGlobalInstance<T> : MonoBehaviour where T : Component
    {
        #region Variables

        /// <summary>Instance of the class.</summary>
        private static T _instance;

        /// <summary>A globally accessible instance of the class.</summary>
        public static T Instance => _instance;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Makes sure only one instance exists.
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