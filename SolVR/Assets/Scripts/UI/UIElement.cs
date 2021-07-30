using UnityEngine;

namespace UI
{
    /// <summary>
    /// A class representing a UI element.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UIElement : MonoBehaviour
    { 
        private Canvas _canvas; // Canvas attached to the gameObject

        // a flag showing whether the UIElement is currently visible or hidden 
        public bool IsVisible { get; private set; }

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        
        /// <summary>
        /// Disables UI canvas (invisible in scene).
        /// </summary>
        public void Hide()
        {
            _canvas.enabled = false;
            IsVisible = false;
        }

        /// <summary>
        /// Enables UI canvas (visible in scene).
        /// </summary>
        public void Show()
        {
            _canvas.enabled = true;
            IsVisible = true;
        }
    }
}
