using UnityEngine;

namespace VisualCoding.Values
{
    /// <summary>
    /// Class representing a constant value with dynamically matched type.
    /// </summary>
    public class Constant : Value
    {
        #region Variables

        [SerializeField] [Tooltip("A constant value of dynamic type.")]
        private dynamic value;

        public dynamic Value
        {
            get => value;
            set => this.value = value;
        } // a constant value

        #endregion

        #region Custom methods

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