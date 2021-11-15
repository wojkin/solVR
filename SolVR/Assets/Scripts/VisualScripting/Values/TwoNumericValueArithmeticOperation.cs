using System;
using UnityEngine;
using VisualScripting.Values.Enums;

namespace VisualScripting.Values
{
    /// <summary>
    /// Class representing value as a result of a arithmetic operation on two values.
    /// </summary>
    public class TwoNumericValueArithmeticOperation : NumericValue
    {
        #region Serialized Fields

        /// <summary>Arithmetic operation performed on right and left values.</summary>
        [SerializeField] [Tooltip("Arithmetic operation performed on right and left values.")]
        private ArithmeticOperation operation;

        /// <summary>A value that is on the left of the operand.</summary>
        [SerializeField] [Tooltip("A value that is on the left of the operand.")]
        private NumericValue left;

        /// <summary>A value that is on the right of the operand.</summary>
        [SerializeField] [Tooltip("A value that is on the right of the operand.")]
        private NumericValue right;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="operation"/></summary>
        public ArithmeticOperation Operation
        {
            get => operation;
            set => operation = value;
        }

        /// <summary><inheritdoc cref="left"/></summary>
        public NumericValue Left
        {
            get => left;
            set => left = value;
        }

        /// <summary><inheritdoc cref="right"/></summary>
        public NumericValue Right
        {
            get => right;
            set => right = value;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Calculates and returns value that is a result of arithmetic operation.
        /// </summary>
        /// <returns>A result of arithmetic operation on left and right values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <see cref="Operation"/> is not handled.</exception>
        public override float GetValue()
        {
            return Operation switch
            {
                ArithmeticOperation.Addition => Left.GetValue() + Right.GetValue(),
                ArithmeticOperation.Difference => Left.GetValue() - Right.GetValue(),
                ArithmeticOperation.Multiplication => Left.GetValue() * Right.GetValue(),
                _ => throw new ArgumentOutOfRangeException("Operation", "This operation is not handled.")
            };
        }

        #endregion
    }
}