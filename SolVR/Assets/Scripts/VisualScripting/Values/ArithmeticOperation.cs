using System;
using UnityEngine;

namespace VisualScripting.Values
{
    /// <summary>
    /// Class representing value as a result of a arithmetic operation on two values.
    /// </summary>
    public class ArithmeticOperation : NumericValue
    {
        #region Serialized Fields

        /// <summary>Arithmetic operation performed on right and left values.</summary>
        [SerializeField] [Tooltip("Arithmetic operation performed on right and left values.")]
        private Enums.ArithmeticOperator @operator;

        /// <summary>A value that is on the left of the operand.</summary>
        [SerializeField] [Tooltip("A value that is on the left of the operand.")]
        private NumericValue left;

        /// <summary>A value that is on the right of the operand.</summary>
        [SerializeField] [Tooltip("A value that is on the right of the operand.")]
        private NumericValue right;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="operator"/></summary>
        public Enums.ArithmeticOperator Operator
        {
            get => @operator;
            set => @operator = value;
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
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <see cref="Operator"/> is not handled.</exception>
        public override float GetValue()
        {
            return Operator switch
            {
                Enums.ArithmeticOperator.Addition => left.GetValue() + right.GetValue(),
                Enums.ArithmeticOperator.Difference => left.GetValue() - right.GetValue(),
                Enums.ArithmeticOperator.Multiplication => left.GetValue() * right.GetValue(),
                _ => throw new ArgumentOutOfRangeException(Operator.ToString(), "This operation is not handled.")
            };
        }

        #endregion
    }
}