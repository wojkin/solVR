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

        // a flag showing whether the canvas is currently visible or hidden and setting canvas visibility
        public bool IsVisible { get => _canvas.enabled; private set => _canvas.enabled = value; }

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        
        /// <summary>
        /// Hides UI canvas (invisible in scene).
        /// </summary>
        public void Hide()
        {
            IsVisible = false;
        }

        /// <summary>
        /// Shows UI canvas (visible in scene).
        /// </summary>
        public void Show()
        {
            IsVisible = true;
        }
    }
}
