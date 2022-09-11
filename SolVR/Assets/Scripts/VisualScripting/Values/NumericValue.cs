using System;
using UnityEngine;

namespace VisualScripting.Values
{
    /// <summary>
    /// Class representing value of numeric type.
    /// </summary>
    public abstract class NumericValue : MonoBehaviour
    {
        #region Custom Methods

        /// <summary>
        /// Returns a value.
        /// </summary>
        /// <returns>A value of a numeric type.</returns>
        public abstract float GetValue();

        /// <summary>
        /// Checks if this object value is less than given <see cref="NumericValue"/> object's one.
        /// </summary>
        /// <param name="rightNumericValue" cref="NumericValue"/> object that is compared to this object.</param>
        /// <returns>A boolean that determines if value is less then given <see cref="rightNumericValue"/>.</returns>
        public bool LessThan(NumericValue rightNumericValue)
        {
            return GetValue() < rightNumericValue.GetValue();
        }

        /// <summary>
        /// Checks if this object value is greater than given <see cref="NumericValue"/> object's one.
        /// </summary>
        /// <param name="rightNumericValue" cref="NumericValue"/> object that is compared to this object.</param>
        /// <returns>A boolean that determines if value is greater then given <see cref="rightNumericValue"/>.</returns>
        public bool GreaterThan(NumericValue rightNumericValue)
        {
            return GetValue() > rightNumericValue.GetValue();
        }

        /// <summary>
        /// Checks if this object value is equal to given <see cref="NumericValue"/> object's one.
        /// </summary>
        /// <param name="rightNumericValue" cref="NumericValue"/> object that is compared to this object.</param>
        /// <returns>A boolean that determines if value is equal to given <see cref="rightNumericValue"/>.</returns>
        public bool EqualTo(NumericValue rightNumericValue)
        {
            return Math.Abs(GetValue() - rightNumericValue.GetValue()) < float.Epsilon;
        }

        #endregion
    }
}