using System;

namespace VisualScripting.Values.Enums
{
    /// <summary>
    /// Enum that represents an arithmetic operation type. 
    /// </summary>
    public enum ArithmeticOperation
    {
        Addition,
        Difference,
        Multiplication
    }

    /// <summary>
    /// Extension to <see cref="ArithmeticOperation"/> enum to get matching operators to operations.
    /// </summary>
    public static class ArithmeticOperationExtensions
    {
        #region Custom Methods

        /// <summary>
        /// Returns arithmetic operators based on an operation.
        /// </summary>
        /// <param name="operation">Arithmetic operation that is matched to operator.</param>
        /// <returns>A string that is an operator that represents arithmetic operation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Exception throws then operation don't have any operator matched.</exception>
        public static string GetArithmeticOperator(this ArithmeticOperation operation)
        {
            return operation switch
            {
                ArithmeticOperation.Addition => "+",
                ArithmeticOperation.Difference => "-",
                ArithmeticOperation.Multiplication => "*",
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
            };
        }

        #endregion
    }
}