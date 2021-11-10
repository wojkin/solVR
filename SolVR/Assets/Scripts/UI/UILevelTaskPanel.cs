using ScriptableObjects.Environments;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class for displaying <see cref="Level"/> fields as texts on UI.
    /// </summary>
    public class UILevelTaskPanel : MonoBehaviour
    {
        /// <summary>Text for displaying the level name.</summary>
        [SerializeField] private TextMeshProUGUI levelName;

        /// <summary>Text for displaying the task description.</summary>
        [SerializeField] private TextMeshProUGUI description;

        /// <summary>Level data to display as texts.</summary>
        [SerializeField] private Level level;

        /// <summary>
        /// Initialise fields.
        /// </summary>
        private void Awake()
        {
            levelName.text = level.environmentName;
            description.text = level.levelTaskDescription.description;
        }

    }
}