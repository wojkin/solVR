using System;
using UnityEngine;

namespace VisualScripting.Values.BooleanValues
{
    /// <summary>
    /// Class representing boolean value as a result of a relational operation on two boolean values.
    /// </summary>
    public class RelationalOperation : BooleanValue
    {
        #region Serialized Fields

        /// <summary>Relational operation between on left and right boolean values.</summary>
        [SerializeField] [Tooltip("Relational operation between on left and right boolean values.")]
        private Enums.RelationalOperator @operator;

        /// <summary>Boolean value that is on the left of the relational operand.</summary>
        [SerializeField] [Tooltip("Numeric value that is on the left of the relational operand.")]
        private NumericValue left;

        /// <summary>Boolean value that is on the right of the relational operand.</summary>
        [SerializeField] [Tooltip("Numeric value that is on the right of the relational operand.")]
        private NumericValue right;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="operator"/></summary>
        public Enums.RelationalOperator Operator
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
        /// Check and returns boolean that is a result of relational operation.
        /// </summary>
        /// <returns>A result of relational operation on left and right boolean values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <see cref="Operator"/> is not handled.</exception>
        public override bool GetValue()
        {
            return Operator switch
            {
                Enums.RelationalOperator.Equal => Left.EqualTo(Right),
                Enums.RelationalOperator.LessThan => Left.LessThan(Right),
                Enums.RelationalOperator.GreaterThan => Left.GreaterThan(Right),
                _ => throw new ArgumentOutOfRangeException(Operator.ToString(), "This operation is not handled.")
            };
        }

        #endregion
    }
}