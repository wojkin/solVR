using System.Collections.Generic;
using Levels;
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

        /// <summary>UI list for displaying task failure conditions.</summary>
        [SerializeField] private UIList failures;

        /// <summary>UI list for displaying task conditions.</summary>
        [SerializeField] private UIList conditions;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Subscribes to needed event.
        /// </summary>
        private void OnEnable()
        {
            PersistentLevelData.Instance.LevelLoaded += InitializeTaskData;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed event.
        /// </summary>
        private void OnDisable()
        {
            if (PersistentLevelData.Instance != null) PersistentLevelData.Instance.LevelLoaded -= InitializeTaskData;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Initialise fields and UI lists' elements dependent on level <see cref="PersistentLevelData"/>.
        /// </summary>
        private void InitializeTaskData()
        {
            var task = PersistentLevelData.Instance.Task;
            taskName.text = task.Description.taskName;
            description.text = task.Description.description;
            failures.ChangeListElements(new List<Object>(task.failures));
            conditions.ChangeListElements(new List<Object>(task.conditions));
        }

        #endregion
    }
}