public static class Logger
{
    public delegate void Message(string message);

    public static event Message LogEvent;

    public static void OnLog(string message)
    {
        LogEvent?.Invoke(message);
    }
}