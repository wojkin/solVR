using System;
using UnityEngine;
using VisualCoding.Values.Enums;

namespace VisualCoding.Values.BooleanValues
{
    /// <summary>
    /// Class representing boolean value as a result of a relational operation on two boolean values.
    /// </summary>
    public class BooleanValueRelationalOperators : BooleanValue
    {
        #region Serialized Fields

        [SerializeField] [Tooltip("Relational operation between on left and right boolean values.")]
        private RelationalOperation operation;

        [SerializeField] [Tooltip("Boolean value that is on the left of the relational operand.")]
        private Value leftValue;

        [SerializeField] [Tooltip("Boolean value that is on the right of the relational operand.")]
        private Value rightValue;

        #endregion

        #region Variables

        public RelationalOperation Operation
        {
            get => operation;
            set => operation = value;
        } // relational operation between on left and right boolean values

        public Value LeftValue
        {
            get => leftValue;
            set => leftValue = value;
        } // boolean value that is on the left of the relational operand

        public Value RightValue
        {
            get => rightValue;
            set => rightValue = value;
        } // boolean value that is on the right of the relational operand

        #endregion

        #region Custom Methods

        /// <summary>
        /// Check and returns boolean that is a result of relational operation.
        /// </summary>
        /// <returns>A result of relational operation on left and right boolean values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <c>Operation</c> is not handled.</exception>
        public override dynamic GetValue()
        {
            return Operation switch
            {
                RelationalOperation.Equal => LeftValue.EqualTo(RightValue),
                RelationalOperation.LessThan => LeftValue.LessThan(RightValue),
                RelationalOperation.GreaterThan => LeftValue.GreaterThan(RightValue),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion
    }
}