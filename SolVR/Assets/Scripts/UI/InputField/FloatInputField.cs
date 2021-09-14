using TMPro;

namespace UI.InputField
{
    /// <summary>
    /// Class validating Input Field to accept only float values.
    /// </summary>
    public class FloatInputField : DataTypeInputField<float>
    {
        /// <summary>
        /// Initializes input content type.
        /// </summary>
        private void Start()
        {
            inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        }

        /// <summary>
        /// Parse string to float.
        /// </summary>
        /// <param name="input">A string that will be parsed to float.</param>
        /// <returns>Parsed value of string in float type.</returns>
        protected override float Parse(string input)
        {
            return float.Parse(input);
        }
    }
}