using Controls.Interactions;

namespace UI.Interactable
{
    /// <summary>
    /// Class for handling intractability for list of <see cref="Grabbable"/> objects.
    /// </summary>
    public class InteractableGrabbables : InteractableOfType<Grabbable>
    {
        #region Custom Methods

        /// <summary>
        /// <inheritdoc/>.
        /// </summary>
        /// <param name="listItem">Grabbable which interactable property is set.</param>
        /// <param name="value"><inheritdoc/></param>
        protected override void SetInteractableListItem(Grabbable listItem, bool value)
        {
            listItem.Interactable = value;
        }

        #endregion
    }
}