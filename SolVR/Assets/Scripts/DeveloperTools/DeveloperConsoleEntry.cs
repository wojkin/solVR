using TMPro;
using UnityEngine;

namespace DeveloperTools
{
    /// <summary>
    /// Class representing a single entry in the developer console list.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DeveloperConsoleEntry : MonoBehaviour
    {
        #region Variables

        private TextMeshProUGUI _entryText; // test visible in the list

        #endregion

        #region Custom Methods

        /// <summary>
        /// Activates the gameobject and sets its text to a string.
        /// </summary>
        /// <param name="text">The string to which the text of the list entry will be set.</param>
        public void SetText(string text)
        {
            // activate the gameobject
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);

            // set the text
            if (_entryText == null)
                _entryText = GetComponent<TextMeshProUGUI>();
            _entryText.text = text;
        }

        #endregion
    }
}