using System;

namespace Exceptions
{
    /// <summary>
    /// An exception thrown when a command is executed on an incompatible robot (it doesn't implement the required
    /// interface).
    /// </summary>
    public class IncompatibleCommandException : Exception
    {
        public IncompatibleCommandException()
        {
        }

        public IncompatibleCommandException(string message)
            : base(message)
        {
        }

        public IncompatibleCommandException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}