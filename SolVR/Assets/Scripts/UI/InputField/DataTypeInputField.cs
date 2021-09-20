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

        [Tooltip("Event that invokes on value changed in input field.")] [SerializeField]
        protected UnityEvent<T> onInputValueChanged;

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
        protected void ParseInputValueOnChange(string value)
        {
            if (value == "")
            {
                inputField.image.color = Color.white;
                return;
            }
            try
            {
                var parsedValue = Parse(value);
                onInputValueChanged.Invoke(parsedValue);
                inputField.image.color = Color.white;
            }
            catch (FormatException)
            {
                inputField.image.color = Color.red;
            }
        }
    }
}