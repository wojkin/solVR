using System;

namespace Exceptions
{
    /// <summary>
    /// An exception thrown when a command is executed on an incompatible robot (it doesn't implement the required
    /// interface).
    /// </summary>
    public class IncompatibleRobotException : Exception
    {
        #region Custom Methods

        public IncompatibleRobotException()
        {
        }

        public IncompatibleRobotException(string message)
            : base(message)
        {
        }

        public IncompatibleRobotException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}