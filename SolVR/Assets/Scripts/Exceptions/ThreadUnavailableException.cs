using System;

namespace Exceptions
{
    /// <summary>
    /// An exception thrown when a not permitted action is performed on a thread which is currently busy or blocked.
    /// </summary>
    public class ThreadUnavailableException : Exception
    {
        public ThreadUnavailableException()
        {
        }

        public ThreadUnavailableException(string message)
            : base(message)
        {
        }

        public ThreadUnavailableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}