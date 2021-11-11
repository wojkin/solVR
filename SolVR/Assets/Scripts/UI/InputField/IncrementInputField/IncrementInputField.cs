using UnityEngine;
using UnityEngine.UI;
using Logger = DeveloperTools.Logger;

namespace UI.InputField.IncrementInputField
{
    /// <summary>
    /// Component for incrementing and decrementing a value of a <see cref="DataTypeInputField"/>.
    /// </summary>
    /// <remarks>Will only work if T is corresponding to one in <see cref="DataTypeInputField"/>.</remarks>
    [RequireComponent(typeof(DataTypeInputField<>))]
    public class IncrementInputField<T> : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Button for incrementing the input field.
        /// </summary>
        [SerializeField] private Button incrementButton;

        /// <summary>
        /// Button for decrementing the input field.
        /// </summary>
        [SerializeField] private Button decrementButton;

        #endregion

        #region Variables

        /// <summary>Input field, which value will be changed.</summary>
        private DataTypeInputField<T> _dataTypeInputField;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize the input filed variable.
        /// </summary>
        void Start()
        {
            _dataTypeInputField = GetComponent<DataTypeInputField<T>>();
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
        /// Tries to increment the text of the input field.
        /// </summary>
        private void Increment()
        {
            try
            {
                T incremented = (dynamic)_dataTypeInputField.GetInputValue() + 1;
                _dataTypeInputField.TrySetInputValue(incremented);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                Logger.Log($"Can't increment input value: {ex.Message}");
            }
        }

        /// <summary>
        /// Tries to decrement the text of the input field.
        /// </summary>
        private void Decrement()
        {
            try
            {
                T decremented = (dynamic)_dataTypeInputField.GetInputValue() - 1;
                _dataTypeInputField.TrySetInputValue(decremented);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                Logger.Log($"Can't decrement input value: {ex.Message}");
            }
        }

        #endregion
    }
}