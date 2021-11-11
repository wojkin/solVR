using System.Collections.Generic;
using Tasks;
using TMPro;
using UI.List;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class for displaying <see cref="Task"/> fields as texts on UI.
    /// </summary>
    public class UITaskPanel : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Text for displaying the task name.</summary>
        [SerializeField] private TextMeshProUGUI taskName;

        /// <summary>Text for displaying the task description.</summary>
        [SerializeField] private TextMeshProUGUI description;

        /// <summary>Task data to display as texts and lists.</summary>
        [SerializeField] private Task task;

        /// <summary>UI list for displaying task failure conditions.</summary>
        [SerializeField] private UIList failures;

        /// <summary>UI list for displaying task conditions.</summary>
        [SerializeField] private UIList conditions;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialise fields.
        /// </summary>
        private void Awake()
        {
            taskName.text = task.Description.taskName;
            description.text = task.Description.description;
        }

        /// <summary>
        /// Initialize UI lists' elements.
        /// </summary>
        private void Start()
        {
            failures.ChangeListElements(new List<Object>(task.failures));
            conditions.ChangeListElements(new List<Object>(task.conditions));
        }

        #endregion
    }
}