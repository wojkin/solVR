namespace UI.Interactable
{
    /// <summary>
    /// Class for handling intractability for list of <see cref="Interactable"/> objects.
    /// </summary>
    public class InteractableList : InteractableOfType<Interactable>
    {
        #region Custom Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="listItem">Interactable object which interactable is set based on value.</param>
        /// <param name="value"><inheritdoc/></param>
        protected override void SetInteractableListItem(Interactable listItem, bool value)
        {
            listItem.SetInteractable(value);
        }

        #endregion
    }
}