using UnityEngine;

namespace VisualCoding.ConditionExpression.Values
{
    /// <summary>
    /// Class representing value of dynamic.
    /// </summary>
    public abstract class Value : MonoBehaviour
    {
        /// <summary>
        /// Returns a value.
        /// </summary>
        /// <returns>A value of dynamic type.</returns>
        public abstract dynamic GetValue();
        
        /// <summary>
        /// Checks if this object value is less than given <c>Value</c> object's one.
        /// </summary>
        /// <param name="rightValue">A <c>Value</c> object that is compared to this object.</param>
        /// <returns>A boolean that determines if value is less then given <c>rightValue</c>.</returns>
        public bool LessThan(Value rightValue)
        {
            return GetValue() < rightValue.GetValue();
        }

        /// <summary>
        /// Checks if this object value is greater than given <c>Value</c> object's one.
        /// </summary>
        /// <param name="rightValue">A <c>Value</c> object that is compared to this object.</param>
        /// <returns>A boolean that determines if value is greater then given <c>rightValue</c>.</returns>
        public bool GreaterThan(Value rightValue)
        {
            return GetValue() > rightValue.GetValue();
        }

        /// <summary>
        /// Checks if this object value is equal to given <c>Value</c> object's one.
        /// </summary>
        /// <param name="rightValue">A <c>Value</c> object that is compared to this object.</param>
        /// <returns>A boolean that determines if value is equal to given <c>rightValue</c>.</returns>
        public bool EqualTo(Value rightValue)
        {
            return GetValue() == rightValue.GetValue();
        }
    }
}