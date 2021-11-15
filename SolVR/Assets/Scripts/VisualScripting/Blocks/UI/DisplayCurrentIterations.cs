using TMPro;
using UnityEngine;
using VisualScripting.Blocks.LogicBlocks.Loop;

namespace VisualScripting.Blocks.UI
{
    /// <summary>
    /// Component for displaying <see cref="ForBlock"/> iteration count on a TMP text filed.
    /// </summary>
    public class DisplayCurrentIterations : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Block which iteration count will be displayed.
        /// </summary>
        [SerializeField]
        private ForBlock forBlock;

        #endregion

        #region Variables

        /// <summary>
        /// Text for displaying the iteration count.
        /// </summary>
        private TextMeshProUGUI _iterationsText;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes the text field component.
        /// </summary>
        private void Start()
        {
            _iterationsText = GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Registers handler for an event raised when iteration count changes.
        /// </summary>
        private void OnEnable()
        {
            forBlock.IterationChanged += UpdateText;
        }

        /// <summary>
        /// Removes handler for an event raised when iteration count changes.
        /// </summary>
        private void OnDisable()
        {
            forBlock.IterationChanged -= UpdateText;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Updates the text field with the iteration count value.
        /// </summary>
        /// <param name="iteration">New iteration count.</param>
        private void UpdateText(int iteration)
        {
            _iterationsText.text = iteration.ToString();
        }

        #endregion
    }
}