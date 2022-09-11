namespace VisualScripting.Execution.Enums
{
    /// <summary>
    /// Enum representing the state of a block execution thread.
    /// </summary>
    /// <remarks>
    /// Running - the thread is currently executing a block.
    /// Stopped - the thread is not executing blocks.
    /// </remarks>
    public enum BlockThreadState
    {
        Running,
        Stopped
    }
}