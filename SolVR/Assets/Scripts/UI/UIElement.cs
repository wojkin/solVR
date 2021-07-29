using UnityEngine;

namespace UI
{
    /// <summary>
    /// A class representing a UI element.
    /// </summary>
    public class UIElement : MonoBehaviour
    {
        /// <summary>
        /// Sets UI element as not active (invisible in scene).
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets UI element as active (visible in scene).
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
