using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Logger = DeveloperTools.Logger;

namespace UI.InputField
{
    /// <summary>
    /// Class validating Input Field to accept only values of T type.
    /// </summary>
    /// <typeparam name="T">Data type in which data will be provided to an input field.</typeparam>
    [RequireComponent(typeof(TMP_InputField))]
    public abstract class DataTypeInputField<T> : MonoBehaviour
    {
        #region Serialized Fields

        [Tooltip("Event that invokes on value changed in input field.")] [SerializeField]
        protected UnityEvent<T> onInputValueChanged;

        #endregion

        #region Variables

        /// <summary>an input field that is validated</summary>
        protected TMP_InputField inputField;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes fields.
        /// </summary>
        protected void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
        }

        /// <summary>
        /// Subscribes to event. 
        /// </summary>
        protected void OnEnable()
        {
            inputField.onValueChanged.AddListener(ParseInputValueOnChange);
        }

        /// <summary>
        /// Unsubscribes from event. 
        /// </summary>
        protected void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(ParseInputValueOnChange);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Parse string to T data type.
        /// </summary>
        /// <param name="input">A string that will be parsed to T data type.</param>
        /// <param name="parsed">A T value that is a parsed string.</param>
        /// <returns>A boolean representation of parsing success.</returns>
        protected abstract bool TryParse(string input, out T parsed);

        /// <summary>
        /// Parses the value to specified type and invokes <see cref="onInputValueChanged"/> event.
        /// Changes color of the input field based of a result of parsing input value.
        /// </summary>
        /// <param name="value">A string value that will be parsed.</param>
        protected void ParseInputValueOnChange(string value)
        {
            if (value == "")
            {
                inputField.image.color = Color.white;
                return;
            }

            if (TryParse(value, out var parsedValue))
            {
                onInputValueChanged.Invoke(parsedValue);
                inputField.image.color = Color.white;
            }
            else
            {
                inputField.image.color = Color.red;
            }
        }

        /// <summary>
        /// Setter for input field text.
        /// </summary>
        /// <param name="value">New value that will be set to input field text.</param>
        /// <returns>A boolean, true if value was set, otherwise false.</returns>
        public void SetInputValue(T value)
        {
            inputField.text = value.ToString();
        }


        /// <summary>
        /// Getter for input field text.
        /// </summary>
        /// <returns>A T value in input field.</returns>
        public T GetInputValue()
        {
            if (!TryParse(inputField.text, out var value))
                Logger.Log($"Can't parse input field content to {typeof(T)} type.");
            return value;
        }

        #endregion
    }
}