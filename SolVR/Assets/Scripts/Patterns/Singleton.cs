using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// A base class for all classes which need a single, always available global instance.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance; // instance of the class

        // a flag representing whether the application is currently shutting down.
        private static bool _shuttingDown = false;

        /// <summary>
        /// A globally accessible instance of the class. If there is no instance of the class already set,
        /// the it searches for an existing one. If none is found, it creates a new one. If the game is being closed
        /// the no instance is returned. This is to avoid memory errors when the instance of a singleton is referenced
        /// in e.g. other objects OnDisable() methods.
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

        /// <summary>
        /// If no other instance is set, it sets this object as the instance, otherwise it destroys itself.
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
        /// Sets a shutting down flag when the application starts exiting.
        /// </summary>
        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        /// <summary>
        /// Sets a shutting down flag when this object is destroyed, so it doesn't recreate itself when the
        /// application is exiting.
        /// </summary>
        private void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
}