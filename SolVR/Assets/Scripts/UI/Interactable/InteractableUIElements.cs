namespace UI.Interactable
{
    /// <summary>
    /// Class for handling intractability for list of <see cref="UIElement"/> objects.
    /// </summary>
    public class InteractableUIElements : InteractableOfType<UIElement>
    {
        #region Custom Methods

        /// <summary>
        /// Sets <see cref="listItem"/> interactable based on <see cref="value"/>.
        /// </summary>
        /// <param name="listItem">UIElement which interactable property is set.</param>
        /// <param name="value"><inheritdoc/></param>
        protected override void SetInteractableListItem(UIElement listItem, bool value)
        {
            listItem.Interactable = value;
        }

        #endregion
    }
}