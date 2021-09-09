using System;

namespace VisualCoding.Values.BooleanValues
{
    /// <summary>
    /// Class representing boolean value as a result of a relational operation on two conditions.
    /// </summary>
    public class BooleanValueRelationalOperators : BooleanValue
    {
        public Value leftValue; // condition that is on the left of the relational operand
        
        public Value rightValue; // condition that is on the right of the relational operand

        public RelationalOperation Operation { get; set; } // relational operation between on left and right condition
        
        /// <summary>
        /// Check and returns boolean that is a result of relational operation.
        /// </summary>
        /// <returns>A result of relational operation on left and right result of checked condition.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <c>Operation</c> is not handled.</exception>
        public override dynamic GetValue()
        {
            return Operation switch
            {
                RelationalOperation.Equal => leftValue.EqualTo(rightValue),
                RelationalOperation.Less => leftValue.LessThan(rightValue),
                RelationalOperation.Greater => leftValue.GreaterThan(rightValue),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    /// <summary>
    /// Enum that represents a relational operation type. 
    /// </summary>
    public enum RelationalOperation
    {
        Equal, Less, Greater
    }
}