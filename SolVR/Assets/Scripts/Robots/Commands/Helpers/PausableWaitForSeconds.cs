using UnityEngine;
using Utils;

namespace Robots.Commands.Helpers
{
    /// <summary>
    /// A custom yield instruction which waits for a given amount of seconds and can be paused.
    /// </summary>
    public class PausableWaitForSeconds : CustomYieldInstruction
    {
        #region Variables

        // a flag (bool wrapped in an object) showing whether the waiting should be paused
        private readonly Wrapper<bool> _isPaused;
        private float _timer; // the remaining number of seconds the custom yield instruction should wait for

        /// <summary>
        /// Variable representing whether enough time has passed.
        /// If the pause flag isn't set, reduces the timer by the number of seconds passed since the last frame. Next it
        /// returns false if enough time has passed and true if not.
        /// </summary>
        public override bool keepWaiting
        {
            get
            {
                if (!_isPaused.Value) _timer -= Time.deltaTime;
                return !(_timer <= 0f);
            }
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Initializes all necessary variables. 
        /// </summary>
        /// <param name="timer">The number of seconds the custom yield instruction should wait for.</param>
        /// <param name="isPaused">A bool wrapped in an object used to check whether showing whether the waiting should
        /// be paused</param>
        public PausableWaitForSeconds(float timer, Wrapper<bool> isPaused)
        {
            _timer = timer;
            _isPaused = isPaused;
        }

        #endregion
    }
}