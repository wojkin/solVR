using System.Collections.Generic;
using UnityEngine;

namespace UI.Interactable
{
    /// <summary>
    /// Class for handling intractability for list of <see cref="T"/> objects.
    /// </summary>
    /// <typeparam name="T">Type of objects in the list, that can be interactable.</typeparam>
    public abstract class InteractableOfType<T> : Interactable
    {
        #region Serialized Fields

        /// <summary>
        /// List of objects to trigger setting interactable.
        /// </summary>
        [Tooltip("List of objects to trigger setting interactable.")] [SerializeField]
        private List<T> interactables;

        /// <summary>
        /// Flag representing whether the object should look for first level children with <see cref="T"/> type.
        /// </summary>
        [Tooltip("Flag representing whether the object should look for first level children with corresponding type.")]
        [SerializeField]
        private bool findInteractablesInChildren;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets list of objects interactable based on <see cref="value"/>.
        /// Looks fo interactable children if <see cref="findInteractablesInChildren"/> flag is set to true.
        /// </summary>
        /// <param name="value">Boolean value that is assigned to an object which is interactable.</param>
        public override void SetInteractable(bool value)
        {
            foreach (var elem in interactables) SetInteractableListItem(elem, value);
            if (findInteractablesInChildren) SetFirstLevelChildrenInteractable(value);
        }

        /// <summary>
        /// Sets gameObject first level interactable children based on <see cref="value"/>.
        /// </summary>
        /// <param name="value">Boolean value that is assigned to children which are interactable.</param>
        private void SetFirstLevelChildrenInteractable(bool value)
        {
            for (var id = 0; id < transform.childCount; id++)
            {
                var child = transform.GetChild(id);
                var childInteractables = child.GetComponents<T>();
                foreach (var interactable in childInteractables) SetInteractableListItem(interactable, value);
            }
        }

        /// <summary>
        /// Sets <see cref="listItem"/> interactable based on <see cref="value"/>.
        /// </summary>
        /// <param name="listItem">List element which interactable property can be changed.</param>
        /// <param name="value">Boolean value that is assigned to <see cref="listItem"/> which is interactable.</param>
        protected abstract void SetInteractableListItem(T listItem, bool value);

        #endregion
    }
}