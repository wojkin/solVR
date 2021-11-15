using System;

namespace VisualCoding.Values.Enums
{
    /// <summary>
    /// Enum that represents a relational operation type. 
    /// </summary>
    public enum RelationalOperation
    {
        Equal,
        LessThan,
        GreaterThan
    }

    /// <summary>
    /// Extension to <see cref="RelationalOperation"/> enum to get matching operators to operations.
    /// </summary>
    public static class RelationalOperationExtensions
    {
        #region Custom Methods

        /// <summary>
        /// Returns relational operators based on an operation.
        /// </summary>
        /// <param name="operation">Relational operation that is matched to operator.</param>
        /// <returns>A string that is an operator that represents relational operation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Exception throws then operation don't have any operator matched.</exception>
        public static string GetRelationalOperator(this RelationalOperation operation)
        {
            return operation switch
            {
                RelationalOperation.Equal => "==",
                RelationalOperation.LessThan => "<",
                RelationalOperation.GreaterThan => ">",
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
            };
        }

        #endregion
    }
}