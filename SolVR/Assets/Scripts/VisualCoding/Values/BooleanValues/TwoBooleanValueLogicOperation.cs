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
        #region Serialized Fields

        /// <summary>A logic operation performed on right and left boolean values.</summary>
        [SerializeField] [Tooltip("A logic operation performed on right and left boolean values.")]
        private LogicOperation operation;

        /// <summary> Boolean value that is on the left of the logic operand.</summary>
        [SerializeField] [Tooltip("Boolean value that is on the left of the logic operand.")]
        private BooleanValue left;

        /// <summary>Boolean value that is on the right of the logic operand.</summary>
        [SerializeField] [Tooltip("Boolean value that is on the right of the logic operand.")]
        private BooleanValue right;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="operation"/></summary>
        public LogicOperation Operation
        {
            get => operation;
            set => operation = value;
        }

        /// <summary><inheritdoc cref="left"/></summary>
        public BooleanValue Left
        {
            get => left;
            set => left = value;
        }

        /// <summary><inheritdoc cref="right"/></summary>
        public BooleanValue Right
        {
            get => right;
            set => right = value;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Check and returns boolean that is a result of logic operation.
        /// </summary>
        /// <returns>A result of logic operation on left and right boolean values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <see cref="Operation"/> is not handled.</exception>
        public override bool GetValue()
        {
            return Operation switch
            {
                LogicOperation.And => Left.GetValue() && Right.GetValue(),
                LogicOperation.Or => Left.GetValue() || Right.GetValue(),
                LogicOperation.Equal => Left.GetValue() == Right.GetValue(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion
    }
}