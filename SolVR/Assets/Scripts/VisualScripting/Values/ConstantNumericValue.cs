using UnityEngine;

namespace VisualScripting.Values
{
    /// <summary>
    /// Class representing a constant value with numeric type.
    /// </summary>
    public class ConstantNumericValue : NumericValue
    {
        #region Serialized Fields

        /// <summary>A value of <see cref="ConstantNumericValue"/> that has numeric type.</summary>
        [SerializeField] [Tooltip("A value of Constant that has numeric type.")]
        private float value;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="value"/></summary>
        public float Value
        {
            get => value;
            set => this.value = value;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Returns a constant value.
        /// </summary>
        /// <returns>A dynamic constant value that is represented by this class object.</returns>
        public override float GetValue()
        {
            return Value;
        }

        #endregion
    }
}