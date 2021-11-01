using UnityEngine;

namespace UI
{
    /// <summary>
    /// Component for automatically setting the world camera of a <see cref="Canvas"/> to the main camera in the scene.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UIConfigureWorldCamera : MonoBehaviour
    {
        #region Built-in Methods

        /// <summary>
        /// Sets the world camera if not already set.
        /// </summary>
        private void Start()
        {
            var canvas = GetComponent<Canvas>();
            if (canvas.worldCamera == null)
                canvas.worldCamera = Camera.main;
        }

        #endregion
    }
}