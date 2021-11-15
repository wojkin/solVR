namespace VisualCoding.Execution.Enums
{
    /// <summary>
    /// Enum representing the state of a block execution thread.
    /// Running - the thread is currently executing a block.
    /// Stopped - the thread is not executing blocks.
    /// </summary>
    public enum BlockThreadState
    {
        Running,
        Stopped
    }
}