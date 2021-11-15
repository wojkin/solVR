using System;
using UnityEngine;
using VisualScripting.Values.Enums;

namespace VisualScripting.Values.BooleanValues
{
    /// <summary>
    /// Class representing boolean value as a result of a relational operation on two boolean values.
    /// </summary>
    public class BooleanValueRelationalOperators : BooleanValue
    {
        #region Serialized Fields

        /// <summary>Relational operation between on left and right boolean values.</summary>
        [SerializeField] [Tooltip("Relational operation between on left and right boolean values.")]
        private RelationalOperation operation;

        /// <summary>Boolean value that is on the left of the relational operand.</summary>
        [SerializeField] [Tooltip("Numeric value that is on the left of the relational operand.")]
        private NumericValue left;

        /// <summary>Boolean value that is on the right of the relational operand.</summary>
        [SerializeField] [Tooltip("Numeric value that is on the right of the relational operand.")]
        private NumericValue right;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="operation"/></summary>
        public RelationalOperation Operation
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
        /// Check and returns boolean that is a result of relational operation.
        /// </summary>
        /// <returns>A result of relational operation on left and right boolean values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <see cref="Operation"/> is not handled.</exception>
        public override bool GetValue()
        {
            return Operation switch
            {
                RelationalOperation.Equal => Left.EqualTo(Right),
                RelationalOperation.LessThan => Left.LessThan(Right),
                RelationalOperation.GreaterThan => Left.GreaterThan(Right),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion
    }
}