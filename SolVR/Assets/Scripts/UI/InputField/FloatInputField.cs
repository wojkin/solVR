using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace UI.InputField
{
    /// <summary>
    /// Class validating Input Field to accept only float values.
    /// </summary>
    public class FloatInputField : DataTypeInputField<float>
    {
        #region Serialized Fields

        [Tooltip("Flag for setting if negative values should be allowed.")] [SerializeField]
        private bool allowNegativeValues;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes input content type.
        /// </summary>
        private void Start()
        {
            inputField.contentType = TMP_InputField.ContentType.Custom;
            inputField.keyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
            inputField.lineType = TMP_InputField.LineType.SingleLine;
        }

        /// <summary>
        /// Subscribes to events. 
        /// </summary>
        private new void OnEnable()
        {
            base.OnEnable();
            inputField.onValidateInput += FloatValidate;
        }

        /// <summary>
        /// Unsubscribes from all events. 
        /// </summary>
        private new void OnDisable()
        {
            base.OnDisable();
            inputField.onValidateInput -= FloatValidate;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Try to parse string to float.
        /// </summary>
        /// <param name="input">A string that will be parsed to float.</param>
        /// <param name="parsed">A float that is a parsed string.</param>
        /// <returns><inheritdoc/></returns>
        protected override bool TryParse(string input, out float parsed)
        {
            return float.TryParse(input, out parsed);
        }

        /// <summary>
        /// Validates added character and the whole string to float schema and returns character based on result.
        /// </summary>
        /// <param name="input">Provided input string before added char.</param>
        /// <param name="charIndex">An index in the string that char will be on in the string.</param>
        /// <param name="addedChar">A character that is validated.</param>
        /// <returns>Not changed char if it passes the validation or empty char if it's not.</returns>
        private char FloatValidate(string input, int charIndex, char addedChar)
        {
            if (!allowNegativeValues && addedChar == '-') // negative values are not allowed and char is a minus
                addedChar = '\0'; // change it to an empty character
            else if (!Regex.IsMatch(input + addedChar, @"^[-]?([0-9]*\.?[0-9]*)$"))
                addedChar = '\0'; // change it to an empty character
            return addedChar;
        }

        #endregion
    }
}