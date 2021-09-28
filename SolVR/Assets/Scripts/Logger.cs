/// <summary>
/// Class for logging messages to different destinations.
/// </summary>
public static class Logger
{
    #region Variables

    /// <summary>a delegate for logging a message</summary>
    public delegate void Log(string message);

    /// <summary>a message log event</summary>
    public static event Log LogEvent;

    #endregion

    #region Custom Methods

    /// <summary>
    /// Calls a log event with a given message.
    /// </summary>
    /// <param name="message">A message to be logged.</param>
    public static void OnLog(string message)
    {
        LogEvent?.Invoke(message);
    }

    #endregion
}