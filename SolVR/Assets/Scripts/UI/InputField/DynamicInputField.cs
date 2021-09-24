using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using VisualCoding.Values.Enums;

namespace UI.InputField
{
    /// <summary>
    /// Class validating Input Field to accept values based on selected ValueDataType value.
    /// </summary>
    public class DynamicInputField : DataTypeInputField<dynamic>
    {
        #region Variables

        public ValueDataType Type { get; set; } // type of the data that is provided

        #endregion

        #region Built-in methods

        /// <summary>
        /// Subscribes to events. 
        /// </summary>
        private new void OnEnable()
        {
            base.OnEnable();
            inputField.onValidateInput += DynamicValidate;
        }

        /// <summary>
        /// Initializes input content type.
        /// </summary>
        private void Start()
        {
            inputField.contentType = TMP_InputField.ContentType.Custom;
            SetKeyboardType();
            inputField.lineType = TMP_InputField.LineType.SingleLine;
        }


        /// <summary>
        /// Unsubscribes from all events. 
        /// </summary>
        private new void OnDisable()
        {
            base.OnDisable();
            inputField.onValidateInput -= DynamicValidate;
        }

        #endregion

        #region Custom methods

        /// <summary>
        /// Parse string to data type based on <c>ValueDataType Type</c> value.
        /// </summary>
        /// <param name="input">A string that will be parsed.</param>
        /// <returns>Parsed value of string in matched type return as dynamic type.</returns>
        protected override dynamic Parse(string input)
        {
            return Type switch
            {
                ValueDataType.Integer => int.Parse(input),
                ValueDataType.Float => float.Parse(input),
                ValueDataType.String => input,
                ValueDataType.Boolean => bool.Parse(input),
                _ => input
            };
        }

        /// <summary>
        /// Validates added character and the whole string to schema based on selected type and
        /// returns character based on result.
        /// </summary>
        /// <param name="input">Provided input string before added char.</param>
        /// <param name="charIndex">An index in the string that char will be on in the string.</param>
        /// <param name="addedChar">A character that is validated.</param>
        /// <returns>Not changed char if it passes the validation or empty char if it's not.</returns>
        private char DynamicValidate(string input, int charIndex, char addedChar)
        {
            var newInput = input + addedChar;
            switch (Type)
            {
                case ValueDataType.Integer:
                    return Regex.IsMatch(newInput, @"^[-]?[0-9]*$") ? addedChar : '\0';
                case ValueDataType.Float:
                    return Regex.IsMatch(newInput, @"^[-]?([0-9]*\.?[0-9]*)$") ? addedChar : '\0';
                case ValueDataType.Boolean:
                    return Regex.IsMatch(newInput.ToLower(), @"^(true|tru|tr|[tf]|fa|fal|fals|false)?$")
                        ? addedChar
                        : '\0';
                default:
                    return addedChar;
            }
        }


        /// <summary>
        /// Parses input field value and sets keyboard type based on changed data type.
        /// </summary>
        public void OnChangedValueDataType()
        {
            ParseInputValueOnChange(inputField.text);
            SetKeyboardType();
        }

        /// <summary>
        /// Sets keyboard type based on value of enum <c>ValueDataType Type</c>.
        /// </summary>
        private void SetKeyboardType()
        {
            switch (Type)
            {
                case ValueDataType.Integer:
                case ValueDataType.Float:
                    inputField.keyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
                    break;
                default:
                    inputField.keyboardType = TouchScreenKeyboardType.Default;
                    break;
            }
        }

        #endregion
    }
}