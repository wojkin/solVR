using TMPro;

namespace UI.InputField
{
    /// <summary>
    /// Class validating Input Field to accept only int values.
    /// </summary>
    public class IntegerInputField : DataTypeInputField<int>
    {
        /// <summary>
        /// Initializes input content type.
        /// </summary>
        private void Start()
        {
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        /// <summary>
        /// Parse string to int.
        /// </summary>
        /// <param name="input">A string that will be parsed to int.</param>
        /// <returns>Parsed value of string in int type.</returns>
        protected override int Parse(string input)
        {
            return int.Parse(input);
        }
    }
}