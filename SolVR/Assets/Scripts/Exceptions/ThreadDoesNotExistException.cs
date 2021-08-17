using System;

namespace Exceptions
{
    /// <summary>
    /// An exception thrown when a thread with a given ID doesn't exist.
    /// </summary>
    public class ThreadDoesNotExistException : Exception
    {
        public ThreadDoesNotExistException()
        {
        }

        public ThreadDoesNotExistException(string message)
            : base(message)
        {
        }

        public ThreadDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}