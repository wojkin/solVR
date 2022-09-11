namespace DeveloperTools
{
    /// <summary>
    /// Class for logging messages to different destinations.
    /// </summary>
    public static class Logger
    {
        #region Variables

        /// <summary>Delegate for message log.</summary>
        public delegate void LogHandler(string message);

        /// <summary>Message log event.</summary>
        public static event LogHandler LogEvent;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invokes a log event with a given message.
        /// </summary>
        /// <param name="message">A message to be logged.</param>
        public static void Log(string message)
        {
            LogEvent?.Invoke(message);
        }

        #endregion
    }
}