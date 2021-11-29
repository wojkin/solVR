namespace VisualScripting.Blocks.Properties
{
    /// <summary>
    /// Interface for resetting block's values that depends on execution.
    /// </summary>
    public interface IResettable
    {
        #region Custom Methods

        /// <summary>
        /// Resets block execution dependent values.
        /// </summary>
        void Reset();

        #endregion
    }
}