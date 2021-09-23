using System;
using UnityEngine;
using VisualCoding.Values.Enums;

namespace VisualCoding.Values.BooleanValues
{
    /// <summary>
    /// Class representing boolean value as a result of a logic operation on two boolean values.
    /// </summary>
    public class TwoBooleanValueLogicOperation : BooleanValue
    {
        [SerializeField] [Tooltip("Boolean value that is on the left of the logic operand.")]
        private BooleanValue left;

        [SerializeField] [Tooltip("Boolean value that is on the right of the logic operand.")]
        private BooleanValue right;

        [SerializeField] [Tooltip("A logic operation performed on right and left boolean values.")]
        private LogicOperation operation;

        public BooleanValue Left
        {
            get => left;
            set => left = value;
        } // boolean value that is on the left of the logic operand

        public BooleanValue Right
        {
            get => right;
            set => right = value;
        } // boolean value  that is on the right of the logic operand

        public LogicOperation Operation
        {
            get => operation;
            set => operation = value;
        } // logic operation performed on right and left boolean values

        /// <summary>
        /// Check and returns boolean that is a result of logic operation.
        /// </summary>
        /// <returns>A result of logic operation on left and right boolean values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <c>Operation</c> is not handled.</exception>
        public override dynamic GetValue()
        {
            return Operation switch
            {
                LogicOperation.And => Left.GetValue() && Right.GetValue(),
                LogicOperation.Or => Left.GetValue() || Right.GetValue(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}