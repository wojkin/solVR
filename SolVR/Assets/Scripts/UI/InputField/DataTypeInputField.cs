using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.InputField
{
    /// <summary>
    /// Class validating Input Field to accept only values of T type.
    /// </summary>
    /// <typeparam name="T">Data type in which data will be provided to an input field.</typeparam>
    [RequireComponent(typeof(TMP_InputField))]
    public abstract class DataTypeInputField<T> : MonoBehaviour
    {
        protected TMP_InputField inputField; // an input field that is validated

        [Tooltip("Event that invokes on value changed in input field.")]
        [SerializeField] private UnityEvent<T> onInputValueChanged;
        
        [Tooltip("Flag for setting if negative values should be allowed.")]
        [SerializeField] private bool allowNegativeValues;

        /// <summary>
        /// Initializes fields.
        /// </summary>
        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
        }
        
        /// <summary>
        /// Subscribes to events. 
        /// </summary>
        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(ParseOnInputValueChanged);
            inputField.onValidateInput += NegativeValidate;
        }

        /// <summary>
        /// Validates and returns char based on <c>allowNegativeValues</c> flag.
        /// </summary>
        /// <param name="input">Provided input string before added char.</param>
        /// <param name="charIndex">An index in the string that char will be on in the string.</param>
        /// <param name="addedChar">A character that is validated.</param>
        /// <returns>Not changed char if it passes the validation or empty char if it's not.</returns>
        private char NegativeValidate(string input, int charIndex, char addedChar)
        {
            if (!allowNegativeValues && addedChar=='-') // negative values are not allowed and char is a minus
            {
                addedChar = '\0';  // change it to an empty character
            }
            return addedChar;
        }
        
        /// <summary>
        /// Unsubscribes from all events. 
        /// </summary>
        private void OnDisable()
        {
            inputField.onValidateInput -= NegativeValidate;
            inputField.onValueChanged.RemoveListener(ParseOnInputValueChanged);
        }

        /// <summary>
        /// Parse string to <c>T</c> data type.
        /// </summary>
        /// <param name="input">A string that will be parsed to T data type.</param>
        /// <returns>Parsed value of T type.</returns>
        protected abstract T Parse(string input);

        /// <summary>
        /// Parses the value to specified type and invokes <c>onInputValueChanged</c> event.
        /// Changes color of the input field based of a result of parsing input value.
        /// </summary>
        /// <param name="value">A string value that will be parsed.</param>
        private void ParseOnInputValueChanged(string value)
        {
            try
            {
                var parsedValue = Parse(value);
                onInputValueChanged.Invoke(parsedValue);
                inputField.image.color = Color.white;
            }
            catch (FormatException e)
            {
                inputField.image.color = Color.red;
            }
        }
    }
}