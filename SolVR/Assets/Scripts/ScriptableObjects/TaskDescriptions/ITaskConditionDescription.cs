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

        #endregion
    }
}