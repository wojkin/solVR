using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// A base class for all classes which need a single, always available global instance.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        #region Variables

        /// <summary>Instance of the class.</summary>
        private static T _instance;

        /// <summary>Flag representing whether the application is currently shutting down.</summary>
        private static bool _shuttingDown;

        /// <summary>
        /// A globally accessible instance of the class. Creates a new instance if it doesn't exist.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_shuttingDown) return null; // if the application is shutting down, don't return an instance

                if (_instance != null) return _instance; // if there is an instance set then return it

                // if there is no instance set, search for an exiting one and return it
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;

                // if no instance has been found then create a new one and return it
                var obj = new GameObject {name = typeof(T).Name};
                _instance = obj.AddComponent<T>();
                return _instance;
            }
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Makes sure only one instance exists.
        /// </summary>
        public virtual void Awake()
        {
            // check if the instance is set
            if (_instance == null)
            {
                // if it's not the set itself as an instance
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject); // destroy itself
            }
        }

        /// <summary>
        /// Sets a shutting down flag when this object is destroyed, so it doesn't recreate itself when the
        /// application is exiting.
        /// </summary>
        private void OnDestroy()
        {
            _shuttingDown = true;
        }

        /// <summary>
        /// Sets a shutting down flag when the application starts exiting.
        /// </summary>
        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        #endregion
    }
}