using System;
using VisualCoding.Values.Enums;

namespace VisualCoding.Values.BooleanValues
{
    /// <summary>
    /// Class representing condition as a result of a logic operation on two conditions.
    /// </summary>
    public class TwoBooleanValueLogicOperation : BooleanValue
    {
        public BooleanValue left; // condition that is on the left of the logic operand
        
        public BooleanValue right; // condition that is on the right of the logic operand

        public LogicOperation Operation { get; set; } // logic operation performed on right and left condition
        
        /// <summary>
        /// Check and returns boolean that is a result of logic operation.
        /// </summary>
        /// <returns>A result of logic operation on left and right result of checked condition.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <c>Operation</c> is not handled.</exception>
        public override dynamic GetValue()
        {
            return Operation switch
            {
                LogicOperation.And => left.GetValue() &&  right.GetValue(),
                LogicOperation.Or => left.GetValue() ||  right.GetValue(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}