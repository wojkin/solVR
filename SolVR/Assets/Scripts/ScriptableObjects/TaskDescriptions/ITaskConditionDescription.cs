namespace ScriptableObjects.TaskDescriptions
{
    /// <summary>
    /// Interface used to get values from task condition description fields.
    /// </summary>
    public interface ITaskConditionDescription
    {
        #region Custom Methods

        /// <summary>
        /// Getter for text description.
        /// </summary>
        /// <returns>String with description about task condition.</returns>
        string GetDescription();

        /// <summary>
        /// Getter for default message for state of task condition.
        /// </summary>
        /// <returns>A string message.</returns>
        string GetDefaultStateMessage();

        /// <summary>
        /// Getter for message when state of task condition is change because condition is met.
        /// </summary>
        /// <returns>A string message.</returns>
        string GetConditionMetStateMessage();

        #endregion
    }
}