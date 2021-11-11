using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Component for incrementing and decrementing a value of a <see cref="TMP_InputField"/>.
    /// </summary>
    /// <remarks>Will only work if the text of the input field can be parsed to an integer.</remarks>
    public class IncrementInputField : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Button for incrementing the input field.
        /// </summary>
        [SerializeField]
        private Button incrementButton;

        /// <summary>
        /// Button for decrementing the input field.
        /// </summary>
        [SerializeField]
        private Button decrementButton;

        /// <summary>
        /// Flag showing whether the input field can be decremented below zero.
        /// </summary>
        [SerializeField]
        private bool allowNegativeValues;

        #endregion

        #region Variables

        /// <summary>Input field, which value will be changed.</summary>
        private TMP_InputField _inputField;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize the input filed variable.
        /// </summary>
        void Start()
        {
            _inputField = GetComponent<TMP_InputField>();
        }

        /// <summary>
        /// Adds listeners for increment and decrement buttons.
        /// </summary>
        private void OnEnable()
        {
            incrementButton.onClick.AddListener(Increment);
            decrementButton.onClick.AddListener(Decrement);
        }

        /// <summary>
        /// Removes listeners for increment and decrement buttons.
        /// </summary>
        private void OnDisable()
        {
            incrementButton.onClick.RemoveListener(Increment);
            decrementButton.onClick.RemoveListener(Decrement);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Increments the text of the input field if it's an integer.
        /// </summary>
        private void Increment()
        {
            // if the text of the input field can't be parsed, exit the function
            if (!int.TryParse(_inputField.text, out var value)) return;

            _inputField.text = (value + 1).ToString();
        }

        /// <summary>
        /// Decrements the text of the input field if it's an integer and can be decremented.
        /// </summary>
        private void Decrement()
        {
            // if the text of the input field can't be parsed, exit the function
            if (!int.TryParse(_inputField.text, out var value)) return;

            // check if the value can be decremented
            if (allowNegativeValues || (!allowNegativeValues && value >= 0))
                _inputField.text = (value + 1).ToString();
        }

        #endregion
    }
}