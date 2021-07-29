﻿/// <summary>
/// Class for logging messages to different destinations.
/// </summary>
public static class Logger
{
    // a delegate for logging a message
    public delegate void Log(string message);

    // a message log event
    public static event Log LogEvent;

    /// <summary>
    /// Calls a log event with a given message.
    /// </summary>
    /// <param name="message">A message to be logged.</param>
    public static void OnLog(string message)
    {
        LogEvent?.Invoke(message);
    }
}