using Levels;
using Managers;
using ScriptableObjects.Environments;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.List
{
    /// <summary>
    /// Class for an element of a level list.
    /// </summary>
    public class UILevelListElement : MonoBehaviour, IUIListElement
    {
        #region Serialized Fields

        /// <summary>Text component for displaying the level's name.</summary>
        [Tooltip("Level name text.")] [SerializeField]
        private TextMeshProUGUI levelName;

        /// <summary>Button for loading the level.</summary>
        [Tooltip("Button to load a level.")] [SerializeField]
        private Button loadLevelButton;

        #endregion

        #region IUIListElement Methods

        /// <summary>
        /// Populates the fields of a level list element.
        /// </summary>
        /// <param name="listElementData"><see cref="Environment"/> with data to fill UI list element.</param>
        public void Populate(Object listElementData)
        {
            var levelData = (Level)listElementData;

            levelName.text = levelData.environmentName; // set the level name

            // add on click listener for persistently storing the level data
            loadLevelButton.onClick.AddListener(() => PersistentLevelData.Instance.SetLevelData(levelData));
            // add on click listener for loading the level
            loadLevelButton.onClick.AddListener(() => CustomSceneManager.Instance.QueueLoadLevel(levelData.scene));
        }

        #endregion
    }
}