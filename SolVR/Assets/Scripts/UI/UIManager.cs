using UnityEngine;

namespace UI
{
    /// <summary>
    /// Manager for displaying UI elements. Makes sure only one UIElement object is visible.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary> <see cref="UIElement"/> that is displayed on start.</summary>
        [SerializeField] private UIElement displayed;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Shows <see cref="UIElement"/> specified in displayed parameter 
        /// </summary>
        private void Start()
        {
            displayed?.Show();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Hides already displayed <see cref="UIElement"/> and show <see cref="UIElement"/> provided as parameter.
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