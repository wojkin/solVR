namespace VisualCoding.ConditionExpression.Values
{
    /// <summary>
    /// Class representing a constant value.
    /// </summary>
    public class Constant: Value
    {
        public dynamic Value { get; set; } // a constant value

        /// <summary>
        /// Returns a constant value.
        /// </summary>
        /// <returns>A dynamic constant value that is represented by this class object.</returns>
        public override dynamic GetValue()
        {
            return Value;
        }
    }
}