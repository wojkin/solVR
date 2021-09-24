using UnityEngine;

namespace UI
{
    /// <summary>
    /// Manager for displaying UI elements. Makes sure only one UIElement object is visible.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private UIElement displayed;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Shows UIElement specified in displayed parameter 
        /// </summary>
        private void Start()
        {
            displayed?.Show();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Hides already displayed UIElement and show UIElement provided as parameter
        /// </summary>
        /// <param name="element">element to display instead of already displayed</param>
        public void ShowElement(UIElement element)
        {
            displayed?.Hide();
            element.Show();
            displayed = element;
        }

        #endregion
    }
}