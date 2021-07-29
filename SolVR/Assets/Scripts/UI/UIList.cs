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
        [Tooltip("Template for a UI element in the list")]
        [SerializeField]
        private GameObject template;
        
        [Tooltip("List of ScriptableObjects containing data to fill the template")]
        [SerializeField]
        private List<ScriptableObject> list;
        
        /// <summary>
        /// Initialize all UI list elements based on a list of ScriptableObjects.
        /// Creates every UI element from a template, populates it with data from a ScriptableObject and shows it.
        /// </summary>
        void InitializeListElements()
        {
            foreach (var data in list)
            {
                var listEntry = Instantiate(template, gameObject.transform);
                var listElement = listEntry.GetComponent<IUIListElement>();
                listElement.Populate(data);
                listEntry.SetActive(true);
            }
        }

        /// <summary>
        /// Initialization of list elements.
        /// </summary>
        private void Awake()
        {
            InitializeListElements();
        }
    }
}
