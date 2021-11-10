using UnityEngine.Events;

namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can be crashed.
    /// </summary>
    public interface ICrashable
    {
        #region Custom Methods

        /// <summary>
        /// Method that add <see cref="invokeOnCrush"/> as listener to crash event.
        /// </summary>
        /// <param name="invokeOnCrush">Method that will be invoked when robot is crashed.</param>
        void AddListenerOnCrash(UnityAction invokeOnCrush);

        /// <summary>
        /// Method that removes a listener, that listens on crush event, based on <see cref="invokeOnCrush"/> parameter.
        /// </summary>
        /// <param name="invokeOnCrush">Method that will be removed from being a listener on crush event.</param>
        void RemoveListenerOnCrash(UnityAction invokeOnCrush);

        /// <summary>
        /// Method that handles a crash action on robot.
        /// </summary>
        void Crash();

        #endregion
    }
}