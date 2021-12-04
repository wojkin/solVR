using System;

namespace VisualScripting.Values.Enums
{
    /// <summary>
    /// Enum that represents a relational operation type. 
    /// </summary>
    public enum RelationalOperator
    {
        Equal,
        LessThan,
        GreaterThan
    }

    /// <summary>
    /// Extension to <see cref="RelationalOperator"/> enum to get matching operators to operations.
    /// </summary>
    public static class RelationalOperatorExtensions
    {
        #region Custom Methods

        /// <summary>
        /// Returns relational operators based on an operation.
        /// </summary>
        /// <param name="operatorn">Relational operation that is matched to operator.</param>
        /// <returns>A string that is an operator that represents relational operation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Exception throws then operation don't have any operator matched.</exception>
        public static string GetRelationalOperator(this RelationalOperator @operator)
        {
            return @operator switch
            {
                RelationalOperator.Equal => "==",
                RelationalOperator.LessThan => "<",
                RelationalOperator.GreaterThan => ">",
                _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
            };
        }

        #endregion
    }
}