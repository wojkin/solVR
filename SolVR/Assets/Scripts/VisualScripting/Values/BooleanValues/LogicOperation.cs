using System;
using UnityEngine;

namespace VisualScripting.Values.BooleanValues
{
    /// <summary>
    /// Class representing boolean value as a result of a logic operation on two boolean values.
    /// </summary>
    public class LogicOperation : BooleanValue
    {
        #region Serialized Fields

        /// <summary>A logic operation performed on right and left boolean values.</summary>
        [SerializeField] [Tooltip("A logic operation performed on right and left boolean values.")]
        private Enums.LogicOperator @operator;

        /// <summary> Boolean value that is on the left of the logic operand.</summary>
        [SerializeField] [Tooltip("Boolean value that is on the left of the logic operand.")]
        private BooleanValue left;

        /// <summary>Boolean value that is on the right of the logic operand.</summary>
        [SerializeField] [Tooltip("Boolean value that is on the right of the logic operand.")]
        private BooleanValue right;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="operator"/></summary>
        public Enums.LogicOperator Operator
        {
            get => @operator;
            set => @operator = value;
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
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <see cref="Operator"/> is not handled.</exception>
        public override bool GetValue()
        {
            return Operator switch
            {
                Enums.LogicOperator.And => left.GetValue() && right.GetValue(),
                Enums.LogicOperator.Or => left.GetValue() || right.GetValue(),
                Enums.LogicOperator.Equal => left.GetValue() == right.GetValue(),
                _ => throw new ArgumentOutOfRangeException(Operator.ToString(), "This operation is not handled.")
            };
        }

        #endregion
    }
}