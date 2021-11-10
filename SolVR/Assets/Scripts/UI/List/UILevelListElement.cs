using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.List
{
    /// <summary>
    /// Class for UI element in levels' list. Contains name of the level and button to load it.
    /// </summary>
    public class UILevelListElement : MonoBehaviour, IUIListElement
    {
        #region Serialized Fields

        [Tooltip("Level name text.")] [SerializeField]
        private TextMeshProUGUI levelName;

        [Tooltip("Button to load a level.")] [SerializeField]
        private Button playButton;

        #endregion

        #region IUIListElement Methods

        /// <summary>
        /// Populates the fields of a level list element.
        /// Sets levelName filed with listElementData.environmentName and
        /// adds loading level on button onClick event based on listElementData.scene.
        /// </summary>
        /// <param name="listElementData">ScriptableObject.Environment with data to fill UI list element.</param>
        public void Populate(Object listElementData)
        {
            var levelData = (Level) listElementData;
            levelName.text = levelData.environmentName;
            playButton.onClick.AddListener(() =>
                CustomSceneManager.Instance.QueueLoadScene(levelData.scene)
            );
        }

        #endregion
    }
}