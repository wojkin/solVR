using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class for a list containing UI elements.
    /// Creates UI elements based on a list of ScriptableObjects and displays them in a specific template. 
    /// </summary>
    public class UIList : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Template for an element of the list.</summary>
        [Tooltip("Template for a UI element in the list")] [SerializeField]
        private GameObject template;

        /// <summary>List of <see cref="ScriptableObject"/>s containing data to fill the template.</summary>
        [Tooltip("List of ScriptableObjects containing data to fill the template")] [SerializeField]
        private List<ScriptableObject> list;

        #endregion

        #region Variables

        /// <summary>List of currently displayed elements.</summary>
        private readonly List<GameObject> _entries = new List<GameObject>();

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Displays list elements.
        /// </summary>
        private void Awake()
        {
            DisplayListElements();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Replaces current list elements with new ones.
        /// </summary>
        /// <param name="newList">New data to be displayed.</param>
        public void ChangeListElements(List<ScriptableObject> newList)
        {
            Clear();
            list = newList;
            DisplayListElements();
        }

        /// <summary>
        /// Initialize all UI list elements based on a list of ScriptableObjects.
        /// </summary>
        private void DisplayListElements()
        {
            foreach (var data in list)
            {
                var listEntry = Instantiate(template, gameObject.transform);
                var listElement = listEntry.GetComponent<IUIListElement>();
                listElement.Populate(data);
                listEntry.SetActive(true);
                _entries.Add(listEntry);
            }
        }

        /// <summary>
        /// Removes all list elements.
        /// </summary>
        private void Clear()
        {
            foreach (var entry in _entries)
                Destroy(entry);

            _entries.Clear();
        }

        #endregion
    }
}