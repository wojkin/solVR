using System;
using UnityEngine;
using VisualCoding.Values.Enums;

namespace VisualCoding.Values
{
    /// <summary>
    /// Class representing value as a result of a arithmetic operation on two values.
    /// </summary>
    public class TwoValueArithmeticOperation : Value
    {

        [SerializeField] [Tooltip("Arithmetic operation performed on right and left values.")]
        private ArithmeticOperation operation;
        
        [SerializeField] [Tooltip("A value that is on the left of the operand.")]
        private Value left; 
        
        [SerializeField] [Tooltip("A value that is on the right of the operand.")]
        private Value right;

        public ArithmeticOperation Operation
        {
            get => operation;
            set => operation = value;
        } // arithmetic operation performed on right and left values

        public Value Left
        {
            get => left;
            set => left = value;
        } // value that is on the left of the operand

        public Value Right
        {
            get => right;
            set => right = value;
        } // value that is on the right of the operand


        /// <summary>
        /// Calculates and returns value that is a result of arithmetic operation.
        /// </summary>
        /// <returns>A result of arithmetic operation on left and right values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception when <c>Operation</c> is not handled.</exception>
        public override dynamic GetValue()
        {
            return Operation switch
            {
                ArithmeticOperation.Addition => Left.GetValue() + Right.GetValue(),
                ArithmeticOperation.Difference => Left.GetValue() - Right.GetValue(),
                ArithmeticOperation.Multiplication => Left.GetValue() * Right.GetValue(),
                _ => throw new ArgumentOutOfRangeException("Operation", "This operation is not handled.")
            };
        }
    }
}