using Patterns;
using UnityEngine;

namespace DeveloperTools
{
    /// <summary>
    /// Class responsible for logging messages from <see cref="Logger"/> events to Unity Console.
    /// </summary>
    public class UnityConsoleLogger : Singleton<UnityConsoleLogger>
    {
        /// <summary>
        /// Subscribes Unity Console log method to <see cref="Logger"/> event.
        /// </summary>
        private void OnEnable()
        {
            Logger.LogEvent += Debug.Log;
        }

        /// <summary>
        /// Unsubscribes Unity Console log method from <see cref="Logger"/> event.
        /// </summary>
        private void OnDisable()
        {
            Logger.LogEvent -= Debug.Log;
        }
    }
}
