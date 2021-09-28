using UnityEngine;

namespace VisualCoding.Values
{
    /// <summary>
    /// Class representing a constant value with dynamically matched type.
    /// </summary>
    public class Constant : Value
    {
        #region Serialized Fields

        /// <summary>A value of <see cref="Constant"/> that has dynamic type.</summary>
        [SerializeField] [Tooltip("A value of Constant that has dynamic type.")]
        private dynamic value;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="value"/></summary>
        public dynamic Value
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
        public override dynamic GetValue()
        {
            return Value;
        }

        #endregion
    }
}