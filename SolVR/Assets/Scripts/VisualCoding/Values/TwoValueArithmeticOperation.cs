using System;

namespace VisualCoding.Values
{
    /// <summary>
    /// Class representing value as a result of a arithmetic operation on two values.
    /// </summary>
    public class TwoValueArithmeticOperation : Value
    {
        public Value left; // value that is on the left of the operand
        
        public Value right; // value that is on the right of the operand
        
        public ArithmeticOperation Operation { get; set; } // arithmetic operation performed on right and left values
        
        /// <summary>
        /// Calculates and returns value that is a result of arithmetic operation.
        /// </summary>
        /// <returns>A result of arithmetic operation on left and right values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <c>Operation</c> is not handled.</exception>
        public override dynamic GetValue()
        {
            return Operation switch
            {
                
                ArithmeticOperation.Addition => left.GetValue() + right.GetValue(),
                ArithmeticOperation.Difference => left.GetValue() - right.GetValue(),
                ArithmeticOperation.Multiplication => left.GetValue() * right.GetValue(),
                _ => throw new ArgumentOutOfRangeException("Operation","This operation is not handled.")
            };
        }
    }

    /// <summary>
    /// Enum that represents an arithmetic operation type. 
    /// </summary>
    public enum ArithmeticOperation
    {
        Addition, Difference, Multiplication
    }
}